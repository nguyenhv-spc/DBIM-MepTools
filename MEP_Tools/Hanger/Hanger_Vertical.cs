using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using System.Collections;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.IFC;
using System.Windows.Input;

namespace MEP_Tools.Hanger
{
    [Transaction(TransactionMode.Manual)]
    public class Hanger_Vertical : WPFData, IExternalCommand
    {
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Hanger_Vertical()
        {
            OKCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                ExternalCommandData commandData = clsdata.cmData;
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Application app = uiapp.Application;
                Document doc = uidoc.Document;
                Autodesk.Revit.Creation.Application creapp = app.Create;
                Func func = new Func();

                #region"Clear Data"
                clsdata.lst_Hanger_Clevis3.Clear();
                clsdata.lst_point.Clear();
                clsdata.lst_distance3.Clear();
                clsdata.lst_h3_beam_min_end.Clear();
                clsdata.lst_distance.Clear();
                clsdata.lst_distance_min.Clear();
                clsdata.lst_h1_beam.Clear();
                clsdata.lst_h1_beam_min.Clear();
                clsdata.lst_distance_start_floor.Clear();
                clsdata.lst_distance_end_floor.Clear();
                clsdata.lst_h2_beam.Clear();
                clsdata.lst_h2_beam_min.Clear();
                clsdata.lst_h3_beam_min.Clear();
                clsdata.lst_h3_beam_min_end.Clear();
                clsdata.lst_h_pick_p1_beam_min.Clear();
                clsdata.lst_h_pick_p2_beam_min.Clear();
                clsdata.lst_h_pick_p1_floor.Clear();
                clsdata.lst_h_pick_p2_floor.Clear();
                clsdata.lst_h_pick_p1_beam.Clear();
                clsdata.lst_h_pick_p2_beam.Clear();
                clsdata.lst_h1_start_floor.Clear();
                clsdata.lst_h2_start_floor.Clear();
                clsdata.lst_h1_end_floor.Clear();
                clsdata.lst_h2_end_floor.Clear();
                clsdata.lst_h1_start_beam.Clear();
                clsdata.lst_h1_end_beam.Clear();
                clsdata.lst_h2_start_beam.Clear();
                clsdata.lst_h2_end_beam.Clear();
                clsdata.lst_point_1.Clear();
                clsdata.lst_point_2.Clear();
                clsdata.lst_h1_mid_beam_min.Clear();
                clsdata.lst_h2_mid_beam_min.Clear();
                clsdata.lst_h1_mid_floor_min.Clear();
                clsdata.lst_h2_mid_floor_min.Clear();
                clsdata.lst_h2_start_beam_min.Clear();
                clsdata.lst_h1_start_beam_min.Clear();
                clsdata.lst_h1_end_beam_min.Clear();
                clsdata.lst_h2_end_beam_min.Clear();
                //clsdata.lst_linkselect.Clear();
                //clsdata.lst_select_link.Clear();
                clsdata.lst_Beam_Limited.Clear();
                clsdata.lst_Floor_Limited.Clear();
                clsdata.lst_poin_intersect.Clear();
                clsdata.lst_hanger_Ubolt.Clear();
                clsdata.lst_diameter2.Clear();
                clsdata.lst_poin_intersect.Clear();
                clsdata.lst_id_hanger.Clear();
                clsdata.lst_point_duct.Clear();
            #endregion
                try
                {
                    if (clsdata.Check_veri == "Over")
                    {
                        Reference r = null;
                        p.Hide();
                        SingleData.Singleton.Instance.RevitData.Transaction.Start();
                        if (clsdata.index_cbb == 0) // Place hanger pick point
                        {
                            if (clsdata.index_page == 0)
                            {
                                r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select");
                                clsdata.pick_point = r.GlobalPoint;
                                Element element = doc.GetElement(r);
                                func.Get_Offset_Hanger(commandData, 6); // Load family  
                                func.Get_Offset_Hanger(commandData, 7); // Load family  
                                func.Infor_Element(commandData, element, 0);
                                XYZ picked1 = uidoc.Selection.PickPoint();
                                XYZ picked2 = uidoc.Selection.PickPoint();
                                XYZ p_center = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z);
                                XYZ p_chen = func.Get_Point_Circle(commandData, picked1, picked2, p_center);
                                //func.Place_Family_Pick_point(commandData, element, clsdata.pick_point, 9);
                                func.Place_Family_Pick_point(commandData, element, p_chen, 12);
                                Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element, 7);
                                func.Place_Family_Pick_point(commandData, element, clsdata.Point_start, 10);
                                Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang1, element, 8);
                            }
                            else
                            {
                                r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select");
                                clsdata.pick_point = r.GlobalPoint;
                                Element element = doc.GetElement(r);
                                func.Get_Offset_Hanger(commandData, 8); // Load family  
                                func.Infor_Element(commandData, element, 4);
                                clsdata.angle_duct = func.Get_direct_Duct(commandData, element, 1);
                                func.Place_Family_Pick_point(commandData, element, clsdata.Point_start, 11);
                                Infor_Hanger_Pick(commandData, clsdata.hanger_ongdung, element, 9);
                            }
                        }
                        else if (clsdata.index_cbb == 1)
                        {
                            r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Face, "Select");
                            GeometryObject geometryObject = doc.GetElement(r).GetGeometryObjectFromReference(r);
                            Face face = geometryObject as Face;
                            XYZ normal = face.ComputeNormal(new UV(0, 0));
                            Element element = doc.GetElement(r);
                            // Offset curve copies for visibility.                  
                            // The normal of the wall external face.              
                            // Offset curve copies for visibility.

                            Transform offset = Transform.CreateTranslation(
                              5 * normal);

                            // If the curve loop direction is counter-
                            // clockwise, change its color to RED.

                            Color colorRed = new Color(255, 0, 0);

                            // Get edge loops as curve loops.

                            IList<CurveLoop> curveLoops
                              = face.GetEdgesAsCurveLoops();

                            // ExporterIFCUtils class can also be used for 
                            // non-IFC purposes. The SortCurveLoops method 
                            // sorts curve loops (edge loops) so that the 
                            // outer loops come first.

                            IList<IList<CurveLoop>> curveLoopLoop
                              = ExporterIFCUtils.SortCurveLoops(
                                curveLoops);
                            foreach (IList<CurveLoop> curveLoops2 in curveLoopLoop)
                            {
                                foreach (CurveLoop curveLoop2 in curveLoops2)
                                {

                                    bool isCCW = curveLoop2.IsCounterclockwise(normal);

                                    CurveArray curves = creapp.NewCurveArray();
                                    foreach (Curve curve in curveLoop2)
                                    {
                                        curves.Append(curve.CreateTransformed(offset));
                                        XYZ start = curve.GetEndPoint(0);
                                        XYZ end = curve.GetEndPoint(1);
                                        clsdata.lst_point_duct.Add(start);
                                        clsdata.lst_point_duct.Add(end);
                                    }

                                }
                            }

                            clsdata.pick_point = r.GlobalPoint;

                            func.Get_Offset_Hanger(commandData, 8); // Load family  
                            func.Infor_Element(commandData, element, 4);
                            clsdata.angle_duct = func.Get_direct_Duct(commandData, element, 2);
                            func.Place_Family_Pick_point(commandData, element, clsdata.Point_start, 11);
                            Infor_Hanger_Pick(commandData, clsdata.hanger_ongdung, element, 9);
                        }
                        else if (clsdata.index_cbb == 2)
                        {
                            IList<Reference> pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids = (from Reference rr in pickedObjs select rr.ElementId).ToList();

                            if (pickedObjs != null && pickedObjs.Count > 0)
                            {
                                Element element1 = doc.GetElement(ids[0]);
                                clsdata.pipe1 = element1;
                                Element element2 = doc.GetElement(ids[1]);
                                func.Infor_Element(commandData, element1, 1);
                                func.Infor_Element(commandData, element2, 2);
                                clsdata.pick_point = pickedObjs[1].GlobalPoint;
                                func.Get_Offset_Hanger(commandData, 6); // Load family 
                                clsdata.picked1 = uidoc.Selection.PickPoint();
                                clsdata.picked2 = uidoc.Selection.PickPoint();
                                func.Get_two_pipe(commandData, element1, element2, 2);
                                func.Get_two_pipe(commandData, element1, element2, 3);
                                if (clsdata.picked1.DistanceTo(clsdata.mid_point1) < clsdata.picked2.DistanceTo(clsdata.mid_point2))
                                {
                                    func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point1, 6);// Place hanger paralell
                                    Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element1, 6);
                                    func.Get_Offset_Hanger(commandData, 7); // Load family 
                                    func.Place_Family_Pick_point(commandData, element1, clsdata.p_center_p1, 7);// Place Ubult
                                                                                                                // 
                                    Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang1, element1, 3);
                                    Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang2, element1, 4);

                                }
                                else
                                {
                                    func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point1, 6);// Place hanger paralell
                                    Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element1, 6);
                                    func.Get_Offset_Hanger(commandData, 7); // Load family 
                                    func.Place_Family_Pick_point(commandData, element1, clsdata.p_center_p1, 7);// Place Ubult
                                                                                                                // 
                                    Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang1, element1, 3);
                                    Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang2, element1, 4);
                                }
                                clsdata.lst_id_hanger.Add(clsdata.ubolt_ngang1.Id);
                                clsdata.lst_id_hanger.Add(clsdata.ubolt_ngang2.Id);

                            }
                            IList<Reference> pickedObjs1 = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids1 = (from Reference rr in pickedObjs1 select rr.ElementId).ToList();
                            if (pickedObjs1 != null && pickedObjs1.Count > 0)
                            {
                                foreach (ElementId eid in ids1)
                                {
                                    Element element = doc.GetElement(eid);
                                    clsdata.lst_MidPipe.Add(element);
                                    double Diameter = element.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                                    clsdata.lst_diameter2.Add(Diameter);
                                    Line pip_location = (element.Location as LocationCurve).Curve as Line;
                                    XYZ start = pip_location.GetEndPoint(0);
                                    XYZ end = pip_location.GetEndPoint(1);
                                    XYZ point = new XYZ(start.X, start.Y, clsdata.pick_point.Z);
                                    clsdata.lst_poin_intersect.Add(point);
                                }



                                //func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 8);// Place Ubult
                                //Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }
                        }
                        SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                        SingleData.Singleton.Instance.RevitData.Transaction.Start();    
                        if (clsdata.index_page == 0 && clsdata.index_cbb == 2)
                        {
                            XYZ mid1 = (clsdata.p_center_p1 + clsdata.p_center_p2) / 2;
                            XYZ mid2 = (clsdata.picked1 + clsdata.picked2) / 2;
                            if (mid2.DistanceTo(mid1) < mid2.DistanceTo(clsdata.mid_point1))
                            {
                                FamilyInstance hanger = clsdata.Hanger_pick as FamilyInstance;
                                XYZ vector = (clsdata.p_center_p1 - clsdata.p_center_p2).Normalize();
                                XYZ vector2 = new XYZ(-vector.Y, vector.X, vector.Z);
                                XYZ mid_point = (clsdata.p_center_p1 + clsdata.p_center_p2) / 2;
                                Plane plane = Plane.CreateByNormalAndOrigin(vector2, mid_point);
                                clsdata.lst_id_hanger.Add(hanger.Id);
                                ElementTransformUtils.MirrorElements(doc, clsdata.lst_id_hanger, plane, false);
                                LocationPoint local = clsdata.ubolt_ngang1.Location as LocationPoint;
                                clsdata.copy_rotate = local.Rotation;
                                func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 13);// Place Ubult
                                Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }
                            else
                            {
                                func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 8);// Place Ubult
                                Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }

                        }                      
                        SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                        p.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                    p.ShowDialog();
                }


            });
            CancelCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Close();
            });
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            clsdata.cmData = commandData;
            SingleData.Singleton.Instance = new SingleData.Singleton();
            SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;
            Func func = new Func();
            func.Get_Link_revit(commandData);
            if (cls_Reg.Login == "Login")
            {
                SingleData.Singleton.Instance.WFData.InputWindow_HangerV.ShowDialog();
            }
                
            return Result.Succeeded;
        }

        public void Infor_Hanger_Pick(ExternalCommandData commandData, Element hanger_pick, Element element, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Func func = new Func();

            switch (k)
            {
                case 3:
                    var R1 = hanger_pick.GetParameters("R");
                    R1[0].Set(clsdata.Diameter1 / 2);
                    var L1 = hanger_pick.GetParameters("L");
                    L1[0].Set(clsdata.Diameter1/2+40/304.8);
                    var Diameter3 = hanger_pick.GetParameters("Diameter");
                    Diameter3[0].Set(clsdata.Diameter_ublot / 304.8);
                    break;
                case 4:
                    var R2 = hanger_pick.GetParameters("R");
                    R2[0].Set(clsdata.Diameter2 / 2);
                    var L2 = hanger_pick.GetParameters("L");
                    L2[0].Set(clsdata.Diameter2 / 2 + 40 / 304.8);
                    var Diameter4 = hanger_pick.GetParameters("Diameter");
                    Diameter4[0].Set(clsdata.Diameter_ublot / 304.8);
                    break;
                case 6:
                    var L6 = hanger_pick.GetParameters("Chiều dài thanh U");
                    L6[0].Set(clsdata.distancee);
                    var B6 = hanger_pick.GetParameters("B");
                    clsdata.width_hanger = B6[0].AsDouble();
                    break;
                case 7:
                    var L7 = hanger_pick.GetParameters("Chiều dài thanh U");
                    L7[0].Set(clsdata.Diameter * 2);
                    var B7 = hanger_pick.GetParameters("B");
                    clsdata.width_hanger = B7[0].AsDouble();
                    break;
                case 8:
                    var R8 = hanger_pick.GetParameters("R");
                    R8[0].Set(clsdata.Diameter / 2);
                    var L8 = hanger_pick.GetParameters("L");
                    L8[0].Set(clsdata.Diameter);
                    var Diameter8 = hanger_pick.GetParameters("Diameter");
                    Diameter8[0].Set(clsdata.Diameter_ublot / 304.8);
                    break;
                case 9:
                    if (clsdata.grap_width == 1)
                    {
                        var H9 = hanger_pick.GetParameters("H");
                        H9[0].Set(clsdata.height_duct);
                        var W9 = hanger_pick.GetParameters("W");
                        W9[0].Set(clsdata.width_duct);
                    }
                    else
                    {
                        var H9 = hanger_pick.GetParameters("H");
                        H9[0].Set(clsdata.width_duct);
                        var W9 = hanger_pick.GetParameters("W");
                        W9[0].Set(clsdata.height_duct);
                    }

                    var L9_2 = hanger_pick.GetParameters("L2");
                    L9_2[0].Set(clsdata.distance_wall / 304.8);
                    break;
                default:
                    break;
            }


        }
        public void Infor_Hanger_lst_Ubolt(ExternalCommandData commandData, List<Element> lst_Ubolt)
        {
            for (int i = 0; i < lst_Ubolt.Count; i++)
            {
                var R = lst_Ubolt[i].GetParameters("R");
                R[0].Set(clsdata.lst_diameter2[i] / 2);
                var L = lst_Ubolt[i].GetParameters("L");
                L[0].Set(clsdata.lst_diameter2[i]/2+40/304.8);
                var Diameter3 = lst_Ubolt[i].GetParameters("Diameter");
                Diameter3[0].Set(clsdata.Diameter_ublot / 304.8);
            }

        }
    }
}
