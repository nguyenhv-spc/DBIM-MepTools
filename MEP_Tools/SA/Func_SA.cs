using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools
{
    class Func_SA
    {      
        public string CheckAVoid(Face face, List<Pipe> List_Pipeinput)
        {
            string result = "";
            foreach (Pipe item in List_Pipeinput)
            {
                Curve PipeCurve = (item.Location as LocationCurve).Curve;
                double radius = item.Diameter / 2;

                Line l1 = null, l2 = null;

                if ((PipeCurve.GetEndPoint(0).X > PipeCurve.GetEndPoint(1).X && PipeCurve.GetEndPoint(0).Y > PipeCurve.GetEndPoint(1).Y)
                        || (PipeCurve.GetEndPoint(1).X > PipeCurve.GetEndPoint(0).X && PipeCurve.GetEndPoint(1).Y > PipeCurve.GetEndPoint(0).Y))
                {
                    l1 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X - radius, PipeCurve.GetEndPoint(0).Y + radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X - radius, PipeCurve.GetEndPoint(1).Y + radius, PipeCurve.GetEndPoint(1).Z));
                    l2 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X + radius, PipeCurve.GetEndPoint(0).Y - radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X + radius, PipeCurve.GetEndPoint(1).Y - radius, PipeCurve.GetEndPoint(1).Z));
                }
                else if ((PipeCurve.GetEndPoint(0).X > PipeCurve.GetEndPoint(1).X && PipeCurve.GetEndPoint(1).Y > PipeCurve.GetEndPoint(0).Y)
                        || (PipeCurve.GetEndPoint(1).X > PipeCurve.GetEndPoint(0).X && PipeCurve.GetEndPoint(0).Y > PipeCurve.GetEndPoint(1).Y))
                {
                    l1 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X - radius, PipeCurve.GetEndPoint(0).Y - radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X - radius, PipeCurve.GetEndPoint(1).Y - radius, PipeCurve.GetEndPoint(1).Z));
                    l2 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X + radius, PipeCurve.GetEndPoint(0).Y + radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X + radius, PipeCurve.GetEndPoint(1).Y + radius, PipeCurve.GetEndPoint(1).Z));
                }
                else
                {
                    l1 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X - radius, PipeCurve.GetEndPoint(0).Y + radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X - radius, PipeCurve.GetEndPoint(1).Y + radius, PipeCurve.GetEndPoint(1).Z));
                    l2 = Line.CreateBound(new XYZ(PipeCurve.GetEndPoint(0).X + radius, PipeCurve.GetEndPoint(0).Y - radius, PipeCurve.GetEndPoint(0).Z), new XYZ(PipeCurve.GetEndPoint(1).X + radius, PipeCurve.GetEndPoint(1).Y - radius, PipeCurve.GetEndPoint(1).Z));
                }

               
                if (face.Intersect(l1) == SetComparisonResult.Overlap)
                {
                    result = "Cham";
                }
                else if (face.Intersect(l2) == SetComparisonResult.Overlap)
                {
                    result = "Cham";
                }
            }
            return result;
        }
        public void AddShaft_Circle(XYZ xyAxis, XYZ profile, XYZ center, XYZ normal, Curve location, double radius, Wall wall_select)
        {
                if (Math.Round(xyAxis.X, 5) == 0)
                {
                    XYZ corner1 = new XYZ(profile.X, center.Y - xyAxis.Y * radius - xyAxis.Y * cls_SA.Offset_W, center.Z - radius - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(profile.X, center.Y + xyAxis.Y * radius + xyAxis.Y * cls_SA.Offset_W, center.Z + radius + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else if (Math.Round(xyAxis.Y, 5) == 0)
                {
                    XYZ corner1 = new XYZ(center.X - xyAxis.X * radius - xyAxis.X * cls_SA.Offset_W, profile.Y, center.Z - radius - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(center.X + xyAxis.X * radius + xyAxis.X * cls_SA.Offset_W, profile.Y, center.Z + radius + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else
                {
                    double tu = normal.X * normal.X * center.Y - normal.X * normal.Y * center.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau = (normal.X * normal.X + normal.Y * normal.Y);

                    double y = tu / mau;
                    double x = ((normal.X * (y - center.Y)) / normal.Y) + center.X;

                    XYZ Vector1 = xyAxis;
                    XYZ Vector2 = XYZ.BasisX;

                    double tugoc = Vector1.X * Vector2.X + Vector1.Y * Vector2.Y;
                    double maugoc = Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y);
                    double Goc = Math.Acos(Math.Abs(tugoc / maugoc));
                    XYZ corner1 = null, corner3 = null;
                    if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * radius - Math.Cos(Goc) * cls_SA.Offset_W, y - Math.Sin(Goc) * radius - Math.Cos(Goc) * cls_SA.Offset_W, center.Z - radius - cls_SA.Offset_H);
                        corner3 = new XYZ(x + Math.Cos(Goc) * radius + Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * radius + Math.Cos(Goc) * cls_SA.Offset_W, center.Z + radius + cls_SA.Offset_H);
                    }
                    else if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * radius - Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * radius + Math.Cos(Goc) * cls_SA.Offset_W, center.Z - radius - cls_SA.Offset_H);
                        corner3 = new XYZ(x + Math.Cos(Goc) * radius + Math.Cos(Goc) * cls_SA.Offset_W, y - Math.Sin(Goc) * radius - Math.Cos(Goc) * cls_SA.Offset_W, center.Z + radius + cls_SA.Offset_H);
                    }
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
        }
        public void AddShaft_Circles(XYZ xyAxis, XYZ profile, XYZ center1, XYZ center2, XYZ normal, Curve location, double radius1, double radius2,double h, Wall wall_select)
        {           
                if (Math.Round(xyAxis.X, 5) == 0)
                {
                    XYZ corner1 = new XYZ(profile.X, center1.Y + xyAxis.Y * radius1 + xyAxis.Y * cls_SA.Offset_W, center1.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(profile.X, center2.Y - xyAxis.Y * radius2 - xyAxis.Y * cls_SA.Offset_W, center2.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else if (Math.Round(xyAxis.Y, 5) == 0)
                {
                    XYZ corner1 = new XYZ(center1.X + xyAxis.X * radius1 + xyAxis.X * cls_SA.Offset_W, profile.Y, center1.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(center2.X - xyAxis.X * radius2 - xyAxis.X * cls_SA.Offset_W, profile.Y, center2.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else
                {
                    double tu = normal.X * normal.X * center1.Y - normal.X * normal.Y * center1.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau = (normal.X * normal.X + normal.Y * normal.Y);

                    double y = tu / mau;
                    double x = ((normal.X * (y - center1.Y)) / normal.Y) + center1.X;


                    double tu2 = normal.X * normal.X * center2.Y - normal.X * normal.Y * center2.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau2 = (normal.X * normal.X + normal.Y * normal.Y);

                    double y2 = tu / mau;
                    double x2 = ((normal.X * (y - center2.Y)) / normal.Y) + center2.X;

                    XYZ Vector1 = xyAxis;
                    XYZ Vector2 = XYZ.BasisX;

                    double tugoc = Vector1.X * Vector2.X + Vector1.Y * Vector2.Y;
                    double maugoc = Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y);
                    double Goc = Math.Acos(Math.Abs(tugoc / maugoc));


                    XYZ corner1 = null, corner3 = null;
                    if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y))
                    {
                        corner1 = new XYZ(x + Math.Cos(Goc) * radius1 + Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * radius1 + Math.Cos(Goc) * cls_SA.Offset_W, center1.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x2 - Math.Cos(Goc) * radius2 - Math.Cos(Goc) * cls_SA.Offset_W, y2 - Math.Sin(Goc) * radius2 - Math.Cos(Goc) * cls_SA.Offset_W, center2.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    else if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * radius1 - Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * radius1 + Math.Cos(Goc) * cls_SA.Offset_W, center1.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x2 + Math.Cos(Goc) * radius2 + Math.Cos(Goc) * cls_SA.Offset_W, y2 - Math.Sin(Goc) * radius2 - Math.Cos(Goc) * cls_SA.Offset_W, center2.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }   
            
        }
        public void AddShaft_Rec(XYZ xyAxis, XYZ profile, XYZ center, XYZ normal, Curve location, double h, double w, Wall wall_select)
        {
            
                if (Math.Round(xyAxis.X, 5) == 0)
                {
                    XYZ corner1 = new XYZ(profile.X, center.Y - xyAxis.Y * w - xyAxis.Y * cls_SA.Offset_W, center.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(profile.X, center.Y + xyAxis.Y * w + xyAxis.Y * cls_SA.Offset_W, center.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else if (Math.Round(xyAxis.Y, 5) == 0)
                {
                    XYZ corner1 = new XYZ(center.X - xyAxis.X * w - xyAxis.X * cls_SA.Offset_W, profile.Y, center.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(center.X + xyAxis.X * w + xyAxis.X * cls_SA.Offset_W, profile.Y, center.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else
                {
                    double tu = normal.X * normal.X * center.Y - normal.X * normal.Y * center.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau = (normal.X * normal.X + normal.Y * normal.Y);

                    double y = tu / mau;
                    double x = ((normal.X * (y - center.Y)) / normal.Y) + center.X;

                    XYZ Vector1 = xyAxis;
                    XYZ Vector2 = XYZ.BasisX;

                    double tugoc = Vector1.X * Vector2.X + Vector1.Y * Vector2.Y;
                    double maugoc = Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y);
                    double Goc = Math.Acos(Math.Abs(tugoc / maugoc));
                    XYZ corner1 = null, corner3 = null;
                    if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * w - Math.Cos(Goc) * cls_SA.Offset_W, y - Math.Sin(Goc) * w - Math.Cos(Goc) * cls_SA.Offset_W, center.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x + Math.Cos(Goc) * w + Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * w + Math.Cos(Goc) * cls_SA.Offset_W, center.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    else if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * w - Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * w + Math.Cos(Goc) * cls_SA.Offset_W, center.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x + Math.Cos(Goc) * w + Math.Cos(Goc) * cls_SA.Offset_W, y - Math.Sin(Goc) * w - Math.Cos(Goc) * cls_SA.Offset_W, center.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }        
        }
        public void AddShaft_Recs(XYZ xyAxis, XYZ profile, XYZ center1, XYZ center2, XYZ normal, Curve location, double h1, double w1, double h2, double w2,double h, Wall wall_select)
        {
            if (Math.Round(xyAxis.X, 5) == 0)
                {
                    XYZ corner1 = new XYZ(profile.X, center1.Y + xyAxis.Y * w1 + xyAxis.Y * cls_SA.Offset_W, center1.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(profile.X, center2.Y - xyAxis.Y * w2 - xyAxis.Y * cls_SA.Offset_W, center2.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else if (Math.Round(xyAxis.Y, 5) == 0)
                {
                    XYZ corner1 = new XYZ(center1.X + xyAxis.X * w1 + xyAxis.X * cls_SA.Offset_W, profile.Y, center1.Z - h - cls_SA.Offset_H);
                    XYZ corner3 = new XYZ(center2.X - xyAxis.X * w2 - xyAxis.X * cls_SA.Offset_W, profile.Y, center2.Z + h + cls_SA.Offset_H);
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }
                else
                {
                    double tu = normal.X * normal.X * center1.Y - normal.X * normal.Y * center1.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau = (normal.X * normal.X + normal.Y * normal.Y);

                    double y = tu / mau;
                    double x = ((normal.X * (y - center1.Y)) / normal.Y) + center1.X;

                    double tu2 = normal.X * normal.X * center2.Y - normal.X * normal.Y * center2.X + normal.X * normal.Y * location.GetEndPoint(0).X + normal.Y * normal.Y * location.GetEndPoint(0).Y;
                    double mau2 = (normal.X * normal.X + normal.Y * normal.Y);

                    double y2 = tu / mau;
                    double x2 = ((normal.X * (y - center2.Y)) / normal.Y) + center2.X;

                    XYZ Vector1 = xyAxis;
                    XYZ Vector2 = XYZ.BasisX;

                    double tugoc = Vector1.X * Vector2.X + Vector1.Y * Vector2.Y;
                    double maugoc = Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y);
                    double Goc = Math.Acos(Math.Abs(tugoc / maugoc));

                    XYZ corner1 = null, corner3 = null;
                    if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * w1 - Math.Cos(Goc) * cls_SA.Offset_W, y - Math.Sin(Goc) * w1 - Math.Cos(Goc) * cls_SA.Offset_W, center1.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x2 + Math.Cos(Goc) * w2 + Math.Cos(Goc) * cls_SA.Offset_W, y2 + Math.Sin(Goc) * w2 + Math.Cos(Goc) * cls_SA.Offset_W, center2.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    else if ((location.GetEndPoint(0).X > location.GetEndPoint(1).X && location.GetEndPoint(1).Y > location.GetEndPoint(0).Y)
                    || (location.GetEndPoint(1).X > location.GetEndPoint(0).X && location.GetEndPoint(0).Y > location.GetEndPoint(1).Y))
                    {
                        corner1 = new XYZ(x - Math.Cos(Goc) * w1 - Math.Cos(Goc) * cls_SA.Offset_W, y + Math.Sin(Goc) * w1 + Math.Cos(Goc) * cls_SA.Offset_W, center1.Z - h - Math.Cos(Goc) * cls_SA.Offset_H);
                        corner3 = new XYZ(x2 + Math.Cos(Goc) * w2 + Math.Cos(Goc) * cls_SA.Offset_W, y2 - Math.Sin(Goc) * w2 - Math.Cos(Goc) * cls_SA.Offset_W, center2.Z + h + Math.Cos(Goc) * cls_SA.Offset_H);
                    }
                    SingleData.Singleton.Instance.RevitData.Document.Create.NewOpening(wall_select, corner1, corner3);
                }             
        }
        public Element FindElementOutermost(List<Element> List_InputEle, List<XYZ> List_InputXYZ, XYZ Outermost)
        {
            Element Ele = null;
            if (List_InputEle.Count > 0)
            {
                double min = List_InputXYZ[0].DistanceTo(Outermost);
                for (int i = 0; i < List_InputEle.Count; i++)
                {
                    double kc = List_InputXYZ[i].DistanceTo(Outermost);
                    if (kc <= min)
                    {
                        min = kc;
                        Ele = List_InputEle[i];
                    }
                }
            }
            return Ele;
        }
        public List<Element> SortElement(List<Element> List_InputEle, List<XYZ> List_InputXYZ, Element ele)
        {
            List<Element> Lists_Ele = new List<Element>();
            List<XYZ> Lists_XYZ = new List<XYZ>();
            foreach (var item in List_InputEle)
            {
                Lists_Ele.Add(item);
            }
            foreach (var item in List_InputXYZ)
            {
                Lists_XYZ.Add(item);
            }
            XYZ Intersec_ele = null;
            for (int i = 0; i < List_InputEle.Count; i++)
            {
                if (List_InputEle[i].Id == ele.Id)
                {
                    Intersec_ele = List_InputXYZ[i];
                    break;
                }   
            }
            Lists_Ele.Remove(ele);
            Lists_XYZ.Remove(Intersec_ele);

            List<double> Lists_Distance = new List<double>();
            foreach (var item in Lists_XYZ)
            {
                Lists_Distance.Add(Intersec_ele.DistanceTo(item));
            }

            for (int i = 0; i < Lists_XYZ.Count; i++)
            {               
                for (int j = i + 1; j < Lists_XYZ.Count; j++)   
                {
                    double kc1 = Intersec_ele.DistanceTo(Lists_XYZ[i]);
                    double kc2 = Intersec_ele.DistanceTo(Lists_XYZ[j]);
                    if (kc1 > kc2)
                    {
                        var temp1 = Lists_XYZ[i];
                        Lists_XYZ[i] = Lists_XYZ[j];
                        Lists_XYZ[j] = temp1;

                        var temp2 = Lists_Ele[i];
                        Lists_Ele[i] = Lists_Ele[j];
                        Lists_Ele[j] = temp2;
                    }                  
                }   
            }

            List<Element> result = new List<Element>();
            result.Add(ele);
            for (int i = 0; i < Lists_Ele.Count; i++)
            {
                result.Add(Lists_Ele[i]);
            }

            return result;
        }
        public List<XYZ> SortXYZ(List<Element> List_InputEle, List<XYZ> List_InputXYZ, Element ele)
        {
            List<Element> Lists_Ele = new List<Element>();
            List<XYZ> Lists_XYZ = new List<XYZ>();
            foreach (var item in List_InputEle)
            {
                Lists_Ele.Add(item);
            }
            foreach (var item in List_InputXYZ)
            {
                Lists_XYZ.Add(item);
            }
            XYZ Intersec_ele = null;
            for (int i = 0; i < List_InputEle.Count; i++)
            {
                if (List_InputEle[i].Id == ele.Id)
                {
                    Intersec_ele = List_InputXYZ[i];
                    break;
                }
            }
            Lists_Ele.Remove(ele);
            Lists_XYZ.Remove(Intersec_ele);

            List<double> Lists_Distance = new List<double>();
            foreach (var item in Lists_XYZ)
            {
                Lists_Distance.Add(Intersec_ele.DistanceTo(item));
            }

            for (int i = 0; i < Lists_XYZ.Count; i++)
            {
                for (int j = i + 1; j < Lists_XYZ.Count; j++)
                {
                    double kc1 = Intersec_ele.DistanceTo(Lists_XYZ[i]);
                    double kc2 = Intersec_ele.DistanceTo(Lists_XYZ[j]);
                    if (kc1 > kc2)
                    {
                        var temp1 = Lists_XYZ[i];
                        Lists_XYZ[i] = Lists_XYZ[j];
                        Lists_XYZ[j] = temp1;

                        var temp2 = Lists_Ele[i];
                        Lists_Ele[i] = Lists_Ele[j];
                        Lists_Ele[j] = temp2;
                    }
                }
            }

            List<XYZ> result = new List<XYZ>();
            result.Add(Intersec_ele);
            for (int i = 0; i < Lists_XYZ.Count; i++)
            {
                result.Add(Lists_XYZ[i]);
            }

            return result;
        }
        public List<List<Pipe>> DividePipe(List<Element> SortElement, List<XYZ> SortXYZ)
        {
            List<Pipe> List_Input = new List<Pipe>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Input.Add(SortElement[i] as Pipe);
            }
            List<XYZ> List_XYZ = new List<XYZ>();
            for (int i = 0; i < SortXYZ.Count; i++)
            {
                List_XYZ.Add(SortXYZ[i]);
            }


            List<Pipe> List_Result = new List<Pipe>();
            List<XYZ> List_Result_XYZ = new List<XYZ>();
            List<List<Pipe>> List_List_Result = new List<List<Pipe>>();
            do
            {
                List_Result.Clear();
                for (int i = 0; i < List_Input.Count - 1; i++)
                {
                    string DK = "";
                    if (List_XYZ[i].DistanceTo(List_XYZ[i + 1]) <= (List_Input[i].Diameter * 1.5 + List_Input[i + 1].Diameter * 1.5) / 2)
                    {
                        DK = "TM";
                    }
                    else
                    {
                        DK = "KTM";
                    }
                    if (DK == "TM")
                    {
                        List_Result.Add(List_Input[i]);
                        List_Result_XYZ.Add(List_XYZ[i]);
                    }
                    else if (DK == "KTM")
                    {
                        List_Result.Add(List_Input[i]);
                        List_Result_XYZ.Add(List_XYZ[i]);
                        break;
                    }
                }
                List<Pipe> Tg = new List<Pipe>();
                foreach (var item in List_Result)
                {
                    Tg.Add(item);
                }
                List_List_Result.Add(Tg);
                foreach (var item in List_Result)
                {
                    List_Input.Remove(item);
                }
                foreach (var item in List_Result_XYZ)
                {
                    List_XYZ.Remove(item);
                }
            } while (List_Input.Count > 1);
            List<Pipe> List_Inputs = new List<Pipe>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Inputs.Add(SortElement[i] as Pipe);
            }
            if (SortXYZ[SortXYZ.Count - 1].DistanceTo(SortXYZ[SortXYZ.Count - 2]) <= (List_Inputs[SortXYZ.Count - 1].Diameter * 1.5 + List_Inputs[SortXYZ.Count - 2].Diameter * 1.5) / 2)
            {
                List_List_Result[List_List_Result.Count - 1].Add(List_Inputs[SortXYZ.Count - 1]);
            }
            else
            {
                List_Result.Clear();
                List_Result.Add(List_Inputs[SortXYZ.Count - 1]);
                List_List_Result.Add(List_Result);
            }    
            return List_List_Result;
        }
        public List<List<Duct>> DivideDuct(List<Element> SortElement, List<XYZ> SortXYZ)
        {
            List<Duct> List_Input = new List<Duct>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Input.Add(SortElement[i] as Duct);
            }
            List<XYZ> List_XYZ = new List<XYZ>();
            for (int i = 0; i < SortXYZ.Count; i++)
            {
                List_XYZ.Add(SortXYZ[i]);
            }


            List<Duct> List_Result = new List<Duct>();
            List<XYZ> List_Result_XYZ = new List<XYZ>();
            List<List<Duct>> List_List_Result = new List<List<Duct>>();
            do
            {
                List_Result.Clear();
                try
                {
                    for (int i = 0; i < List_Input.Count - 1; i++)
                    {
                        string DK = "";
                        if (List_XYZ[i].DistanceTo(List_XYZ[i + 1]) <= (List_Input[i].Diameter * 1.5 + List_Input[i + 1].Diameter * 1.5) / 2)
                        {
                            DK = "TM";
                        }
                        else
                        {
                            DK = "KTM";
                        }
                        if (DK == "TM")
                        {
                            List_Result.Add(List_Input[i]);
                            List_Result_XYZ.Add(List_XYZ[i]);
                        }
                        else if (DK == "KTM")
                        {
                            List_Result.Add(List_Input[i]);
                            List_Result_XYZ.Add(List_XYZ[i]);
                            break;
                        }
                    }
                }
                catch
                {
                    for (int i = 0; i < List_Input.Count - 1; i++)
                    {
                        string DK = "";
                        if (List_XYZ[i].DistanceTo(List_XYZ[i + 1]) <= (List_Input[i].Width * 1.5 + List_Input[i + 1].Width * 1.5) / 2)
                        {
                            DK = "TM";
                        }
                        else
                        {
                            DK = "KTM";
                        }
                        if (DK == "TM")
                        {
                            List_Result.Add(List_Input[i]);
                            List_Result_XYZ.Add(List_XYZ[i]);
                        }
                        else if (DK == "KTM")
                        {
                            List_Result.Add(List_Input[i]);
                            List_Result_XYZ.Add(List_XYZ[i]);
                            break;
                        }
                    }
                }
                
                List<Duct> Tg = new List<Duct>();
                foreach (var item in List_Result)
                {
                    Tg.Add(item);
                }
                List_List_Result.Add(Tg);
                foreach (var item in List_Result)
                {
                    List_Input.Remove(item);
                }
                foreach (var item in List_Result_XYZ)
                {
                    List_XYZ.Remove(item);
                }
            } while (List_Input.Count > 1);
            List<Duct> List_Inputs = new List<Duct>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Inputs.Add(SortElement[i] as Duct);
            }
            try
            {
                if (SortXYZ[SortXYZ.Count - 1].DistanceTo(SortXYZ[SortXYZ.Count - 2]) <= (List_Inputs[SortXYZ.Count - 1].Diameter * 1.5 + List_Inputs[SortXYZ.Count - 2].Diameter * 1.5) / 2)
                {
                    List_List_Result[List_List_Result.Count - 1].Add(List_Inputs[SortXYZ.Count - 1]);
                }
                else
                {
                    List_Result.Clear();
                    List_Result.Add(List_Inputs[SortXYZ.Count - 1]);
                    List_List_Result.Add(List_Result);
                }
            }
            catch
            {
                if (SortXYZ[SortXYZ.Count - 1].DistanceTo(SortXYZ[SortXYZ.Count - 2]) <= (List_Inputs[SortXYZ.Count - 1].Width * 1.5 + List_Inputs[SortXYZ.Count - 2].Width * 1.5) / 2)
                {
                    List_List_Result[List_List_Result.Count - 1].Add(List_Inputs[SortXYZ.Count - 1]);
                }
                else
                {
                    List_Result.Clear();
                    List_Result.Add(List_Inputs[SortXYZ.Count - 1]);
                    List_List_Result.Add(List_Result);
                }
            }                       
            return List_List_Result;
        }
        public List<List<CableTray>> DivideCabTray(List<Element> SortElement, List<XYZ> SortXYZ)
        {
            List<CableTray> List_Input = new List<CableTray>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Input.Add(SortElement[i] as CableTray);
            }
            List<XYZ> List_XYZ = new List<XYZ>();
            for (int i = 0; i < SortXYZ.Count; i++)
            {
                List_XYZ.Add(SortXYZ[i]);
            }


            List<CableTray> List_Result = new List<CableTray>();
            List<XYZ> List_Result_XYZ = new List<XYZ>();
            List<List<CableTray>> List_List_Result = new List<List<CableTray>>();
            do
            {
                List_Result.Clear();
                for (int i = 0; i < List_Input.Count - 1; i++)
                {
                    string DK = "";
                    if (List_XYZ[i].DistanceTo(List_XYZ[i + 1]) <= (List_Input[i].Width * 1.5 + List_Input[i + 1].Width * 1.5) / 2)
                    {
                        DK = "TM";
                    }
                    else
                    {
                        DK = "KTM";
                    }
                    if (DK == "TM")
                    {
                        List_Result.Add(List_Input[i]);
                        List_Result_XYZ.Add(List_XYZ[i]);
                    }
                    else if (DK == "KTM")
                    {
                        List_Result.Add(List_Input[i]);
                        List_Result_XYZ.Add(List_XYZ[i]);
                        break;
                    }
                }
                List<CableTray> Tg = new List<CableTray>();
                foreach (var item in List_Result)
                {
                    Tg.Add(item);
                }
                List_List_Result.Add(Tg);
                foreach (var item in List_Result)
                {
                    List_Input.Remove(item);
                }
                foreach (var item in List_Result_XYZ)
                {
                    List_XYZ.Remove(item);
                }
            } while (List_Input.Count > 1);
            List<CableTray> List_Inputs = new List<CableTray>();
            for (int i = 0; i < SortElement.Count; i++)
            {
                List_Inputs.Add(SortElement[i] as CableTray);
            }
            if (SortXYZ[SortXYZ.Count - 1].DistanceTo(SortXYZ[SortXYZ.Count - 2]) <= (List_Inputs[SortXYZ.Count - 1].Width * 1.5 + List_Inputs[SortXYZ.Count - 2].Width * 1.5) / 2)
            {
                List_List_Result[List_List_Result.Count - 1].Add(List_Inputs[SortXYZ.Count - 1]);
            }
            else
            {
                List_Result.Clear();
                List_Result.Add(List_Inputs[SortXYZ.Count - 1]);
                List_List_Result.Add(List_Result);
            }
            return List_List_Result;
        }

        
    }
}
