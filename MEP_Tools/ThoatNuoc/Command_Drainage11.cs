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
    public class Command_Drainage11 : WPFData, IExternalCommand
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
            cls_ThoatNuoc.Id_Tee11 = null; ;

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
                                Pipe pipe_chinh = SingleData.Singleton.Instance.RevitData.Document.GetElement(r_chinh) as Pipe;
                                Pipe pipe_nhanh = SingleData.Singleton.Instance.RevitData.Document.GetElement(r_nhanh) as Pipe;
                                Connector Conn_Bottom = F.GetConnector(pipe_nhanh);

                                ElementId _levelId = pipe_chinh.ReferenceLevel.Id;
                                PipeType _pipeType = pipe_chinh.PipeType;
                                ElementId _pipeSystemType = pipe_chinh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId();

                                LocationCurve lc1 = pipe_chinh.Location as LocationCurve;
                                XYZ stPoint = lc1.Curve.GetEndPoint(0);
                                XYZ edPoint = lc1.Curve.GetEndPoint(1);

                                LocationCurve lc_nhanh = pipe_nhanh.Location as LocationCurve;
                                XYZ stPoint_nhanh = lc_nhanh.Curve.GetEndPoint(0);
                                XYZ edPoint_nhanh = lc_nhanh.Curve.GetEndPoint(1);


                                XYZ Intersec = F.CheckIntersec(stPoint, edPoint, F.GetIntersec(stPoint, edPoint, Conn_Bottom));

                                if (Intersec!= null)
                                {
                                    double kc_canthiet = pipe_nhanh.Diameter * 2;
                                    double kc0 = Conn_Bottom.Origin.DistanceTo(Intersec);

                                    double kc_keolaigan = kc0 - kc_canthiet;
                                    XYZ u_keolaigan = null;
                                    if ((stPoint_nhanh - edPoint_nhanh).Normalize().Z < 0)
                                    {
                                        u_keolaigan = stPoint_nhanh - edPoint_nhanh;
                                    }
                                    else
                                    {
                                        u_keolaigan = -stPoint_nhanh + edPoint_nhanh;
                                    }

                                    Conn_Bottom.Origin = Conn_Bottom.Origin + kc_keolaigan * u_keolaigan.Normalize();

                                    Intersec = F.GetIntersec(stPoint, edPoint, Conn_Bottom);

                                    Pipe p_new = Pipe.Create(SingleData.Singleton.Instance.RevitData.Document, pipe_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), pipe_nhanh.PipeType.Id, pipe_nhanh.ReferenceLevel.Id, Conn_Bottom.Origin, Intersec);
                                    F.CopyParameters(pipe_nhanh, p_new);
                                    double len = p_new.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();
                                    F.Case11(pipe_chinh, pipe_nhanh, stPoint, edPoint, Intersec, len, _pipeSystemType, _pipeType.Id, _levelId, p_new, F.GetConnectorFromPoint(p_new, Intersec), Conn_Bottom);
                                }

                                SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                            }
                            if (cls_ThoatNuoc.Id_Tee11 != null)
                            {

                                    SingleData.Singleton.Instance.RevitData.Transaction.Start();
                                    ElementTransformUtils.MoveElement(SingleData.Singleton.Instance.RevitData.Document, cls_ThoatNuoc.Id_Tee11, new XYZ(0.001 / 304.8, 0.001 / 304.8, 0));
                                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                            }

                        }
                        catch (Exception ex)
                        {
                            SingleData.Singleton.Instance.RevitData.Transaction.RollBack();
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
