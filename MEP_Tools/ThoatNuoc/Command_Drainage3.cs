﻿#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Interop;
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
    public class Command_Drainage3 : WPFData, IExternalCommand
    {
        Document _doc;
        Transaction ts = null;
        TransactionGroup tsg = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SingleData.Singleton.Instance = new SingleData.Singleton();
            SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;

            tsg = SingleData.Singleton.Instance.RevitData.TransactionGroup;
            ts = SingleData.Singleton.Instance.RevitData.Transaction;
            _doc = SingleData.Singleton.Instance.RevitData.Document;

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
            cls_ThoatNuoc.Id_Tee3 = null;
            try
            {
                r_nhanh = SingleData.Singleton.Instance.RevitData.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Please choose branch pipe");
                if (r_nhanh != null)
                {
                    try
                    {
                        r_chinh = SingleData.Singleton.Instance.RevitData.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Please choose main pipe");
                        if (r_chinh != null)
                        {
                            try
                            {
                                tsg.Start(" MepTools ");
                                ts.Start();
                                Func_ThoatNuoc F = new Func_ThoatNuoc();

                                #region 'Get Value'
                                cls_ThoatNuoc.Id_Tee3 = null;

                                Pipe p_chinh = _doc.GetElement(r_chinh) as Pipe; // chinh
                                Pipe p_nhanh = _doc.GetElement(r_nhanh) as Pipe; // phu

                                ElementId _levelId = p_chinh.ReferenceLevel.Id;
                                PipeType _pipeType = p_chinh.PipeType;
                                ElementId _pipeSystemType = p_chinh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId();

                                Connector Conn_Bottom = F.GetConnector(p_nhanh);

                                LocationCurve lc = p_chinh.Location as LocationCurve;
                                XYZ stPoint = lc.Curve.GetEndPoint(0);
                                XYZ edPoint = lc.Curve.GetEndPoint(1);
                                #endregion
                                XYZ u1 = new XYZ(edPoint.X - stPoint.X, edPoint.Y - stPoint.Y, 0).Normalize();
                                LocationCurve lc2 = p_nhanh.Location as LocationCurve;
                                XYZ stPoint2 = lc2.Curve.GetEndPoint(0);
                                XYZ edPoint2 = lc2.Curve.GetEndPoint(1);
                                XYZ u2 = new XYZ(edPoint2.X - stPoint2.X, edPoint2.Y - stPoint2.Y, 0).Normalize();
                                ts.Commit();

                                if (Math.Round(u1.X, 5) == Math.Round(u2.X, 5) && Math.Round(u1.Y, 5) == Math.Round(u2.Y, 5))
                                {                                  
                                    XYZ Intersec = F.CheckIntersec(stPoint, edPoint, F.GetIntersec(stPoint, edPoint, Conn_Bottom));
                                    if (Intersec != null)
                                    {
                                        ts.Start();
                                        #region 'Get Distance and point'
                                        XYZ diem1 = new XYZ(Intersec.X, Intersec.Y, 0);
                                        XYZ diem2 = new XYZ(Conn_Bottom.Origin.X, Conn_Bottom.Origin.Y, 0);
                                        double kc = diem1.DistanceTo(diem2);
                                        #endregion
                                        #region 'CT chinh'
                                        Pipe p_new = Pipe.Create(_doc, p_nhanh.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsElementId(), p_nhanh.PipeType.Id, p_nhanh.ReferenceLevel.Id, Conn_Bottom.Origin, Intersec);
                                        F.CopyParameters(p_nhanh, p_new);
                                        double len = p_new.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();
                                        F.Case3(p_chinh, stPoint, edPoint, Intersec, len, _pipeSystemType, _pipeType.Id, _levelId, p_new, F.GetConnectorFromPoint(p_new, Intersec), Conn_Bottom);
                                        #endregion
                                        ts.Commit();
                                        //return Result.Succeeded;
                                        if (cls_ThoatNuoc.Id_Tee3 != null)
                                        {
                                            ts.Start();
                                            ElementTransformUtils.MoveElement(_doc, cls_ThoatNuoc.Id_Tee3, new XYZ(0.001 / 304.8, 0.001 / 304.8, 0));
                                            ts.Commit();
                                        }
                                    }
                                    
                                }
                                tsg.Assimilate();
                            }
                            catch (Exception ex)
                            {
                                if (ts.HasStarted()) ts.RollBack();
                                if (tsg.HasStarted()) tsg.RollBack();
                                System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
                                return;
                            }
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
