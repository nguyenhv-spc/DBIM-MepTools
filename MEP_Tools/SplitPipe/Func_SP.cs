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
    class Func_SP
    {
        public void SplitPipe(Element Ele_Input, Document doc, double l)
        {
            #region 'Get value create pipe' 
            Pipe P = Ele_Input as Pipe;
            ElementId levelId = P.ReferenceLevel.Id;
            PipeType _pipeType = P.PipeType;

            double len = P.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble() * 304.8;

            if (len <= l)
                return; 
            
            Curve c1 = (P.Location as LocationCurve).Curve;
            XYZ startPoint = new XYZ(), endPoint = new XYZ();
            if (clsBien_SP.FromEdge == "Start")
            {
                startPoint = c1.GetEndPoint(0);
                endPoint = c1.GetEndPoint(1);
            }
            else if (clsBien_SP.FromEdge == "End")
            {
                startPoint = c1.GetEndPoint(1);
                endPoint = c1.GetEndPoint(0);
            }
            
            XYZ splitpoint = (endPoint - startPoint) * (l / len);
            var newpoint = startPoint + splitpoint;
            #endregion
            #region 'creating pipe'
            Connector startConn = FindConnectedTo(P, startPoint);
            Connector endConn = FindConnectedTo(P, endPoint);
            Pipe pipe_new_1 = null;
            Pipe pipe_new_2 = null;
            if (startConn != null)
            {
                pipe_new_1 = Pipe.Create(doc, _pipeType.Id, levelId, startConn, newpoint);
                Connector conn1 = FindConnector(pipe_new_1, newpoint);

                pipe_new_2 = Pipe.Create(doc, _pipeType.Id, levelId, conn1, endPoint);
                Connector conn2 = FindConnector(pipe_new_2, newpoint);

                CopyParameters(pipe_new_1, pipe_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            else
            {
                Connector stConn = FindConnector(P, startPoint);
                pipe_new_1 = Pipe.Create(doc, _pipeType.Id, levelId, stConn, newpoint);
                Connector conn1 = FindConnector(pipe_new_1, newpoint);

                pipe_new_2 = Pipe.Create(doc, _pipeType.Id, levelId, conn1, endPoint);
                Connector conn2 = FindConnector(pipe_new_2, newpoint);

                CopyParameters(pipe_new_1, pipe_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            #endregion
            #region 'Connect'
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(pipe_new_2, endPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = doc.Delete(Ele_Input.Id);

            if (0 == deletedIdSet.Count)
            {
                throw new Exception("Deleting the selected elements in Revit failed.");
            }

            #endregion
            #region 'Lap'
            l = Convert.ToDouble(clsBien_SP.Distance_Twos_Union);
            if (UnitUtils.ConvertFromInternalUnits(pipe_new_2.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters) > l)
            {
                clsBien_SP.FromEdge = "Start";
                SplitPipe(pipe_new_2, doc, l);
            }
            #endregion
        }
        public void SplitDuct(Element Ele_Input, Document doc, double l)
        {
            #region 'Get value create pipe' 
            Duct Dt = Ele_Input as Duct;
            ElementId levelId = Dt.ReferenceLevel.Id;
            DuctType _ducttype = Dt.DuctType;

            double len = Dt.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble() * 304.8;

            if (len <= l)
                return;

            Curve c1 = (Dt.Location as LocationCurve).Curve;
            XYZ startPoint = new XYZ(), endPoint = new XYZ();
            if (clsBien_SP.FromEdge == "Start")
            {
                startPoint = c1.GetEndPoint(0);
                endPoint = c1.GetEndPoint(1);
            }
            else if (clsBien_SP.FromEdge == "End")
            {
                startPoint = c1.GetEndPoint(1);
                endPoint = c1.GetEndPoint(0);
            }
            XYZ splitpoint = (endPoint - startPoint) * (l / len);
            var newpoint = startPoint + splitpoint;
            #endregion
            #region 'creating pipe'
            Connector startConn = FindConnectedTo(Dt, startPoint);
            Connector endConn = FindConnectedTo(Dt, endPoint);
            Duct duct_new_1 = null;
            Duct duct_new_2 = null;
            if (startConn != null)
            {
                duct_new_1 = Duct.Create(doc, _ducttype.Id, levelId, startConn, newpoint);
                Connector conn1 = FindConnector(duct_new_1, newpoint);

                duct_new_2 = Duct.Create(doc, _ducttype.Id, levelId, conn1, endPoint);
                Connector conn2 = FindConnector(duct_new_2, newpoint);

                CopyParameters1(duct_new_1, duct_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            else
            {
                Connector stConn = FindConnector(Dt, startPoint);
                duct_new_1 = Duct.Create(doc, _ducttype.Id, levelId, stConn, newpoint);
                Connector conn1 = FindConnector(duct_new_1, newpoint);

                duct_new_2 = Duct.Create(doc, _ducttype.Id, levelId, conn1, endPoint);
                Connector conn2 = FindConnector(duct_new_2, newpoint);

                CopyParameters1(duct_new_1, duct_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            #endregion
            #region 'Connect'
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(duct_new_2, endPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = doc.Delete(Ele_Input.Id);

            if (0 == deletedIdSet.Count)
            {
                throw new Exception("Deleting the selected elements in Revit failed.");
            }

            #endregion
            #region 'Lap'
            l = Convert.ToDouble(clsBien_SP.Distance_Twos_Union);
            if (UnitUtils.ConvertFromInternalUnits(duct_new_2.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters) > l)
            {
                clsBien_SP.FromEdge = "Start";
                SplitDuct(duct_new_2, doc, l);
            }
            #endregion
        }
        public void SplitCableTray(Element Ele_Input, Document doc, double l)
        {
            #region 'Get value create pipe' 
            CableTray Cbt = Ele_Input as CableTray;
            ElementId levelId = Cbt.ReferenceLevel.Id;
            ElementId _CbtTypeID = Cbt.GetTypeId();

            double len = Cbt.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble() * 304.8;

            if (len <= l)
                return;

            Curve c1 = (Cbt.Location as LocationCurve).Curve;
            XYZ startPoint = new XYZ(), endPoint = new XYZ();
            if (clsBien_SP.FromEdge == "Start")
            {
                startPoint = c1.GetEndPoint(0);
                endPoint = c1.GetEndPoint(1);
            }
            else if (clsBien_SP.FromEdge == "End")
            {
                startPoint = c1.GetEndPoint(1);
                endPoint = c1.GetEndPoint(0);
            }
            XYZ splitpoint = (endPoint - startPoint) * (l / len);
            var newpoint = startPoint + splitpoint;
            #endregion
            #region 'creating pipe'
            Connector startConn = FindConnectedTo(Cbt, startPoint);
            Connector endConn = FindConnectedTo(Cbt, endPoint);
            CableTray Cbt_new_1 = null;
            CableTray Cbt_new_2 = null;
            if (startConn != null)
            {
                Cbt_new_1 = CableTray.Create(doc, _CbtTypeID, startConn.Origin, newpoint, levelId);
                Connector conn1 = FindConnector(Cbt_new_1, newpoint);

                Cbt_new_2 = CableTray.Create(doc, _CbtTypeID, conn1.Origin, endPoint, levelId);
                Connector conn2 = FindConnector(Cbt_new_2, newpoint);

                CopyParameters2(Cbt_new_1, Cbt_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            else
            {
                Connector stConn = FindConnector(Cbt, startPoint);
                Cbt_new_1 = CableTray.Create(doc, _CbtTypeID, stConn.Origin, newpoint, levelId);
                Connector conn1 = FindConnector(Cbt_new_1, newpoint);

                Cbt_new_2 = CableTray.Create(doc, _CbtTypeID, conn1.Origin, endPoint, levelId);
                Connector conn2 = FindConnector(Cbt_new_2, newpoint);

                CopyParameters2(Cbt_new_1, Cbt_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            #endregion
            #region 'Connect'
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(Cbt_new_2, endPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = doc.Delete(Ele_Input.Id);

            if (0 == deletedIdSet.Count)
            {
                throw new Exception("Deleting the selected elements in Revit failed.");
            }

            #endregion
            #region 'Lap'
            l = Convert.ToDouble(clsBien_SP.Distance_Twos_Union);
            if (UnitUtils.ConvertFromInternalUnits(Cbt_new_2.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters) > l)
            {
                clsBien_SP.FromEdge = "Start";
                SplitCableTray(Cbt_new_2, doc, l);
            }
            #endregion
        }
        public void SplitConduit(Element Ele_Input, Document doc, double l)
        {
            #region 'Get value create pipe' 
            Conduit Cd = Ele_Input as Conduit;
            ElementId levelId = Cd.ReferenceLevel.Id;
            ElementId _CdTypeID = Cd.GetTypeId();

            double len = Cd.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble() * 304.8;

            if (len <= l)
                return;

            Curve c1 = (Cd.Location as LocationCurve).Curve;
            XYZ startPoint = new XYZ(), endPoint = new XYZ();
            if (clsBien_SP.FromEdge == "Start")
            {
                startPoint = c1.GetEndPoint(0);
                endPoint = c1.GetEndPoint(1);
            }
            else if (clsBien_SP.FromEdge == "End")
            {
                startPoint = c1.GetEndPoint(1);
                endPoint = c1.GetEndPoint(0);
            }
            XYZ splitpoint = (endPoint - startPoint) * (l / len);
            var newpoint = startPoint + splitpoint;
            #endregion
            #region 'creating pipe'
            Connector startConn = FindConnectedTo(Cd, startPoint);
            Connector endConn = FindConnectedTo(Cd, endPoint);
            Conduit Cd_new_1 = null;
            Conduit Cd_new_2 = null;
            if (startConn != null)
            {
                Cd_new_1 = Conduit.Create(doc, _CdTypeID, startConn.Origin, newpoint, levelId);
                Connector conn1 = FindConnector(Cd_new_1, newpoint);

                Cd_new_2 = Conduit.Create(doc, _CdTypeID, conn1.Origin, endPoint, levelId);
                Connector conn2 = FindConnector(Cd_new_2, newpoint);

                CopyParameters3(Cd, Cd_new_1);
                CopyParameters3(Cd, Cd_new_2);
                doc.Create.NewUnionFitting(conn1, conn2);
            }
            else
            {
                Connector stConn = FindConnector(Cd, startPoint);
                Cd_new_1 = Conduit.Create(doc, _CdTypeID, stConn.Origin, newpoint, levelId);
                Connector conn1 = FindConnector(Cd_new_1, newpoint);

                Cd_new_2 = Conduit.Create(doc, _CdTypeID, conn1.Origin, endPoint, levelId);
                Connector conn2 = FindConnector(Cd_new_2, newpoint);
                CopyParameters3(Cd, Cd_new_1);
                CopyParameters3(Cd, Cd_new_2);

                doc.Create.NewUnionFitting(conn1, conn2);
            }
            #endregion
            #region 'Connect'
            if (null != endConn)
            {
                Connector pipeEndConn = FindConnector(Cd_new_2, endPoint);
                pipeEndConn.ConnectTo(endConn);
            }

            ICollection<ElementId> deletedIdSet = doc.Delete(Ele_Input.Id);

            if (0 == deletedIdSet.Count)
            {
                throw new Exception("Deleting the selected elements in Revit failed.");
            }

            #endregion
            #region 'Lap'
            l = Convert.ToDouble(clsBien_SP.Distance_Twos_Union);
            if (UnitUtils.ConvertFromInternalUnits(Cd_new_2.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Millimeters) > l)
            {
                clsBien_SP.FromEdge = "Start";
                SplitConduit(Cd_new_2, doc, l);
            }
            #endregion
        }
        public Connector FindConnector(Element Ele, XYZ conXYZ)
        {
            ConnectorSet conns = null;
            if (Ele is Pipe)
            {
                Pipe p = Ele as Pipe;
                conns = p.ConnectorManager.Connectors;
            }
            else if (Ele is Duct)
            {
                Duct d = Ele as Duct;
                conns = d.ConnectorManager.Connectors;
            }
            else if (Ele is CableTray)
            {
                CableTray ct = Ele as CableTray;
                conns = ct.ConnectorManager.Connectors;
            }
            else if (Ele is Conduit)
            {
                Conduit cd = Ele as Conduit;
                conns = cd.ConnectorManager.Connectors;
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
        public void CopyParameters(Pipe source, Pipe target)
        {
            double diameter = source.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).AsDouble();
            target.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).Set(diameter);
        }
        public void CopyParameters1(Duct source, Duct target)
        {
            try
            {
                double diameter = source.Diameter;
                target.LookupParameter("Diameter").Set(diameter);
            }
            catch
            {
                double height = source.Height;
                target.LookupParameter("Width").Set(height);
                double width = source.Width;
                target.LookupParameter("Width").Set(width);
            }
            
        }
        public void CopyParameters2(CableTray source, CableTray target)
        {
            double height = source.get_Parameter(BuiltInParameter.RBS_CABLETRAY_HEIGHT_PARAM).AsDouble();
            target.get_Parameter(BuiltInParameter.RBS_CABLETRAY_HEIGHT_PARAM).Set(height);
            double width = source.get_Parameter(BuiltInParameter.RBS_CABLETRAY_WIDTH_PARAM).AsDouble();
            target.get_Parameter(BuiltInParameter.RBS_CABLETRAY_WIDTH_PARAM).Set(width);
        }
        public void CopyParameters3(Conduit source, Conduit target)
        {
            double diameter = source.get_Parameter(BuiltInParameter.RBS_CONDUIT_DIAMETER_PARAM).AsDouble();
            target.get_Parameter(BuiltInParameter.RBS_CONDUIT_DIAMETER_PARAM).Set(diameter);
        }
    }
}
