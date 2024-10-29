using Autodesk.Revit.DB;
using Autodesk.Revit.DB.IFC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools
{
    class Func_CutWall
    {
        public void Intersec_With_Floor(Document doc, Wall wall_select, Floor floor)
        {
            BoundingBoxXYZ boundingbox_wall = wall_select.get_BoundingBox(null);
            XYZ max1 = boundingbox_wall.Max;

            BoundingBoxXYZ boundingbox_floor = floor.get_BoundingBox(null);
            XYZ min2 = boundingbox_floor.Min;

            if (max1.Z > min2.Z)
            {
                double z = min2.Z - max1.Z;
                double z_wall = wall_select.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET).AsDouble();
                double z_new = z_wall + z;
                wall_select.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET).Set(z_new);
            }
        }
        public void Intersec_With_Framing(Document doc, Wall wall_select, FamilyInstance Framing)
        {

            #region 'Xet cat nhau'
            IList<Reference> sideFaces_ex = HostObjectUtils.GetSideFaces(wall_select, ShellLayerType.Exterior);
            Element e_exterior = doc.GetElement(sideFaces_ex[0]);
            Face face_ex = e_exterior.GetGeometryObjectFromReference(sideFaces_ex[0]) as Face;

            IList<Reference> sideFaces_in = HostObjectUtils.GetSideFaces(wall_select, ShellLayerType.Interior);
            Element e_interior = doc.GetElement(sideFaces_in[0]);
            Face face_in = e_interior.GetGeometryObjectFromReference(sideFaces_in[0]) as Face;

            LocationCurve lc = Framing.Location as LocationCurve;
            XYZ st = lc.Curve.GetEndPoint(0);
            XYZ ed = lc.Curve.GetEndPoint(1);
            XYZ u = (ed - st).Normalize();

            double h = GetHeight(Framing);
            double b = GetWidth(Framing);
            double l = st.DistanceTo(ed);

            Line line = lc.Curve as Line;
            XYZ vectorX = line.Direction;
            XYZ vectorZ = XYZ.BasisZ;
            XYZ vectorY = vectorZ.CrossProduct(vectorX);
            Parameter zJus = Framing.LookupParameter("z Justification");
            int zPos = zJus.AsInteger();
            double x = 0;
            switch (zPos)
            {
                case 0: // Top   
                    x = 1;
                    break;
                case 1: // Center  
                case 2: // Origin  
                    x = 0.5;
                    break;
                case 3: // Bottom  
                    x = 0;
                    break;
            }
            XYZ Diem1, Diem2, Diem3, Diem4;
            XYZ origin = st - vectorY * b / 2;
            // - vectorZ * h * x;
            Diem1 = origin;
            Diem2 = origin + vectorY * b;
            Diem3 = origin + vectorY * b - vectorZ * h * x;
            Diem4 = origin - vectorZ * h * x;

            Line L1 = Line.CreateBound(Diem1, Diem1 + u * l);
            Line L2 = Line.CreateBound(Diem2, Diem2 + u * l);
            Line L3 = Line.CreateBound(Diem3, Diem3 + u * l);
            Line L4 = Line.CreateBound(Diem4, Diem4 + u * l);

            List<Line> List_Line = new List<Line>();
            List_Line.Add(L1);
            List_Line.Add(L2);
            List_Line.Add(L3);
            List_Line.Add(L4);

            List<XYZ> List_PointIntersec_Ex = new List<XYZ>();
            List<XYZ> List_PointIntersec_In = new List<XYZ>();
            foreach (var item in List_Line)
            {
                if (face_ex.Intersect(item, out IntersectionResultArray results) == SetComparisonResult.Overlap)
                {
                    foreach (IntersectionResult intersectresult in results)
                    {
                        List_PointIntersec_Ex.Add(intersectresult.XYZPoint);
                    }
                }
            }
            foreach (var item in List_Line)
            {
                if (face_in.Intersect(item, out IntersectionResultArray results) == SetComparisonResult.Overlap)
                {
                    foreach (IntersectionResult intersectresult in results)
                    {
                        List_PointIntersec_In.Add(intersectresult.XYZPoint);
                    }
                }
            }


            #endregion
            if (List_PointIntersec_Ex.Count <= 0 && List_PointIntersec_In.Count <= 0)
            {
                // va cham nhung k cat nhau
                BoundingBoxXYZ boundingbox_wall = wall_select.get_BoundingBox(null);
                XYZ max1 = boundingbox_wall.Max;

                BoundingBoxXYZ boundingbox_framing = Framing.get_BoundingBox(null);
                XYZ min2 = boundingbox_framing.Min;

                if (max1.Z > min2.Z)
                {
                    double z = min2.Z - max1.Z;
                    double z_wall = wall_select.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET).AsDouble();
                    double z_new = z_wall + z;
                    wall_select.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET).Set(z_new);
                }
            }
            else
            {
                var solid = GetTargetSolids(Framing);
                string namesolid = "DBIM" + wall_select.Id.ToString() + Framing.Id.ToString();
                namesolid = CheckNameSolid(doc, namesolid);
                FamilyInstance famSolid =  CreateFamilyInstaceFromSolid(doc, solid.FirstOrDefault<Solid>(), namesolid, wall_select);
                if (famSolid != null)
                {
                    JoinGeometryUtils.JoinGeometry(doc, famSolid, wall_select);
                    View v = doc.ActiveView;
                    List<ElementId> ids = new List<ElementId>();
                    ids.Add(famSolid.Id);
                    v.HideElements(ids);
                }               
            }


        }   
        public string CheckNameSolid(Document doc, string namesolid)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
            string check = "";
            do
            {
                check = "KCO";
                foreach (FamilySymbol item in symbols)
                {
                    if (item.FamilyName == namesolid)
                    {
                        namesolid = namesolid + "1";
                        check = "CO";
                    }
                }
            } while (check == "CO");
            return namesolid;
        }
        public double GetHeight(FamilyInstance F)
        {
            BoundingBoxXYZ boundingBoxXYZ = F.get_BoundingBox(null);
            return boundingBoxXYZ.Max.Z - boundingBoxXYZ.Min.Z;
        }
        public double GetWidth(FamilyInstance F)
        {
            BoundingBoxXYZ BDbox = F.get_BoundingBox(null);

            LocationCurve lc = F.Location as LocationCurve;
            XYZ st = lc.Curve.GetEndPoint(0);
            XYZ ed = lc.Curve.GetEndPoint(1);
            XYZ u = (ed - st).Normalize();

            double l = st.DistanceTo(ed);

            if (u.IsAlmostEqualTo(XYZ.BasisX) || u.IsAlmostEqualTo(-XYZ.BasisX))
            {
                double Cheo = BDbox.Max.DistanceTo(new XYZ(BDbox.Min.X, BDbox.Min.Y, BDbox.Max.Z));
                double b = Math.Sqrt(Cheo * Cheo - l * l);
                return Math.Round(b);
            }
            else if (u.IsAlmostEqualTo(XYZ.BasisY) || u.IsAlmostEqualTo(-XYZ.BasisY))
            {
                double Cheo = BDbox.Max.DistanceTo(new XYZ(BDbox.Min.X, BDbox.Min.Y, BDbox.Max.Z));
                double b = Math.Sqrt(Cheo * Cheo - l * l);
                return Math.Round(b);
            }
            else
            {
                Line l_center = Line.CreateUnbound(new XYZ(st.X,st.Y,0), new XYZ(ed.X, ed.Y, 0));
                if ((st.X > ed.X && st.Y > ed.Y )|| (ed.X > st.X && ed.Y > st.Y))
                {
                    XYZ point1 = new XYZ(BDbox.Max.X, BDbox.Max.Y, 0);
                    XYZ point2 = new XYZ(BDbox.Max.X, BDbox.Min.Y, 0);
                    Line l_intersec = Line.CreateBound(point1, point2);
                    XYZ point_intersec = null;

                    point_intersec = GetIntersec_2Line(point1, point2, new XYZ(st.X, st.Y, 0), new XYZ(ed.X, ed.Y, 0));
                    XYZ point3 = null;
                    if (point1.DistanceTo(new XYZ(st.X, st.Y, 0)) < point1.DistanceTo(new XYZ(ed.X, ed.Y, 0)))
                    {
                        point3 = new XYZ(st.X, st.Y, 0);
                    }
                    else
                    {
                        point3 = new XYZ(ed.X, ed.Y, 0);
                    }
                    double canh1 = point1.DistanceTo(point_intersec);
                    double canh2 = point3.DistanceTo(point_intersec);
                    double canh3 = point1.DistanceTo(point3);

                    double tu = canh1 * canh1 + canh2 * canh2 - canh3 * canh3;
                    double mau = 2*canh1*canh2;

                    double Cos_Goc_intersec = tu / mau;
                    double Goc_intersec = Math.Acos(Cos_Goc_intersec);
                    double Goc_result = Math.PI - Goc_intersec;

                    double result = Math.Tan(Goc_result) * canh2;
                    double b = result* 2;
                    return Math.Round(b);

                }
                else if ((st.X < ed.X && st.Y > ed.Y) || (ed.X < st.X && ed.Y > st.Y))
                {
                    XYZ point1 = new XYZ(BDbox.Max.X, BDbox.Max.Y, 0);
                    XYZ point2 = new XYZ(BDbox.Min.X, BDbox.Max.Y, 0);
                    Line l_intersec = Line.CreateBound(point1, point2);
                    XYZ point_intersec = null;

                    point_intersec = GetIntersec_2Line(point1, point2, new XYZ(st.X, st.Y, 0), new XYZ(ed.X, ed.Y, 0));

                    XYZ point3 = null;
                    if (point1.DistanceTo(new XYZ(st.X, st.Y, 0)) < point1.DistanceTo(new XYZ(ed.X, ed.Y, 0)))
                    {
                        point3 = new XYZ(st.X, st.Y, 0);
                    }
                    else
                    {
                        point3 = new XYZ(ed.X, ed.Y, 0);
                    }
                    double canh1 = point2.DistanceTo(point_intersec);
                    double canh2 = point3.DistanceTo(point_intersec);
                    double canh3 = point2.DistanceTo(point3);

                    double tu = canh1 * canh1 + canh2 * canh2 - canh3 * canh3;
                    double mau = 2 * canh1 * canh2;

                    double Cos_Goc_intersec = tu / mau;
                    double Goc_intersec = Math.Acos(Cos_Goc_intersec);
                    double Goc_do_intersec = Goc_intersec * 180 / Math.PI;
                    double Goc_result = Math.PI - Goc_intersec;
                    double Goc_do_result = Goc_result * 180 / Math.PI;
                    double gocgoc = Math.Tan(Goc_result);
                    double result = Math.Tan(Goc_result) * canh2;
                    double b = result * 2;
                    return Math.Round(b);
                }
            }
            return double.NaN;
        }
        public List<XYZ> GetPoint_1(List<XYZ> List_PointIntersec_Ex, List<XYZ> List_PointIntersec_In, XYZ stpoint)
        {
            List<XYZ> List_Point = new List<XYZ>();
            List < XYZ > list_result = new List<XYZ>();
            foreach (var item in List_PointIntersec_Ex)
            {
                List_Point.Add(new XYZ(item.X, item.Y, 0));
            }
            foreach (var item in List_PointIntersec_In)
            {
                List_Point.Add(new XYZ(item.X, item.Y, 0));
            }
            XYZ DiemGoc = new XYZ(stpoint.X, stpoint.Y, 0);
           
            double kcmin = DiemGoc.DistanceTo(List_Point[0]);
            XYZ result = List_Point[0];
            foreach (var item in List_Point)
            {
                double kc = DiemGoc.DistanceTo(item);
                if (kc < kcmin)
                {
                    result = item;
                    kcmin = DiemGoc.DistanceTo(item);
                }
            }
            foreach (var item in List_PointIntersec_Ex)
            {
                if (item.X == result.X && item.Y == result.Y)
                {
                    list_result.Add(item);
                }
            }
            foreach (var item in List_PointIntersec_In)
            {
                if (item.X == result.X && item.Y == result.Y)
                {
                    list_result.Add(item);
                }
            }
            return list_result;
        }
        public List<XYZ> GetPoint_2(List<XYZ> List_PointIntersec_Ex, List<XYZ> List_PointIntersec_In, XYZ endpoint)
        {
            List<XYZ> List_Point = new List<XYZ>();
            List<XYZ> list_result = new List<XYZ>();
            foreach (var item in List_PointIntersec_Ex)
            {
                List_Point.Add(new XYZ(item.X, item.Y, 0));
            }
            foreach (var item in List_PointIntersec_In)
            {
                List_Point.Add(new XYZ(item.X, item.Y, 0));
            }
            XYZ DiemGoc = new XYZ(endpoint.X, endpoint.Y, 0);

            double kcmin = DiemGoc.DistanceTo(List_Point[0]);
            XYZ result = List_Point[0];
            foreach (var item in List_Point)
            {
                if (DiemGoc.DistanceTo(item) < kcmin)
                {
                    result = item;
                    kcmin = DiemGoc.DistanceTo(item);
                }
            }
            foreach (var item in List_PointIntersec_Ex)
            {
                if (item.X == result.X && item.Y == result.Y)
                {
                    list_result.Add(item);
                }
            }
            foreach (var item in List_PointIntersec_In)
            {
                if (item.X == result.X && item.Y == result.Y)
                {
                    list_result.Add(item);
                }
            }
            return list_result;
        }
        public XYZ GetCenterPoint(XYZ point, XYZ u, double kc, XYZ stpoint, XYZ endpoint)
        {
            XYZ st_point = new XYZ(stpoint.X, stpoint.Y, point.Z);
            XYZ ed_point = new XYZ(endpoint.X, endpoint.Y, point.Z);
            XYZ point1 = point + u * kc;
            XYZ point2 = point - u * kc;
            XYZ u1 = (point1 - st_point).Normalize();
            XYZ u2 = (ed_point - point1).Normalize();
            if (u1.IsAlmostEqualTo(u2))
            {
                return point1;
            }
            else
            {
                return point2;
            }
        }
        public List<Curve> CreateCurve(XYZ point1, XYZ point2, XYZ point3, XYZ point4)
        {
            List<Curve> result = new List<Curve>();
            if (point1.Z == point3.Z)
            {
                result.Add(Line.CreateBound(point1, point2) as Curve);
                result.Add(Line.CreateBound(point2, point4) as Curve);
                result.Add(Line.CreateBound(point4, point3) as Curve);
                result.Add(Line.CreateBound(point3, point1) as Curve);                              
            }
            else if (point1.Z == point4.Z)
            {
                result.Add(Line.CreateBound(point1, point2) as Curve);
                result.Add(Line.CreateBound(point2, point3) as Curve);
                result.Add(Line.CreateBound(point3, point4) as Curve);
                result.Add(Line.CreateBound(point4, point1) as Curve);
            }
            return result;
        }
        public List<Curve> list_curve_new(List<Curve> profile, List<Curve> list_curve) 
        {
            List<Curve> list_results = new List<Curve>();
            List<Curve> list_remove1 = new List<Curve>();
            List<Curve> list_remove2 = new List<Curve>();

            foreach (var item1 in profile)
            {
                foreach (var item2 in list_curve)
                {
                    if (item1.Intersect(item2) == SetComparisonResult.Equal)
                    {
                        List<Curve> list_remove = RemoveEquals(item1, item2);
                        if (list_remove.Count == 1)
                        {
                            list_results.Add(list_remove[0]);
                            list_remove1.Add(item1);
                            list_remove2.Add(item2);
                        }
                        else if (list_remove.Count == 2)
                        {
                            list_results.Add(list_remove[0]);
                            list_results.Add(list_remove[1]);
                            list_remove1.Add(item1);
                            list_remove2.Add(item2);
                        }                
                    }   
                }          
            }
            foreach (var item in list_remove1)
            {
                profile.Remove(item);
            }
            foreach (var item in list_remove2)
            {
                list_curve.Remove(item);
            }

            return list_results;

        }
        public List<Curve> list_curve_new22(List<Curve> profile, XYZ point1, XYZ point3)
        {
            List<Curve> list_results = new List<Curve>();
            List<Curve> list_curve = new List<Curve>();
            List<Curve> list_remove1 = new List<Curve>();
            List<Curve> list_remove2 = new List<Curve>();
            var max = profile[0].GetEndPoint(0).Z;

            foreach (var item in profile)
            {
                if (item.GetEndPoint(0).Z > max)
                {
                    max = item.GetEndPoint(0).Z;
                }
                else if (item.GetEndPoint(1).Z > max)
                {
                    max = item.GetEndPoint(1).Z;
                }
            }
            XYZ p1 = null, p2 = null;
            foreach (var item in profile)
            {
                if (item.GetEndPoint(0).Z == max && item.GetEndPoint(1).Z == max)
                {
                    p1 = item.GetEndPoint(0);
                    p2 = item.GetEndPoint(1);
                }
            }
            XYZ point_intersec1 = GetIntersec_3Point(p1, p2, point1);
            XYZ point_intersec2 = GetIntersec_3Point(p1, p2, point3);

            list_curve = CreateCurve(point_intersec1, point1, point_intersec2, point3);

            foreach (var item1 in profile)
            {
                foreach (var item2 in list_curve)
                {
                    if (item1.Intersect(item2) == SetComparisonResult.Equal)
                    {
                        List<Curve> list_remove = RemoveEquals(item1, item2);
                        if (list_remove.Count == 1)
                        {
                            list_results.Add(list_remove[0]);
                            list_remove1.Add(item1);
                            list_remove2.Add(item2);
                        }
                        else if (list_remove.Count == 2)
                        {
                            list_results.Add(list_remove[0]);
                            list_results.Add(list_remove[1]);
                            list_remove1.Add(item1);
                            list_remove2.Add(item2);
                        }
                    }
                }
            }
            foreach (var item in list_remove1)
            {
                profile.Remove(item);
            }
            foreach (var item in list_remove2)
            {
                list_curve.Remove(item);
            }
            list_results.Add(Line.CreateBound(point1,point3));
            list_results.Add(Line.CreateBound(point1, point_intersec1));
            list_results.Add(Line.CreateBound(point_intersec2, point3));
            return list_results;

        }
        public List<Curve> RemoveEquals(Curve C1, Curve C2)
        {
            Line l1 = C1 as Line ;
            Line l2 = C2 as Line ;

            XYZ point1 = l1.GetEndPoint(0);
            XYZ point2 = l1.GetEndPoint(1);
            XYZ point3 = l2.GetEndPoint(0);
            XYZ point4 = l2.GetEndPoint(1);

            Line out_C1 = null;
            Line out_C2 = null;
            if (point1.IsAlmostEqualTo(point3))
            {
                out_C1 = Line.CreateBound(point4, point2);
            }
            else if (point1.IsAlmostEqualTo(point4))
            {
                out_C1 = Line.CreateBound(point3, point2);
            }
            else if (point2.IsAlmostEqualTo(point3))
            {
                out_C1 = Line.CreateBound(point1, point4);
            }
            else if (point2.IsAlmostEqualTo(point4))
            {
                out_C1 = Line.CreateBound(point1, point3);
            }
            else if ((point3 - point1).Normalize().IsAlmostEqualTo((point3 - point4).Normalize()))
            {
                out_C1 = Line.CreateBound(point1, point4);
                out_C2 = Line.CreateBound(point3, point2);
            }
            else if ((point3 - point1).Normalize() != ((point3 - point4).Normalize()))
            {
                out_C1 = Line.CreateBound(point1, point3);
                out_C2 = Line.CreateBound(point4, point2);
            }
            List<Curve> list_result = new List<Curve>();
            if (out_C1 != null)
            {
                list_result.Add(out_C1);
            }
            if (out_C2 != null)
            {
                list_result.Add(out_C2);
            }   
            return list_result;
        }
        public List<Curve> Sort_Curve_1(List<Curve> profile)
        {
            List<XYZ> list_point = new List<XYZ>();
            List<Curve> list_result = new List<Curve>();
            XYZ point1 = profile[0].GetEndPoint(0);
            XYZ point2 = profile[0].GetEndPoint(1);
            profile.Remove(profile[0]);
            int len = profile.Count;
            list_point.Add(point1);

            for (int i = 0; i < len; i++)
            {
                foreach (var item in profile)
                {
                    XYZ p1 = item.GetEndPoint(0);
                    XYZ p2 = item.GetEndPoint(1);
                    if (p1.IsAlmostEqualTo(point2))
                    {
                        list_point.Add(point2);
                        point1 = item.GetEndPoint(0);
                        point2 = item.GetEndPoint(1);
                        profile.Remove(item);                       
                        break;
                    }
                    else if (p2.IsAlmostEqualTo(point2))
                    {
                        list_point.Add(point2);
                        point1 = item.GetEndPoint(1);
                        point2 = item.GetEndPoint(0);
                        profile.Remove(item);
                        break;
                    }
                    
                }                
            }
            for (int i = 0; i < list_point.Count; i++)
            {
                if (i == list_point.Count - 1)
                {
                    list_result.Add(Line.CreateBound(list_point[i], list_point[0]));
                }
                else
                {
                    list_result.Add(Line.CreateBound(list_point[i], list_point[i + 1]));
                }
            }
            return list_result;
        }
        public List<Curve> Sort_Curve_2(List<Curve> profile)
        {
            List<XYZ> list_point = new List<XYZ>();
            List<Curve> list_result = new List<Curve>();

            XYZ point1 = profile[0].GetEndPoint(0);
            XYZ point2 = profile[0].GetEndPoint(1);
            profile.Remove(profile[0]);
            int len = profile.Count;
            list_point.Add(point1);

            for (int i = 0; i < len; i++)
            {
                foreach (var item in profile)
                {
                    XYZ p1 = item.GetEndPoint(0);
                    XYZ p2 = item.GetEndPoint(1);
                    if (p1.IsAlmostEqualTo(point2))
                    {
                        list_point.Add(point2);
                        point1 = item.GetEndPoint(0);
                        point2 = item.GetEndPoint(1);
                        profile.Remove(item);
                        break;
                    }
                    else if (p2.IsAlmostEqualTo(point2))
                    {
                        list_point.Add(point2);
                        point1 = item.GetEndPoint(1);
                        point2 = item.GetEndPoint(0);
                        profile.Remove(item);
                        break;
                    }

                }
            }
            for (int i = 0; i < list_point.Count; i++)
            {
                if (i == list_point.Count - 1)
                {
                    list_result.Add(Line.CreateBound(list_point[i], list_point[0]));
                }
                else
                {
                    list_result.Add(Line.CreateBound(list_point[i], list_point[i + 1]));
                }
            }
            return list_result;
        }
        public XYZ GetIntersec_2Line(XYZ point1, XYZ point2, XYZ point3, XYZ point4)
        {


            XYZ n1 = (point2 - point1).Normalize();
            XYZ n2 = (point4 - point3).Normalize();

            XYZ u1 = new XYZ(n1.Y, -n1.X, 0);
            XYZ u2 = new XYZ(n2.Y, -n2.X, 0);
            double y,x;
            if (Math.Round(u1.X,5) == 0)
            {
                y = point1.Y;
                x = (-u2.Y * y + u2.Y * point3.Y + point3.X * u2.X) / u2.X; 
            }
            else if (Math.Round(u1.Y, 5) == 0)
            {
                x = point1.X;
                y = (-u2.X * x + u2.X * point3.X + u2.Y * point3.Y) / u2.Y;
            }
            else
            {
                y = (u1.X * u2.X * point1.X + u2.X * u1.Y * point1.Y - u1.X * u2.X * point3.X - u1.X * u2.Y * point3.Y) / (-u2.Y * u1.X + u1.Y * u2.X);
                x = (u1.X * point1.X + u1.Y * point1.Y - u1.Y * y) / u1.X;
            }
            
            return new XYZ(x, y, 0);
        }
        public XYZ GetIntersec_3Point(XYZ point1, XYZ point2, XYZ point3)
        {
            XYZ n1 = (point2 - point1).Normalize();
            XYZ n2 = n1.CrossProduct(XYZ.BasisZ).Normalize();

            XYZ u1 = new XYZ(n1.Y, -n1.X, 0);
            XYZ u2 = new XYZ(n2.Y, -n2.X, 0);
            double y, x;
            if (Math.Round(u1.X, 5) == 0)
            {
                y = point1.Y;
                x = (-u2.Y * y + u2.Y * point3.Y + point3.X * u2.X) / u2.X;
            }
            else if (Math.Round(u1.Y, 5) == 0)
            {
                x = point1.X;
                y = (-u2.X * x + u2.X * point3.X + u2.Y * point3.Y) / u2.Y;
            }
            else
            {
                y = (u1.X * u2.X * point1.X + u2.X * u1.Y * point1.Y - u1.X * u2.X * point3.X - u1.X * u2.Y * point3.Y) / (-u2.Y * u1.X + u1.Y * u2.X);
                x = (u1.X * point1.X + u1.Y * point1.Y - u1.Y * y) / u1.X;
            }
            double z = point1.Z;
            return new XYZ(x, y, z);
        }
        public IList<Solid> GetTargetSolids(Element element)
        {
            List<Solid> solids = new List<Solid>();
            Options options = new Options();
            options.DetailLevel = ViewDetailLevel.Fine;
            GeometryElement geomElem = element.get_Geometry(options);
            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is Solid)
                {
                    Solid solid = (Solid)geomObj;
                    if (solid.Faces.Size > 0 && solid.Volume > 0.0)
                    {
                        solids.Add(solid);
                    }
                    // Single-level recursive check of instances. If viable solids are more than
                    // one level deep, this example ignores them.
                }
                else if (geomObj is GeometryInstance)
                {
                    GeometryInstance geomInst = (GeometryInstance)geomObj;
                    GeometryElement instGeomElem = geomInst.GetInstanceGeometry();
                    foreach (GeometryObject instGeomObj in instGeomElem)
                    {
                        if (instGeomObj is Solid)
                        {
                            Solid solid = (Solid)instGeomObj;
                            if (solid.Faces.Size > 0 && solid.Volume > 0.0)
                            {
                                solids.Add(solid);
                            }
                        }
                    }
                }
            }
            return solids;
        }
        public FamilyInstance CreateFamilyInstaceFromSolid(Document doc, Solid solid, string solidname, Element ele_pick)
        {
            if (solid == null) return null;
            FamilyInstance result = null;
            string[] acceptableViews = new string[] { "ThreeD", "FloorPlan", "EngineeringPlan", "CeilingPlan", "Elevation", "Section" };
            var origin = XYZ.Zero;
            var tempPath = Path.GetTempPath();
            var familyPath = SingleData.Singleton.Instance.RevitData.Application.FamilyTemplatePath + @"\English-Imperial";
            var filePath = Path.Combine(familyPath, "Generic Model.rft");
            var satOpt = new SATImportOptions();
            satOpt.Placement = ImportPlacement.Origin;
            satOpt.Unit = ImportUnit.Foot;
            var satName = solidname + ".sat";
            var rfaName = solidname + ".rfa";
            var opt1 = new Options();
            opt1.ComputeReferences = true;
            var saveAsOpt = new SaveAsOptions();
            saveAsOpt.OverwriteExistingFile = true;
            Family family = null;
            try
            {
                var familyDoc = doc.Application.NewFamilyDocument(filePath);

                var satPathExport = ExportToSat(familyDoc, solid, satName);
                var viewFEC = new FilteredElementCollector(familyDoc).OfClass(typeof(View)).ToElements();
                View view1 = null;

                foreach (Element e in viewFEC)
                {
                    if (e is View v && acceptableViews.Contains(v.ViewType.ToString()) && v.IsTemplate == false)
                    {
                        view1 = v;
                        break;
                    }
                }

                using (var transactiongroup = new TransactionGroup(familyDoc, "Family"))
                {
                    transactiongroup.Start();
                    using (var t = new Transaction(familyDoc, "Modify Family"))
                    {
                        t.Start();
                        var satId = familyDoc.Import(satPathExport, satOpt, view1);
                        var ele1 = familyDoc.GetElement(satId);
                        var geom1 = ele1.get_Geometry(opt1);
                        IEnumerator<GeometryObject> enumerator = geom1.GetEnumerator();
                        enumerator.MoveNext();
                        GeometryElement geom2 = (enumerator.Current as GeometryInstance)?.GetInstanceGeometry();
                        IEnumerator<GeometryObject> enum2 = null;
                        if (geom2 != null)
                        {
                            enum2 = geom2.GetEnumerator();
                        }
                        if (enum2 != null) enum2.MoveNext();
                        if (enum2 != null)
                        {
                            solid = (Solid)enum2.Current;
                            familyDoc.Delete(satId);
                            File.Delete(satPathExport);

                            try
                            {
                                var famCat = familyDoc.Settings.Categories.get_Item(BuiltInCategory.OST_GenericModel);
                                familyDoc.OwnerFamily.FamilyCategory = famCat;
                            }
                            catch (Exception)
                            {
                            }
                        }

                        t.Commit();
                    }
                    transactiongroup.Commit();
                }
                var saveFamilyPath = Path.Combine(tempPath, rfaName);
                familyDoc.SaveAs(saveFamilyPath, saveAsOpt);
                doc.LoadFamily(saveFamilyPath, out family);
                familyDoc.Close(false);
                File.Delete(saveFamilyPath);
                var symbols = family.GetFamilySymbolIds().GetEnumerator();
                symbols.MoveNext();
                FamilySymbol familySymbol = doc.GetElement(symbols.Current) as FamilySymbol;

                //using (var t = new Transaction(doc, "Place Family"))
                //{
                //    t.Start();
                    if (familySymbol != null && familySymbol.IsActive == false)
                    {
                        familySymbol.Activate();
                    }

                    result = doc.Create.NewFamilyInstance(origin, familySymbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                    if (ele_pick.Location is LocationPoint)
                    {
                        LocationPoint location = ele_pick.Location as LocationPoint;
                        if (location != null) origin = location.Point;
                    }
                    else if (ele_pick.Location is LocationCurve)
                    {
                        LocationCurve location = ele_pick.Location as LocationCurve;
                        if (location != null)
                        {
                            Line cur = location.Curve as Line;
                            if (cur != null) origin = cur.Origin;
                        }
                    }


                //    t.Commit();
                //}
            }
            catch (Exception )
            {

            }
            return result;
        }
        public string ExportToSat(Document doc, Solid solid, string name)
        {
            string path = string.Empty;
            try
            {
                var viewFamilyType = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewFamilyType))
                    .OfType<ViewFamilyType>()
                    .FirstOrDefault(x => x.ViewFamily == ViewFamily.ThreeDimensional);
                var tempPath = Path.GetTempPath();
                path = Path.Combine(tempPath, name);
                View3D threeDView = null;
                using (var t = new Transaction(doc, "Create 3D View"))
                {
                    t.Start();
                    if (viewFamilyType != null) threeDView = View3D.CreateIsometric(doc, viewFamilyType.Id);
                    t.Commit();
                }
                using (Transaction tx = new Transaction(doc, "Create Element Soild"))
                {
                    tx.Start();
                    FreeFormElement.Create(doc, solid);
                    tx.Commit();
                }

                if (threeDView != null)
                {
                    var viewSet = new List<ElementId>()
                    {
                        threeDView.Id
                    };
                    SATExportOptions exportOptions = new SATExportOptions();
                    doc.Export(tempPath, name, viewSet, exportOptions);
                }
            }
            catch (Exception)
            {

            }
            return path;
        }
    }
}
