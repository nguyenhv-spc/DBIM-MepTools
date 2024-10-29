using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools.ThoatNuoc
{
    public class Func_ThoatNuoc
    {
        public Pipe GetElement1(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double max = doc.GetElement(Input_ref[0]).LookupParameter("Top Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Top Elevation").AsDouble();
                if (elevation > max)
                {
                    max = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Top Elevation").AsDouble();
                if (elevation == max)
                {
                    result = pipe;
                }
            }
            return result;
        }
        public Pipe GetElement2(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double max = doc.GetElement(Input_ref[0]).LookupParameter("Top Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Top Elevation").AsDouble();
                if (elevation > max)
                {
                    max = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Top Elevation").AsDouble();
                if (elevation == max)
                {
                    continue;
                }
                else
                {
                    result = pipe;
                }
            }
            return result;
        }
        public List<Connector> GetConnectors(Element Input_Element)
        {
            //Element result = null;
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            return list_Connector;
        }
        public Connector GetConnector(Element Input_Element)
        {
            //Element result = null;
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            Connector connector = null;
            if (list_Connector[0].Origin.Z < list_Connector[1].Origin.Z)
            {
                connector = list_Connector[0];
            }
            else if (list_Connector[0].Origin.Z > list_Connector[1].Origin.Z)
            {
                connector = list_Connector[1];
            }
            return connector;
        }
        public XYZ GetIntersec1(XYZ stPoint, XYZ edPoint, Connector Conn_Origin, Pipe p_nhanh)
        {
            XYZ u = (edPoint - stPoint).Normalize();

            //pt duong thang
            //(x-Sx)/Ux = (y-Sy)/Uy = (z-Sz)/Uz
            //pt mat phang
            // Ux(x-Ox) + Uy(y-Oy) + Uz(z-Oz) = 0
            double intersectPoint_y = 0;
            double intersectPoint_x = 0;
            double intersectPoint_z = 0;

            if (Math.Round(u.X, 5) == 0)
            {
                intersectPoint_x = stPoint.X;
                double tu = (-u.Y * stPoint.Y + u.Y * Conn_Origin.Origin.Y - u.Z * stPoint.Z + u.Z * Conn_Origin.Origin.Z);
                double mau = (u.Y * u.Y + u.Z * u.Z);
                double t = tu / mau;
                intersectPoint_y = Conn_Origin.Origin.Y;
                intersectPoint_z = stPoint.Z + u.Z * t;
            }
            else if (Math.Round(u.Y, 5) == 0)
            {
                intersectPoint_y = stPoint.Y;
                double tu = (-u.X * stPoint.X + u.X * Conn_Origin.Origin.X - u.Z * stPoint.Z + u.Z * Conn_Origin.Origin.Z);
                double mau = (u.X * u.X + u.Z * u.Z);
                double t = tu / mau;
                intersectPoint_x = Conn_Origin.Origin.X;
                intersectPoint_z = stPoint.Z + u.Z * t;
            }
            else if (Math.Round(u.Z, 5) == 0)
            {
                intersectPoint_z = stPoint.Z;
                double tu = (-u.Y * stPoint.Y + u.Y * Conn_Origin.Origin.Y - u.X * stPoint.X + u.X * Conn_Origin.Origin.X);
                double mau = (u.Y * u.Y + u.X * u.X);
                double t = tu / mau;
                intersectPoint_x = stPoint.X + u.X * t;
                intersectPoint_y = stPoint.Y + u.Y * t;
            }
            else
            {
                XYZ CrossU = new XYZ(u.X,u.Y,u.Z).CrossProduct(XYZ.BasisZ);
                double tu1 = -((stPoint.X - Conn_Origin.Origin.X) * u.X + (stPoint.Y - Conn_Origin.Origin.Y) * u.Y + (stPoint.Z - Conn_Origin.Origin.Z) * u.Z);
                double mau1 = u.X * u.X + u.Y * u.Y + u.Z * u.Z; ;
                double t = tu1 / mau1;
                XYZ h = new XYZ(stPoint.X + u.X * t, stPoint.Y + u.Y * t, stPoint.Z + u.Z * t);
                

                XYZ diem1 = new XYZ(h.X, h.Y, 0);
                XYZ diem2 = new XYZ(Conn_Origin.Origin.X, Conn_Origin.Origin.Y, 0);
                double kc = diem1.DistanceTo(diem2);
                XYZ NewPoint =  null;
                XYZ NewPoint1 = CrossU * kc + h;
                XYZ NewPoint2 =  - CrossU * kc + h;
                if (NewPoint1.DistanceTo(Conn_Origin.Origin) < NewPoint2.DistanceTo(Conn_Origin.Origin))
                {
                    NewPoint = NewPoint1;
                }
                else
                {
                    NewPoint = NewPoint2;
                }

                XYZ umove = new XYZ(Conn_Origin.Origin.X, Conn_Origin.Origin.Y, NewPoint.Z) - NewPoint;
                XYZ result = h + umove;

                intersectPoint_x = result.X;
                intersectPoint_y = result.Y;
                intersectPoint_z = result.Z;

            }
            return new XYZ(intersectPoint_x, intersectPoint_y, intersectPoint_z);
        }
        public XYZ GetIntersec(XYZ stPoint, XYZ edPoint, Connector Conn_Origin)
        {
            XYZ u = (edPoint - stPoint).Normalize();
            
            //pt duong thang
            //(x-Sx)/Ux = (y-Sy)/Uy = (z-Sz)/Uz
            //pt mat phang
            // Ux(x-Ox) + Uy(y-Oy) + Uz(z-Oz) = 0
            double intersectPoint_y = 0;
            double intersectPoint_x = 0;
            double intersectPoint_z = 0;

            if (Math.Round(u.X, 5) == 0)
            {
                intersectPoint_x = stPoint.X;
                double tu = (-u.Y * stPoint.Y + u.Y * Conn_Origin.Origin.Y - u.Z * stPoint.Z + u.Z * Conn_Origin.Origin.Z);
                double mau = (u.Y * u.Y + u.Z * u.Z);
                double t = tu / mau;
                intersectPoint_y = stPoint.Y + u.Y * t;
                intersectPoint_z = stPoint.Z + u.Z * t;
            }
            else if (Math.Round(u.Y, 5) == 0)
            {
                intersectPoint_y = stPoint.Y;
                double tu = (-u.X * stPoint.X + u.X * Conn_Origin.Origin.X - u.Z * stPoint.Z + u.Z * Conn_Origin.Origin.Z);
                double mau = (u.X * u.X + u.Z * u.Z);
                double t = tu / mau;
                intersectPoint_x = stPoint.X + u.X * t;
                intersectPoint_z = stPoint.Z + u.Z * t;
            }
            else if (Math.Round(u.Z, 5) == 0)
            {
                intersectPoint_z = stPoint.Z;
                double tu = (-u.Y * stPoint.Y + u.Y * Conn_Origin.Origin.Y - u.X * stPoint.X + u.X * Conn_Origin.Origin.X);
                double mau = (u.Y * u.Y + u.X * u.X);
                double t = tu / mau;
                intersectPoint_x = stPoint.X + u.X * t;
                intersectPoint_y = stPoint.Y + u.Y * t;
            }
            else
            {
                double tu = u.X * u.X * stPoint.Y - stPoint.X * u.X * u.Y + Conn_Origin.Origin.X * u.X * u.Y + u.Y * u.Y * Conn_Origin.Origin.Y + u.Z * u.Z * stPoint.Y - stPoint.Z * u.Z * u.Y + Conn_Origin.Origin.Z * u.Z * u.Y;
                double mau = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
                intersectPoint_y = tu / mau;
                intersectPoint_x = (u.X / u.Y) * (intersectPoint_y - stPoint.Y) + stPoint.X;
                intersectPoint_z = (u.Z / u.Y) * (intersectPoint_y - stPoint.Y) + stPoint.Z;
            }
            return new XYZ(intersectPoint_x, intersectPoint_y, intersectPoint_z);
        }
        public Connector GetConnectorFromPoint(Element Input_Element, XYZ conXYZ)
        {
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            foreach (var item in list_Connector)
            {
                if (Math.Round(item.Origin.X,5) == Math.Round(conXYZ.X, 5) && Math.Round(item.Origin.Y, 5) == Math.Round(conXYZ.Y, 5) && Math.Round(item.Origin.Z, 5) == Math.Round(conXYZ.Z, 5))
                {
                    return item;
                }
            }
            return null;
        }
        public Connector GetConnectorNearPoint(Element Input_Element, XYZ conXYZ)
        {
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            if (list_Connector[0].Origin.DistanceTo(conXYZ) < list_Connector[1].Origin.DistanceTo(conXYZ))
            {
                return list_Connector[0];
            }
            else
            {
                return list_Connector[1];
            }
            
        }
        public Connector FindConnector(Element Ele, XYZ conXYZ)
        {
            ConnectorSet conns = null;
            if (Ele is Pipe)
            {
                Pipe p = Ele as Pipe;
                conns = p.ConnectorManager.Connectors;
            }
            foreach (Connector conn in conns)
            {
                if (conn.Origin.IsAlmostEqualTo(conXYZ))
                {
                    return conn;
                }
            }
            return null;
        }
        public Connector GetConnectorDifferentPoint(Element Input_Element, XYZ conXYZ)
        {
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            foreach (var item in list_Connector)
            {
                if (!item.Origin.IsAlmostEqualTo(conXYZ))
                {
                    return item;
                }
            }
            return null;
        }
        public void CopyParameters(Pipe source, Pipe target)
        {
            double diameter = source.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).AsDouble();
            target.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).Set(diameter);
        }
        public void Case1(XYZ Intersec, XYZ stPoint, XYZ edPoint, ElementId _pipeTypeID, ElementId _levelID, Element EleInput, double distance, XYZ point, Connector Conn_With_Tee, Pipe p1, Pipe p2, Pipe p3, Connector Conn_With_Elbow, XYZ newp)
        {
            #region 'Lay thong so'
            Pipe P = EleInput as Pipe;

            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;
            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1  / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2  / len);

            XYZ splitpoint = null;
            XYZ translation = null;
            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }
            var newpoint = stPoint + splitpoint;

            Pipe p_copy = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, p3.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), p3.PipeType.Id, p3.ReferenceLevel.Id, newp, Intersec);
            CopyParameters(p3, p_copy);


            double kcslope = newpoint.DistanceTo(newp);
            XYZ newp_slope = new XYZ(newp.X, newp.Y, newpoint.Z + kcslope * cls_ThoatNuoc.Slope / 100);
            double kcmove = Conn_With_Elbow.Origin.DistanceTo(newp_slope);

            ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, p1.Id, -new XYZ(0, 0, 1) * kcmove);
            ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, p2.Id, -new XYZ(0, 0, 1) * kcmove);
            ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, p3.Id, -new XYZ(0, 0, 1) * kcmove);

            Conn_With_Elbow.Origin = newp_slope;

            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);


                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, p_copy.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                SingleData.Singleton.Instance.RevitData.Document.Delete(p_copy.Id);
                Conn_With_Tee.Origin = newpoint;
                FamilyInstance Fitting2 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_With_Elbow, GetConnector(p2));

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.Id_Tee1 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, p_copy.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                SingleData.Singleton.Instance.RevitData.Document.Delete(p_copy.Id);

                Conn_With_Tee.Origin = newpoint;
                FamilyInstance Fitting2 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_With_Elbow, GetConnector(p2));

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.Id_Tee1 = Tee.Id;
            }

            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(EleInput.Id);

            //if (0 == deletedIdSet.Count)
            //{
            //    throw new Exception("Deleting the selected elements in Revit failed.");
            //}
            #endregion
        }
        public Connector GetConnecterNotConnected(Element Input_Element)
        {
            FamilyInstance tee = Input_Element as FamilyInstance;
            ConnectorSetIterator csi = null;
            csi = tee.MEPModel.ConnectorManager.Connectors.ForwardIterator();
            while (csi.MoveNext())
            {
                Connector connector = csi.Current as Connector;
                if (connector.IsConnected != true)
                {
                    return connector;
                }
            }
            return null;
        }
        public Connector GetConnecterPipeNotConnected(Element Input_Element)
        {
            Pipe p = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = p.ConnectorManager.Connectors.ForwardIterator();
            while (csi.MoveNext())
            {
                Connector connector = csi.Current as Connector;
                if (connector.IsConnected != true)
                {
                    return connector;
                }
            }
            return null;
        }
        public void Case2(Element Input_Ele, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn3, Connector Conn_O)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters);          
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters);
            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;
            Pipe pipe_new_test = null;
            Connector Conn_With_Tee = null;
            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnector(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()));
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);

                double d = newpoint.DistanceTo(new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, newpoint.Z));
                pipe_new_test = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeSystemType, _pipeTypeID, _levelID, newpoint, new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, newpoint.Z + d * cls_ThoatNuoc.Slope / 100));
                CopyParameters(P_new, pipe_new_test);
                Conn_With_Tee = GetConnector(pipe_new_test);
                LocationCurve lc_pipenewtest = pipe_new_test.Location as LocationCurve;
                XYZ st = lc_pipenewtest.Curve.GetEndPoint(0);
                XYZ ed = lc_pipenewtest.Curve.GetEndPoint(1);
                XYZ pointrugon = null;
                XYZ pointrugon1 = Conn_With_Tee.Origin + (st - ed).Normalize() * pipe_new_test.Diameter;
                XYZ pointrugon2 = Conn_With_Tee.Origin + (ed - st).Normalize() * pipe_new_test.Diameter;
                if (pointrugon1.Z < pointrugon2.Z)
                {
                    pointrugon = pointrugon2;
                }
                else
                {
                    pointrugon = pointrugon1;
                }
                Conn_With_Tee.Origin = pointrugon;

                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.Id_Tee2 = Tee.Id;

            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnector(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()));
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);

                double d = newpoint.DistanceTo(new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, newpoint.Z));
                pipe_new_test = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeSystemType, _pipeTypeID, _levelID, newpoint, new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, newpoint.Z + d * cls_ThoatNuoc.Slope / 100));
                CopyParameters(P_new, pipe_new_test);
                Conn_With_Tee = GetConnector(pipe_new_test);
                LocationCurve lc_pipenewtest = pipe_new_test.Location as LocationCurve;
                XYZ st = lc_pipenewtest.Curve.GetEndPoint(0);
                XYZ ed = lc_pipenewtest.Curve.GetEndPoint(1);
                XYZ pointrugon = null;
                XYZ pointrugon1 = Conn_With_Tee.Origin + (st - ed).Normalize() * pipe_new_test.Diameter;
                XYZ pointrugon2 = Conn_With_Tee.Origin + (ed - st).Normalize() * pipe_new_test.Diameter;
                if (pointrugon1.Z < pointrugon2.Z)
                {
                    pointrugon = pointrugon2;
                }
                else
                {
                    pointrugon = pointrugon1;
                }
                Conn_With_Tee.Origin = pointrugon;
                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.Id_Tee2 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'
            Connector Conn_Check = GetConnectorDifferentPoint(pipe_new_test, Conn_With_Tee.Origin);
            XYZ diem1 = new XYZ(Conn_With_Tee.Origin.X, Conn_With_Tee.Origin.Y, 0);
            XYZ diem2 = new XYZ(Conn_Check.Origin.X, Conn_Check.Origin.Y, 0);
            double kc = diem1.DistanceTo(diem2);

            double d_check = pipe_new_test.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).AsDouble();

            XYZ splitpoint_2check = (Conn_With_Tee.Origin - Conn_Check.Origin) * ((d_check * 1.2) / kc);
            // point 2 elbow
            XYZ newpoint1_2check = Conn_Check.Origin + splitpoint_2check;

            Pipe p_new_check = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeSystemType, _pipeTypeID, _levelID, Conn_Check.Origin, newpoint1_2check);
            CopyParameters(P_new, p_new_check);
            Conn_Check.Origin = newpoint1_2check;
            Connector Conn_Check_Top = GetConnectorDifferentPoint(p_new_check, newpoint1_2check);
            double z = p_new_check.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();
            Conn_Check_Top.Origin = new XYZ(Conn_Check_Top.Origin.X, Conn_Check_Top.Origin.Y, Conn_Check_Top.Origin.Z + z);
            Conn_O.Origin = new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, Conn_O.Origin.Z + z);

            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_O, Conn_Check_Top);
            FamilyInstance Fitting1 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Check, GetConnectorFromPoint(p_new_check, newpoint1_2check));
            SingleData.Singleton.Instance.RevitData.Document.Delete(P_new.Id);
            #endregion
        }
        public Pipe GetElement1_Case3(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double min = doc.GetElement(Input_ref[0]).LookupParameter("Middle Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Middle Elevation").AsDouble();
                if (elevation < min)
                {
                    min = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Middle Elevation").AsDouble();
                if (elevation == min)
                {
                    result = pipe;
                }
            }
            return result;
        }
        public Pipe GetElement2_Case3(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double max = doc.GetElement(Input_ref[0]).LookupParameter("Middle Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Middle Elevation").AsDouble();
                if (elevation > max)
                {
                    max = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Middle Elevation").AsDouble();
                if (elevation == max)
                {
                    result = pipe;
                }
            }
            return result;
        }
        public Connector GetConnector_Case3(Element Input_Element)
        {
            //Element result = null;
            Pipe pipe = Input_Element as Pipe;
            ConnectorSetIterator csi = null;
            csi = pipe.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                Connector conn = csi.Current as Connector;
                list_Connector.Add(conn);
            }
            Connector connector = null;
            if (list_Connector[0].Origin.Z < list_Connector[1].Origin.Z)
            {
                connector = list_Connector[0];
            }
            else
            {
                connector = list_Connector[1];
            }
            return connector;
        }
        public void Case3(Element Input_Ele, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn3, Connector Conn_O)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee3 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn3.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee3 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'
            Connector Conn_Check = GetConnectorDifferentPoint(P_new, Conn3.Origin);
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_O, Conn_Check);
            #endregion
        }
        public void Case4(Element Input_Ele, Element Input_Ele2, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn3, Connector Conn_O)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            Pipe P2 = Input_Ele2 as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1  / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2  / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee4 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn3.Origin = newpoint;
                

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee4 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            //if (0 == deletedIdSet.Count)
            //{
            //    throw new Exception("Deleting the selected elements in Revit failed.");
            //}
            #endregion
            #region 'Tao check'
            Connector Conn_Check = GetConnectorDifferentPoint(P_new, Conn3.Origin);
            XYZ diem1 = new XYZ(Conn3.Origin.X, Conn3.Origin.Y, 0);
            XYZ diem2 = new XYZ(Conn_Check.Origin.X, Conn_Check.Origin.Y, 0);
            double kc = diem1.DistanceTo(diem2);

            XYZ splitpoint_2check = (Conn3.Origin - Conn_Check.Origin) * ((kc / 6) / kc);
            // point 2 elbow
            XYZ newpoint1_2check = Conn_Check.Origin + splitpoint_2check;

            double d = newpoint1_2check.DistanceTo(Conn_Check.Origin);

            LocationCurve lc2 = P2.Location as LocationCurve;
            XYZ stPoint2 = lc2.Curve.GetEndPoint(0);
            XYZ edPoint2 = lc2.Curve.GetEndPoint(1);
            XYZ u = null;
            if (edPoint2.X - stPoint2.X > 0)
            {
                u = new XYZ(edPoint2.X - stPoint2.X, edPoint2.Y - stPoint2.Y, edPoint2.Z - stPoint2.Z).Normalize();
            }
            else
            {
                u = new XYZ(-edPoint2.X + stPoint2.X, -edPoint2.Y + stPoint2.Y, -edPoint2.Z + stPoint2.Z).Normalize();
            }

            XYZ point1 = Conn_O.Origin + d * u;
            XYZ point2 = Conn_O.Origin - d * u;

            if (point1.Z < point2.Z)
            {
                Conn_O.Origin = point2;
                cls_ThoatNuoc.u_move = -u;
            }
            else
            {
                Conn_O.Origin = point1;
                cls_ThoatNuoc.u_move = u;
            }

            Pipe p_new_check = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeSystemType, _pipeTypeID, _levelID, Conn_O.Origin, newpoint1_2check);
            CopyParameters(P_new, p_new_check);
            Conn_Check.Origin = newpoint1_2check;

            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_O, GetConnectorDifferentPoint(p_new_check, newpoint1_2check));
            FamilyInstance Fitting1 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Check, GetConnectorFromPoint(p_new_check, newpoint1_2check));

            cls_ThoatNuoc.Id_move = Fitting0.Id;          
            cls_ThoatNuoc.kc_move = Fitting0.get_Parameter(BuiltInParameter.RBS_PIPE_SIZE_MAXIMUM).AsDouble() /2;
            #endregion
        }
        public Pipe GetElement1_Case5(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double min = doc.GetElement(Input_ref[0]).LookupParameter("Bottom Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Bottom Elevation").AsDouble();
                if (elevation < min)
                {
                    min = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Bottom Elevation").AsDouble();
                if (elevation == min)
                {
                    result = pipe;
                }
            }
            return result;
        }
        public Pipe GetElement2_Case5(IList<Reference> Input_ref, Document doc)
        {
            Pipe result = null;
            double max = doc.GetElement(Input_ref[0]).LookupParameter("Bottom Elevation").AsDouble();
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Bottom Elevation").AsDouble();
                if (elevation > max)
                {
                    max = elevation;
                }
            }
            foreach (var item in Input_ref)
            {
                Pipe pipe = doc.GetElement(item) as Pipe;
                double elevation = pipe.LookupParameter("Bottom Elevation").AsDouble();
                if (elevation == max)
                {
                    result = pipe;
                }
            }
            return result;
        }
        public void Case5(Element Input_Ele, Pipe P_new, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Connector Conn3, Connector Conn_O)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance/4, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance/4, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1  / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2  / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong new'
            Connector Conn_new_Intersec = GetConnectorFromPoint(P_new, Conn3.Origin);
            Connector Conn_new_Bottom = GetConnectorFromPoint(P_new, Conn_O.Origin);

            XYZ splitpoint_Intersec = (Conn_new_Bottom.Origin - Conn_new_Intersec.Origin) * (distance / 4 / distance);
            var newpoint_Intersec = Conn_new_Intersec.Origin + splitpoint_Intersec;

            Conn_new_Intersec.Origin = newpoint_Intersec ;

            double d_check = P_new.Diameter;

            XYZ splitpoint_Bottom = (Conn_new_Intersec.Origin - Conn_new_Bottom.Origin) * (d_check * 2 / distance);
            var newpoint_Bottom = Conn_new_Bottom.Origin + splitpoint_Bottom;

            Conn_new_Bottom.Origin = newpoint_Bottom ;

            

            //distance
            Pipe p_new_3chac = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, P_new.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), P_new.PipeType.Id, P_new.ReferenceLevel.Id, Conn_new_Intersec.Origin, Intersec);
            CopyParameters(P_new, p_new_3chac);
            Pipe p_new_2check = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, P_new.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), P_new.PipeType.Id, P_new.ReferenceLevel.Id, Conn_new_Bottom.Origin, Conn_O.Origin);
            CopyParameters(P_new, p_new_2check);

            //Connector conncheckbot = GetConnector(p_new_2check);
            //Connector connchecktop = GetConnectorDifferentPoint(p_new_2check, GetConnector(p_new_2check).Origin);
            //Connector connvsbot = Conn_O;
            //double kc = new XYZ(conncheckbot.Origin.X, conncheckbot.Origin.Y, 0).DistanceTo(new XYZ(connvsbot.Origin.X, connvsbot.Origin.Y, 0));

            //connchecktop.Origin = new XYZ(connvsbot.Origin.X, connvsbot.Origin.Y, conncheckbot.Origin.Z + kc);

            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(pipe_new_1, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, p_new_3chac.Id, translation * distance / 4);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                Connector conn_not_tg = GetConnectorDifferentPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                conn_not_tg.Origin = new XYZ(conn_not_tg.Origin.X, conn_not_tg.Origin.Y, conn_tg.Origin.Z);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Connector Conn_3_chac = GetConnectorFromPoint(p_new_3chac, Intersec);
                Conn_3_chac.Origin = newpoint;
                

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_3_chac);
                cls_ThoatNuoc.Id_Tee5 = Tee.Id;

            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(pipe_new_1, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, p_new_3chac.Id, translation * distance / 4);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                Connector conn_not_tg = GetConnectorDifferentPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                conn_not_tg.Origin = new XYZ(conn_not_tg.Origin.X, conn_not_tg.Origin.Y, conn_tg.Origin.Z);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Connector Conn_3_chac = GetConnectorFromPoint(p_new_3chac, Intersec);
                Conn_3_chac.Origin = newpoint;
                

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_3_chac);
                cls_ThoatNuoc.Id_Tee5 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'

            double len_p_2check = p_new_2check.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();


            GetConnectorDifferentPoint(p_new_2check, Conn_new_Bottom.Origin).Origin = new XYZ(GetConnectorDifferentPoint(p_new_2check, Conn_new_Bottom.Origin).Origin.X, GetConnectorDifferentPoint(p_new_2check, Conn_new_Bottom.Origin).Origin.Y, GetConnectorDifferentPoint(p_new_2check, Conn_new_Bottom.Origin).Origin.Z + len_p_2check);

            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_new_Intersec, GetConnectorFromPoint(p_new_3chac, Conn_new_Intersec.Origin));
            FamilyInstance Fitting1 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_new_Bottom, GetConnectorFromPoint(p_new_2check, Conn_new_Bottom.Origin));
            FamilyInstance Fitting2 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(GetConnectorDifferentPoint(p_new_2check,GetConnector(p_new_2check).Origin), Conn_O);
            #endregion
        }
        public Connector FindConnectedTo(Element Ele, XYZ conXYZ)
        {
            Connector connItself = FindConnector(Ele, conXYZ);
            ConnectorSet connSet = connItself.AllRefs;
            foreach (Connector conn in connSet)
            {
                if (conn.Owner.Id.IntegerValue != Ele.Id.IntegerValue &&
                    conn.ConnectorType == ConnectorType.End)
                {
                    return conn;
                }
            }
            return null;
        }
        public void Case6(Element Input_Ele, Element Input_Ele2, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn3, Connector Conn_O)
        {
           
            #region 'Lay thong so'
            Pipe P_chinh = Input_Ele as Pipe;
            Pipe P_nhanh = Input_Ele2 as Pipe;

            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); 
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); 
            double len = UnitUtils.ConvertFromInternalUnits(P_chinh.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;          

            Connector startConn = FindConnectedTo(P_chinh, stPoint);
            Connector endConn = FindConnectedTo(P_chinh, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.list_Id_Tee6.Add(Tee.Id);
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P_chinh, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.list_Id_Tee6.Add(Tee.Id);
            }
            cls_ThoatNuoc.Id_pipe_new_1 = pipe_new_1.Id;
            cls_ThoatNuoc.Id_pipe_new_2 = pipe_new_2.Id;
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'
            Connector Conn_Check = GetConnectorDifferentPoint(P_new, Conn3.Origin);
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_O, Conn_Check);

            #endregion
        }
        public void Case7(Element Input_Ele, Element Input_Ele2, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Connector Conn_O)
        {
            #region 'Lay thong so'
            Pipe P_chinh = Input_Ele as Pipe;
            Pipe P_nhanh = Input_Ele2 as Pipe;     

            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters);
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters);
            double len = UnitUtils.ConvertFromInternalUnits(P_chinh.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }
            var newpoint = stPoint + splitpoint;

            XYZ diem1 = new XYZ(newpoint.X, newpoint.Y, 0);
            XYZ diem2 = new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, 0);
            double kc = diem1.DistanceTo(diem2);
            XYZ DiemMove = new XYZ(Conn_O.Origin.X, Conn_O.Origin.Y, newpoint.Z + kc * cls_ThoatNuoc.Slope / 100);
            double kc_Move = Conn_O.Origin.DistanceTo(DiemMove);
            XYZ axis = null;
            if (DiemMove.Z > Conn_O.Origin.Z)
            {
                axis = new XYZ(0, 0, 1);
            }
            else
            {
                axis = -new XYZ(0, 0, 1);
            }
            ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, P_nhanh.Id, kc_Move * axis);
            Pipe p_new = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, P_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), P_nhanh.PipeType.Id, P_nhanh.ReferenceLevel.Id, newpoint, DiemMove);
            CopyParameters(P_nhanh, p_new);

            Connector Conn_With_Tee = GetConnectorFromPoint(p_new, newpoint);

            Connector startConn = FindConnectedTo(P_chinh, stPoint);
            Connector endConn = FindConnectedTo(P_chinh, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);

                LocationCurve lc = P_nhanh.Location as LocationCurve;
                Line dir = lc.Curve as Line;
                double kcslpoe = Intersec.DistanceTo(Conn_O.Origin);
                XYZ pointslope = (newpoint + kcslpoe* dir.Direction.Normalize());
                Pipe p_new1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, P_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), P_nhanh.PipeType.Id, P_nhanh.ReferenceLevel.Id, pointslope, newpoint);
                CopyParameters(P_nhanh, p_new1);

                Connector conn_tg = GetConnectorFromPoint(p_new1, newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(p_new1.Id);
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.list_Id_Tee7.Add(Tee.Id);
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P_chinh, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);

                LocationCurve lc = P_nhanh.Location as LocationCurve;
                Line dir = lc.Curve as Line;
                double kcslpoe = Intersec.DistanceTo(Conn_O.Origin);
                XYZ pointslope = (newpoint + kcslpoe * dir.Direction.Normalize());
                Pipe p_new1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, P_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), P_nhanh.PipeType.Id, P_nhanh.ReferenceLevel.Id, pointslope, newpoint);
                CopyParameters(P_nhanh, p_new1);

                Connector conn_tg = GetConnectorFromPoint(p_new1, newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(p_new1.Id);
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_With_Tee);
                cls_ThoatNuoc.list_Id_Tee7.Add(Tee.Id);
            }
            cls_ThoatNuoc.Id_pipe_new_1 = pipe_new_1.Id;
            cls_ThoatNuoc.Id_pipe_new_2 = pipe_new_2.Id;
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'
            cls_ThoatNuoc.Id_pipe_new_case7 = p_new.Id;
            #endregion
        }
        public void Case8(Element Input_Ele, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn_p_Intersec, Connector Conn_p_newcheck, Connector Conn_Bottom_check)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee8 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee8 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'           
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Bottom_check, Conn_p_newcheck);
            #endregion
        }
        public void Case9(Element Input_Ele, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn_p_Intersec, Connector Conn_p_new, Connector Conn_Bottom_along)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee9 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee9 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'           
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Bottom_along, Conn_p_new);
            #endregion
        }
        public void Case10(Element Input_Ele, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn_p_Intersec, Connector Conn_p_new, Connector Conn_Bottom_along)
        {
            #region 'Lay thong so'
            Pipe P = Input_Ele as Pipe;
            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters); ;
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters); ;

            double len = UnitUtils.ConvertFromInternalUnits(P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;
            Connector startConn = FindConnectedTo(P, stPoint);
            Connector endConn = FindConnectedTo(P, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;

                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee10 = Tee.Id;
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorNearPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());

                Conn_p_Intersec.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn_p_Intersec);
                cls_ThoatNuoc.Id_Tee10 = Tee.Id;
            }
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'           
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Bottom_along, Conn_p_new);
            #endregion
        }
        public void Case11(Element Input_Ele, Element Input_Ele2, XYZ stPoint, XYZ edPoint, XYZ Intersec, double distance, ElementId _pipeSystemType, ElementId _pipeTypeID, ElementId _levelID, Pipe P_new, Connector Conn3, Connector Conn_O)
        {

            #region 'Lay thong so'
            Pipe P_chinh = Input_Ele as Pipe;
            Pipe P_nhanh = Input_Ele2 as Pipe;

            double kc1 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) - distance, UnitTypeId.Millimeters);
            double kc2 = UnitUtils.ConvertFromInternalUnits(stPoint.DistanceTo(Intersec) + distance, UnitTypeId.Millimeters);
            double len = UnitUtils.ConvertFromInternalUnits(P_chinh.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters);

            XYZ splitpoint1 = (edPoint - stPoint) * (kc1 / len);
            XYZ splitpoint2 = (edPoint - stPoint) * (kc2 / len);

            XYZ splitpoint = null;
            XYZ translation = null;

            if (splitpoint1.Z < splitpoint2.Z)
            {
                splitpoint = splitpoint1;
                translation = (stPoint - edPoint).Normalize();
            }
            else
            {
                splitpoint = splitpoint2;
                translation = (edPoint - stPoint).Normalize();
            }

            var newpoint = stPoint + splitpoint;

            Connector startConn = FindConnectedTo(P_chinh, stPoint);
            Connector endConn = FindConnectedTo(P_chinh, edPoint);
            #endregion
            #region 'Chia ong tao Tee'
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;

            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, startConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);

                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee11 =  (Tee.Id);
            }
            else
            {
                Connector stConn = GetConnectorFromPoint(P_chinh, stPoint);
                pipe_new_1 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, stConn, newpoint);
                Connector conn1 = GetConnectorFromPoint(pipe_new_1, newpoint);
                CopyParameters(P_chinh, pipe_new_1);
                pipe_new_2 = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, _pipeTypeID, _levelID, conn1, edPoint);
                Connector conn2 = GetConnectorFromPoint(pipe_new_2, newpoint);
                CopyParameters(P_chinh, pipe_new_2);
                ICollection<ElementId> a = ElementTransformUtils.CopyElement(SingleData.Singleton.Instance.RevitData.Document, P_new.Id, translation * distance);
                Connector conn_tg = GetConnectorFromPoint(SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()), newpoint);
                CopyParameters(pipe_new_1, SingleData.Singleton.Instance.RevitData.Document.GetElement(a.First<ElementId>()) as Pipe);
                FamilyInstance Tee = SingleData.Singleton.Instance.RevitData.Document.Create.NewTeeFitting(conn1, conn2, conn_tg);
                SingleData.Singleton.Instance.RevitData.Document.Delete(a.First<ElementId>());
                Conn3.Origin = newpoint;
                try
                {
                    Tee.LookupParameter("Angle").SetValueString("45");
                }
                catch (Exception)
                {
                    Tee.LookupParameter("ANGLE3").SetValueString("45");
                }
                Connector Connector_Tee = GetConnecterNotConnected(Tee);
                Connector_Tee.ConnectTo(Conn3);
                cls_ThoatNuoc.Id_Tee11 = (Tee.Id);
            }

            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, edPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = SingleData.Singleton.Instance.RevitData.Document.Delete(Input_Ele.Id);

            #endregion
            #region 'Tao check'
            Connector Conn_Check = GetConnectorDifferentPoint(P_new, Conn3.Origin);
            FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_O, Conn_Check);

            #endregion
        }  
        public XYZ CheckIntersec(XYZ stpoint, XYZ endpoit, XYZ Intersec)
        {
            if (Math.Round(stpoint.X,5) == Math.Round(endpoit.X, 5))
            {
                if (stpoint.Y < endpoit.Y)
                {
                    if (Intersec.Y > stpoint.Y && Intersec.Y < endpoit.Y)
                    {
                        return Intersec;
                    }
                }
                else if (stpoint.Y > endpoit.Y)
                {
                    if (Intersec.Y > endpoit.Y && Intersec.Y < stpoint.Y)
                    {
                        return Intersec;
                    }
                }
                else if (stpoint.Y == endpoit.Y)
                {
                    if (stpoint.Z > endpoit.Z)
                    {
                        if (Intersec.Z > endpoit.Z && Intersec.Z < stpoint.Z)
                        {
                            return Intersec;
                        }
                    }
                    else
                    {
                        if (Intersec.Z > stpoint.Z && Intersec.Z < endpoit.Z)
                        {
                            return Intersec;
                        }
                    }
                }
            }
            else if (Math.Round(stpoint.Y, 5) == Math.Round(endpoit.Y, 5))
            {
                if (stpoint.X < endpoit.X)
                {
                    if (Intersec.X > stpoint.X && Intersec.X < endpoit.X)
                    {
                        return Intersec;
                    }
                }
                else if (stpoint.X > endpoit.X)
                {
                    if (Intersec.X > endpoit.X && Intersec.X < stpoint.X)
                    {
                        return Intersec;
                    }
                }
                else if (stpoint.X == endpoit.X)
                {
                    if (stpoint.Z > endpoit.Z)
                    {
                        if (Intersec.Z > endpoit.Z && Intersec.Z < stpoint.Z)
                        {
                            return Intersec;
                        }
                    }
                    else
                    {
                        if (Intersec.Z > stpoint.Z && Intersec.Z < endpoit.Z)
                        {
                            return Intersec;
                        }
                    }
                }
            }
            else
            {
                if (stpoint.X < endpoit.X)
                {
                    if (stpoint.Y < endpoit.Y)
                    {
                        if (Intersec.X > stpoint.X && Intersec.X < endpoit.X && Intersec.Y > stpoint.Y && Intersec.Y < endpoit.Y)
                        {
                            return Intersec;
                        }
                    }
                    else if (stpoint.Y > endpoit.Y)
                    {
                        if (Intersec.X > stpoint.X && Intersec.X < endpoit.X && Intersec.Y > endpoit.Y && Intersec.Y < stpoint.Y)
                        {
                            return Intersec;
                        }
                    }
                }
                else if (stpoint.X > endpoit.X)
                {
                    if (stpoint.Y < endpoit.Y)
                    {
                        if (Intersec.X > endpoit.X && Intersec.X < stpoint.X && Intersec.Y > stpoint.Y && Intersec.Y < endpoit.Y)
                        {
                            return Intersec;
                        }
                    }
                    else if (stpoint.Y > endpoit.Y)
                    {
                        if (Intersec.X > endpoit.X && Intersec.X < stpoint.X && Intersec.Y > endpoit.Y && Intersec.Y < stpoint.Y)
                        {
                            return Intersec;
                        }
                    }
                }
            }
            return null;
        }

        public Connector GetConnector_Fixtures(FamilyInstance FamIns)
        {
            Connector result = null;
            ConnectorSetIterator csi = null;
            csi = FamIns.MEPModel.ConnectorManager.Connectors.ForwardIterator();
            List<Connector> list_Connector = new List<Connector>();
            while (csi.MoveNext())
            {
                list_Connector.Add(csi.Current as Connector);
            }

            double min = list_Connector[0].Origin.Z;

            foreach (var item in list_Connector)
            {
                if (item.Origin.Z < min)
                {
                    min = item.Origin.Z;
                }   
            }

            foreach (var item in list_Connector)
            {
                if (item.Origin.Z == min)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
    
    }
}
