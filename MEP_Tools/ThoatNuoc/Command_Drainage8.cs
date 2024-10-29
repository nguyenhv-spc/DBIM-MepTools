#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace MEP_Tools.ThoatNuoc
{
    [Transaction(TransactionMode.Manual)]
    public class Command_Drainage8 : WPFData, IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SingleData.Singleton.Instance = new SingleData.Singleton();
            SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;
            if (cls_Reg.Login == "Login")
            {
               
               Run();
            }
            return Result.Succeeded;
        }
        public void Run()
        {
            Reference r_nhanh = null;
            Reference r_chinh = null;
            cls_ThoatNuoc.Id_Tee8 = null;

            try
            {
                r_nhanh = SingleData.Singleton.Instance.RevitData.Selection.PickObject(ObjectType.Element, "Please choose branch pipe");
                if (r_nhanh != null)
                {
                    Func_ThoatNuoc F = new Func_ThoatNuoc();
                    try
                    {
                        r_chinh = SingleData.Singleton.Instance.RevitData.Selection.PickObject(ObjectType.Element, "Please choose main pipe");
                        try
                        {
                            if (r_chinh != null)
                            {

                                SingleData.Singleton.Instance.RevitData.Transaction.Start();
                                Pipe pipe_nhanh = SingleData.Singleton.Instance.RevitData.Document.GetElement(r_nhanh) as Pipe;
                                Connector Conn_Bottom = F.GetConnector(pipe_nhanh);

                                Pipe pipe_chinh = SingleData.Singleton.Instance.RevitData.Document.GetElement(r_chinh) as Pipe;

                                ElementId _levelId_1 = pipe_chinh.ReferenceLevel.Id;
                                PipeType _pipeType_1 = pipe_chinh.PipeType;
                                ElementId _pipeSystemType_1 = pipe_chinh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId();

                                LocationCurve lc_chinh = pipe_chinh.Location as LocationCurve;
                                XYZ stPoint_chinh = lc_chinh.Curve.GetEndPoint(0);
                                XYZ edPoint_chinh = lc_chinh.Curve.GetEndPoint(1);

                                LocationCurve lc_nhanh = pipe_nhanh.Location as LocationCurve;
                                XYZ stPoint_nhanh = lc_nhanh.Curve.GetEndPoint(0);
                                XYZ edPoint_nhanh = lc_nhanh.Curve.GetEndPoint(1);

                                XYZ Intersec = F.CheckIntersec(stPoint_chinh, edPoint_chinh, F.GetIntersec(stPoint_chinh, edPoint_chinh, Conn_Bottom));

                                if (Intersec != null)
                                {
                                    if (Conn_Bottom.Origin.Z > Intersec.Z)
                                    {
                                        XYZ diem1 = new XYZ(Conn_Bottom.Origin.X, Conn_Bottom.Origin.Y, Conn_Bottom.Origin.Z);
                                        XYZ diem2 = new XYZ(Conn_Bottom.Origin.X, Conn_Bottom.Origin.Y, Intersec.Z);
                                        double kc = diem1.DistanceTo(diem2) / 3;
                                        XYZ direction = (edPoint_nhanh - stPoint_nhanh).Normalize();

                                        XYZ point_check45 = null;
                                        XYZ point1 = Conn_Bottom.Origin + direction * kc;
                                        XYZ point2 = Conn_Bottom.Origin - direction * kc;
                                        if (point1.Z < point2.Z)
                                        {
                                            point_check45 = point1;
                                        }
                                        else
                                        {
                                            point_check45 = point2;
                                        }
                                        XYZ cross = new XYZ(1, 1, 0).CrossProduct(direction);
                                        point_check45 = point_check45 - kc * cross;
                                        //new XYZ(point_check45.X, point_check45.Y, point_check45.Z - kc * cross);
                                        Pipe pcheck = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, pipe_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), pipe_nhanh.PipeType.Id, pipe_nhanh.ReferenceLevel.Id, Conn_Bottom.Origin, point_check45);
                                        F.CopyParameters(pipe_nhanh, pcheck);

                                        Connector Conn_Bottom_check = F.GetConnector(pcheck);
                                        Connector Conn_Top_check = F.GetConnectorDifferentPoint(pcheck, Conn_Bottom_check.Origin);

                                        FamilyInstance Fitting0 = SingleData.Singleton.Instance.RevitData.Document.Create.NewElbowFitting(Conn_Top_check, Conn_Bottom);

                                        XYZ Intersec_check = F.CheckIntersec(stPoint_chinh, edPoint_chinh, F.GetIntersec(stPoint_chinh, edPoint_chinh, Conn_Bottom_check));

                                        if (Intersec_check!= null)
                                        {
                                            Pipe p_new = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, pipe_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), pipe_nhanh.PipeType.Id, pipe_nhanh.ReferenceLevel.Id, Conn_Bottom_check.Origin, Intersec_check);
                                            F.CopyParameters(pipe_nhanh, p_new);
                                            double len = p_new.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();

                                            Connector Conn_p_new_intersec = F.GetConnectorFromPoint(p_new, Intersec_check);
                                            Connector Conn_p_new_check = F.GetConnectorFromPoint(p_new, Conn_Bottom_check.Origin);

                                            F.Case8(pipe_chinh, stPoint_chinh, edPoint_chinh, Intersec_check, len, _pipeSystemType_1, _pipeType_1.Id, _levelId_1, p_new, Conn_p_new_intersec, Conn_p_new_check, Conn_Bottom_check);
                                        }                                      
                                    }
                                }
                                SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                                if (cls_ThoatNuoc.Id_Tee8 != null)
                                {

                                    SingleData.Singleton.Instance.RevitData.Transaction.Start();
                                    ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, cls_ThoatNuoc.Id_Tee8, new XYZ(0.001 / 304.8, 0.001 / 304.8, 0));
                                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
                            return;
                        }
                       
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
