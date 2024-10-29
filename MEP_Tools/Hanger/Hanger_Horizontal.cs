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
using System.Windows.Input;

namespace MEP_Tools.Hanger
{
    [Transaction(TransactionMode.Manual)]
    public class Hanger_Horizontal : WPFData,IExternalCommand
    {
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        
        public Hanger_Horizontal()
        {
            OKCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                ExternalCommandData commandData = clsdata.cmData;
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Application app = uiapp.Application;
                Document doc = uidoc.Document;
                Func func = new Func();
                if (clsdata.lst_select_link.Count != 0)
                {
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
                    clsdata.lst_Beam_Limited.Clear();
                    clsdata.lst_Floor_Limited.Clear();
                    clsdata.lst_poin_intersect.Clear();
                    clsdata.lst_hanger_Ubolt.Clear();
                    clsdata.lst_diameter2.Clear();
                    clsdata.lst_poin_intersect.Clear();
                    #endregion
                    Reference r = null;
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        p.Hide();
                        SingleData.Singleton.Instance.RevitData.Transaction.Start();
                        //tx.Start("Start");
                        if (clsdata.index_cbb == 0) // Place hanger pick point
                        {
                            r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick point on element to place hanger:");
                            clsdata.pick_point = r.GlobalPoint;
                            Element element = doc.GetElement(r);
                            func.Infor_Element(commandData, element, 0);
                            if (clsdata.index_page == 0)
                            {
                                func.Get_Offset_Hanger(commandData, 3); // Load family                                    
                                func.Place_Family_Pick_point(commandData, element, func.Intersec_point(commandData), 0);
                                clsdata.pick_foor = func.Distance_start_floor(commandData, func.Intersec_point(commandData));
                                func.Get_h1_beam(commandData, func.Intersec_point(commandData));
                                Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element, 0);
                            }
                            else if (clsdata.index_page == 1)
                            {
                                func.Get_Offset_Hanger(commandData, 2); // Load family                                   
                                func.Place_Family_Pick_point(commandData, element, func.Intersec_point(commandData), 1);
                                clsdata.pick_p1_floor = func.h_pick_p1_floor(commandData, clsdata.pick_1);
                                clsdata.pick_p2_floor = func.h_pick_p2_floor(commandData, clsdata.pick_2);
                                func.Get_h1_beam(commandData, func.Intersec_point(commandData));
                                clsdata.pick_p1_beam = func.h_pick_p1_beam(commandData, clsdata.pick_1);
                                clsdata.pick_p2_beam = func.h_pick_p2_beam(commandData, clsdata.pick_2);
                                Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element, 1);
                            }
                        }
                        else if (clsdata.index_cbb == 3)
                        {
                            IList<Reference> pickedObjs = null;
                            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids = (from Reference rr in pickedObjs select rr.ElementId).ToList();
                            if (pickedObjs != null && pickedObjs.Count > 0)
                            {
                                Element element1 = doc.GetElement(ids[0]);
                                Element element2 = doc.GetElement(ids[1]);
                                func.Infor_Element(commandData, element1, 1);
                                func.Infor_Element(commandData, element2, 2);
                                clsdata.pick_point = pickedObjs[1].GlobalPoint;
                                func.Get_Offset_Hanger(commandData, 2); // Load family 
                                func.Get_two_pipe(commandData, element1, element2, 1);
                                func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point, 3);// Place hanger paralell
                            func.Get_Offset_Hanger(commandData, 4);
                            func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point, 4);// Place Ubult
                                Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element1, 2);
                                Infor_Hanger_Pick(commandData, clsdata.ubolt1, element1, 3);
                                Infor_Hanger_Pick(commandData, clsdata.ubolt2, element1, 4);

                            }
                            IList<Reference> pickedObjs1 = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids1 = (from Reference rr in pickedObjs1 select rr.ElementId).ToList();
                            if (pickedObjs1 != null && pickedObjs1.Count > 0)
                            {
                                foreach (ElementId eid in ids1)
                                {
                                    Element element = doc.GetElement(eid);
                                    double Diameter = element.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                                    clsdata.lst_diameter2.Add(Diameter);
                                    Line pip_location = (element.Location as LocationCurve).Curve as Line;
                                    XYZ start = pip_location.GetEndPoint(0);
                                    XYZ end = pip_location.GetEndPoint(1);
                                    XYZ intersect_point = func.intersect_twoline(commandData, start, end, clsdata.p_center_pipeshort);// Find intersection between two pipe
                                    clsdata.lst_poin_intersect.Add(intersect_point);
                                }
                                func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 5);// Place Ubult
                                Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }
                        }
                        else if (clsdata.index_cbb == 4)
                        {
                            IList<Reference> pickedObjs = null;
                            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids = (from Reference rr in pickedObjs select rr.ElementId).ToList();
                            Element element1 = doc.GetElement(ids[0]);
                            Element element2 = doc.GetElement(ids[1]);
                            func.Infor_Element(commandData, element1, 1);
                            func.Infor_Element(commandData, element2, 2);
                            func.Get_two_pipe_along(commandData, element1, element2, 1);
                            func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point, 3);// Place hanger paralell
                            Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element1, 2);
                            
                        func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point, 4);// Place Ubult
                            Infor_Hanger_Pick(commandData, clsdata.ubolt1, element1, 3);
                            Infor_Hanger_Pick(commandData, clsdata.ubolt2, element1, 4);
                            func.PlaceFamily_Paralell(commandData, element2, 1);
                            foreach (ElementId eid in ids)
                            {

                            }
                            IList<Reference> pickedObjs1 = null;
                            pickedObjs1 = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids1 = (from Reference rr in pickedObjs1 select rr.ElementId).ToList();
                            if (pickedObjs1 != null && pickedObjs1.Count > 0)
                            {
                                foreach (ElementId eid in ids1)
                                {
                                    Element element = doc.GetElement(eid);
                                    double Diameter = element.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                                    clsdata.lst_diameter2.Add(Diameter);
                                    Line pip_location = (element.Location as LocationCurve).Curve as Line;
                                    XYZ start = pip_location.GetEndPoint(0);
                                    XYZ end = pip_location.GetEndPoint(1);
                                    XYZ intersect_point = func.intersect_twoline(commandData, start, end, clsdata.p_center_pipeshort);// Find intersection between two pipe
                                    clsdata.lst_poin_intersect.Add(intersect_point);
                                }
                                func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 5);// Place Ubult
                                Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }
                        }
                        else if (clsdata.index_cbb == 6)
                        {
                            IList<Reference> pickedObjs = null;
                            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            //IList<Reference> pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids = (from Reference rr in pickedObjs select rr.ElementId).ToList();
                            if (pickedObjs != null && pickedObjs.Count > 0)
                            {
                                Element element1 = doc.GetElement(ids[0]);
                                Element element2 = doc.GetElement(ids[1]);
                                func.Infor_Element(commandData, element1, 1);
                                func.Infor_Element(commandData, element2, 2);
                                clsdata.pick_point = pickedObjs[1].GlobalPoint;
                                func.Get_Offset_Hanger(commandData, 6); // Load family 
                                func.Get_two_pipe(commandData, element1, element2, 2);
                                func.Place_Family_Pick_point(commandData, element1, clsdata.mid_point, 6);// Place hanger paralell
                                Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element1, 6);
                                func.Get_Offset_Hanger(commandData, 7); // Load family 
                                func.Place_Family_Pick_point(commandData, element1, clsdata.p_center_p1, 7);// Place Ubult
                                                                                                            // 
                                Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang1, element1, 3);
                                Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang2, element1, 4);
                            }
                            IList<Reference> pickedObjs1 = null;
                            pickedObjs1 = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            //IList<Reference> pickedObjs1 = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids1 = (from Reference rr in pickedObjs1 select rr.ElementId).ToList();
                            if (pickedObjs1 != null && pickedObjs1.Count > 0)
                            {
                                foreach (ElementId eid in ids1)
                                {
                                    Element element = doc.GetElement(eid);
                                    double Diameter = element.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                                    clsdata.lst_diameter2.Add(Diameter);
                                    Line pip_location = (element.Location as LocationCurve).Curve as Line;
                                    XYZ start = pip_location.GetEndPoint(0);
                                    XYZ end = pip_location.GetEndPoint(1);
                                    XYZ point = new XYZ(start.X, start.Y, clsdata.pick_point.Z);
                                    clsdata.lst_poin_intersect.Add(point);
                                }
                                func.Place_Family_Pick_point(commandData, clsdata.ubolt1, clsdata.mid_point, 8);// Place Ubult
                                Infor_Hanger_lst_Ubolt(commandData, clsdata.lst_hanger_Ubolt);
                            }
                        }
                        else if (clsdata.index_cbb == 7)
                        {
                            r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select");
                            //r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select");
                            clsdata.pick_point = r.GlobalPoint;
                            Element element = doc.GetElement(r);
                            func.Get_Offset_Hanger(commandData, 3); // Load family  
                            func.Infor_Element(commandData, element, 0);
                            func.Place_Family_Pick_point(commandData, element, clsdata.pick_point, 9);
                            Infor_Hanger_Pick(commandData, clsdata.Hanger_pick, element, 7);
                            func.Place_Family_Pick_point(commandData, element, clsdata.Point_start, 10);
                            Infor_Hanger_Pick(commandData, clsdata.ubolt_ngang1, element, 8);
                        }
                        else
                        {
                            IList<Reference> pickedObjs = null;
                            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            //IList<Reference> pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select elements");
                            List<ElementId> ids = (from Reference rr in pickedObjs select rr.ElementId).ToList();
                            if (pickedObjs != null && pickedObjs.Count > 0)
                            {
                                foreach (ElementId eid in ids)
                                {
                                    Element element = doc.GetElement(eid);
                                    func.Infor_Element(commandData, element, 0);
                                    if (clsdata.index_cbb == 4)// Place hanger along element
                                    {
                                        if (clsdata.index_page == 0)
                                        {
                                            func.Get_Offset_Hanger(commandData, 3); // Load family                                   
                                            PlaceFamily(commandData, element, 0);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 0); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 0);// Get infor Hanger
                                        }
                                        else if (clsdata.index_page == 1)
                                        {
                                            func.Get_Offset_Hanger(commandData, 2); // Load family                                  
                                            PlaceFamily(commandData, element, 1);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 1); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 1);
                                        }
                                    }
                                    else if (clsdata.index_cbb == 1)
                                    {
                                        if (clsdata.index_page == 0)
                                        {
                                            func.Get_Offset_Hanger(commandData, 3); // Load family                                   
                                            PlaceFamily(commandData, element, 2);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 2); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 0);// Get infor Hanger
                                        }
                                        else
                                        {
                                            func.Get_Offset_Hanger(commandData, 2); // Load family  
                                            PlaceFamily(commandData, element, 3);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 3); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 1);// Get infor Hanger
                                        }
                                    }
                                    else if (clsdata.index_cbb == 2)
                                    {
                                        if (clsdata.index_page == 0)
                                        {
                                            func.Get_Offset_Hanger(commandData, 3); // Load family  
                                            PlaceFamily(commandData, element, 4);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 4); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 0);// Get infor Hanger
                                        }
                                        else
                                        {
                                            func.Get_Offset_Hanger(commandData, 2); // Load family  
                                            PlaceFamily(commandData, element, 5);// Place Hanger start , end pipe
                                            func.PlaceFamily(commandData, element, 5); // Place Hanger mid Hanger
                                            Infor_Hanger(commandData, clsdata.Hanger_Clevis1, clsdata.Hanger_Clevis2, clsdata.lst_Hanger_Clevis3, 1);// Get infor Hanger
                                        }
                                    }
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
                                    clsdata.lst_select_link.Clear();
                                    #endregion
                                }
                            }
                        }
                        //tx.Commit();
                        SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                        //if (r != null)
                        //{
                        //    ElementId elementId = r.ElementId;
                        //    Element element = doc.GetElement(r);
                        //}
                        p.ShowDialog();
                }
                    catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                    p.ShowDialog();

                }

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
                SingleData.Singleton.Instance.WFData.InputWindow_HangerH.ShowDialog();
            }               
            return Result.Succeeded;
        }

        public void PlaceFamily(ExternalCommandData commandData, Element e, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Func func = new Func();
            double anpha = Math.PI - clsdata.rotate_element;
            double w = clsdata.width_duct / 2 + 11 / 304.8;
            // Find Family 
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
            switch (k)
            {
                case 0:
                    FamilySymbol symbol = null;
                    //FamilySymbol symbol1 = null;

                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Pipe" && sym.Name == "Standard")
                        {
                            symbol = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol.IsActive)
                    {
                        symbol.Activate();
                    }
                    if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                    {
                        XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z);
                        XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p1.Z);
                        XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z);
                        XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p2.Z);
                        clsdata.p1_1 = p1_1;
                        clsdata.p2_1 = p2_1;

                        XYZ p11 = new XYZ(p1_1.X + clsdata.diemchen, p1_1.Y, p1_1.Z);
                        XYZ p21 = new XYZ(p2_1.X + clsdata.diemchen, p2_1.Y, p2_1.Z);
                        if (true)
                        {

                        }
                        FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        clsdata.Hanger_Clevis1 = hanger1;
                        clsdata.distance1 = func.Distance_start_floor(commandData, p11);
                        clsdata.distance2 = func.Distance_end_floor(commandData, p21);
                        func.Get_h1_beam(commandData, p11);
                        func.Get_h2_beam(commandData, p21);

                        clsdata.Hanger_Clevis2 = hanger2;
                        XYZ axis_start1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z);
                        XYZ axis_end1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z + 10);
                        Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                        hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element + Math.PI);

                        XYZ axis_start2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z);
                        XYZ axis_end2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z + 10);
                        Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                        hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element + Math.PI);
                    }
                    else
                    {
                        XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z);
                        XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p1.Z);
                        XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z);
                        XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p2.Z);
                        clsdata.p1_1 = p1_1;
                        clsdata.p2_1 = p2_1;

                        XYZ p11 = new XYZ(p1_1.X + clsdata.diemchen, p1_1.Y, p1_1.Z);
                        XYZ p21 = new XYZ(p2_1.X + clsdata.diemchen, p2_1.Y, p2_1.Z);
                        FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        clsdata.Hanger_Clevis1 = hanger1;
                        clsdata.distance1 = func.Distance_start_floor(commandData, p11);
                        clsdata.distance2 = func.Distance_end_floor(commandData, p21);
                        func.Get_h1_beam(commandData, p11);
                        func.Get_h2_beam(commandData, p21);
                        clsdata.Hanger_Clevis2 = hanger2;
                        XYZ axis_start1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z);
                        XYZ axis_end1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z + 10);
                        Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                        hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element + Math.PI);

                        XYZ axis_start2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z);
                        XYZ axis_end2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z + 10);
                        Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                        hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element + Math.PI);
                    }
                    break;
                case 1:
                    if (e is Pipe)
                    {
                        FamilySymbol symbol1_1 = null;
                        

                        foreach (FamilySymbol sym in symbols)
                        {

                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_1 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_1.IsActive)
                        {
                            symbol1_1.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Z - clsdata.Diameter / 2);
                            XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Z - clsdata.Diameter / 2);
                            clsdata.p1_1 = p1_1;
                            clsdata.p2_1 = p2_1;
                            clsdata.start_1 = new XYZ(p1_1.X - Math.Sin(anpha) * w, p1_1.Y + Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X + Math.Sin(anpha) * w, p1_1.Y - Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.end_1 = new XYZ(p2_1.X - Math.Sin(anpha) * w, p2_1.Y + Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X + Math.Sin(anpha) * w, p2_1.Y - Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            func.Get_h1_beam(commandData, p1_1);
                            func.Get_h2_beam(commandData, p2_1);

                            clsdata.Hanger_Clevis2 = hanger2;
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                        else
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p1.Z - clsdata.Diameter / 2);
                            XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p2.Z - clsdata.Diameter / 2);
                            clsdata.p1_1 = p1_1;
                            clsdata.p2_1 = p2_1;
                            clsdata.start_1 = new XYZ(p1_1.X + Math.Abs(Math.Sin(anpha)) * w, p1_1.Y + Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X - Math.Abs(Math.Sin(anpha)) * w, p1_1.Y - Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.end_1 = new XYZ(p2_1.X + Math.Abs(Math.Sin(anpha)) * w, p2_1.Y + Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X - Math.Abs(Math.Sin(anpha)) * w, p2_1.Y - Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            func.Get_h1_beam(commandData, p1_1);
                            func.Get_h2_beam(commandData, p2_1);
                            clsdata.Hanger_Clevis2 = hanger2;
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        FamilySymbol symbol1_2 = null;

                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_2 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_2.IsActive)
                        {
                            symbol1_2.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p1.Z);
                            XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p2.Z);
                            clsdata.p1_1 = p1_1;
                            clsdata.p2_1 = p2_1;
                            clsdata.start_1 = new XYZ(p1_1.X - Math.Sin(anpha) * w, p1_1.Y + Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X + Math.Sin(anpha) * w, p1_1.Y - Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.end_1 = new XYZ(p2_1.X - Math.Sin(anpha) * w, p2_1.Y + Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X + Math.Sin(anpha) * w, p2_1.Y - Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            func.Get_h1_beam(commandData, p1_1);
                            func.Get_h2_beam(commandData, p2_1);

                            clsdata.Hanger_Clevis2 = hanger2;
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                        else
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p1.Z);
                            XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p2.Z);
                            clsdata.p1_1 = p1_1;
                            clsdata.p2_1 = p2_1;
                            clsdata.start_1 = new XYZ(p1_1.X + Math.Abs(Math.Sin(anpha)) * w, p1_1.Y + Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X - Math.Abs(Math.Sin(anpha)) * w, p1_1.Y - Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.end_1 = new XYZ(p2_1.X + Math.Abs(Math.Sin(anpha)) * w, p2_1.Y + Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X - Math.Abs(Math.Sin(anpha)) * w, p2_1.Y - Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            func.Get_h1_beam(commandData, p1_1);
                            func.Get_h2_beam(commandData, p2_1);
                            clsdata.Hanger_Clevis2 = hanger2;
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                    }
                    break;
                case 2:
                    FamilySymbol symbol2 = null;
                    

                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Pipe" && sym.Name == "Standard")
                        {
                            symbol2 = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol2.IsActive)
                    {
                        symbol2.Activate();
                    }
                    if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                    {
                        XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z);
                        XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p1.Z);                     
                        clsdata.p1_1 = p1_1;                      
                        XYZ p11 = new XYZ(p1_1.X + clsdata.diemchen, p1_1.Y, p1_1.Z);                                          
                        FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);                    
                        clsdata.Hanger_Clevis1 = hanger1;
                        clsdata.distance1 = func.Distance_start_floor(commandData, p11);                      
                        func.Get_h1_beam(commandData, p11);
                        

                        
                        XYZ axis_start1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z);
                        XYZ axis_end1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z + 10);
                        Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                        hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element + Math.PI);
                      
                    }
                    else
                    {
                        XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z);
                        XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p1.Z);
                         clsdata.p1_1 = p1_1;
                        

                        XYZ p11 = new XYZ(p1_1.X + clsdata.diemchen, p1_1.Y, p1_1.Z);
                        
                        FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                         clsdata.Hanger_Clevis1 = hanger1;
                        clsdata.distance1 = func.Distance_start_floor(commandData, p11);                     
                        func.Get_h1_beam(commandData, p11);                                           
                        XYZ axis_start1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z);
                        XYZ axis_end1 = new XYZ(p1_1.X + 335.5 / 304.8, p1_1.Y, p1_1.Z + 10);
                        Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                        hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element + Math.PI);
                       
                   }
                    break;
                case 3:
                    if (e is Pipe)
                    {
                        FamilySymbol symbol1_1 = null;


                        foreach (FamilySymbol sym in symbols)
                        {

                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_1 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_1.IsActive)
                        {
                            symbol1_1.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Z - clsdata.Diameter / 2);
                           clsdata.p1_1 = p1_1;
                           
                            clsdata.start_1 = new XYZ(p1_1.X - Math.Sin(anpha) * w, p1_1.Y + Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X + Math.Sin(anpha) * w, p1_1.Y - Math.Cos(anpha) * w, p1_1.Z);
                           clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);
                            func.Get_h1_beam(commandData, p1_1);
                                                     
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);
                           
                        }
                        else
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p1.Z - clsdata.Diameter / 2);
                           clsdata.p1_1 = p1_1;                        
                            clsdata.start_1 = new XYZ(p1_1.X + Math.Abs(Math.Sin(anpha)) * w, p1_1.Y + Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X - Math.Abs(Math.Sin(anpha)) * w, p1_1.Y - Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                            
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                           
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                           clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);                          
                            func.Get_h1_beam(commandData, p1_1);                                                   
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        FamilySymbol symbol1_2 = null;

                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_2 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_2.IsActive)
                        {
                            symbol1_2.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Z);
                           clsdata.p1_1 = p1_1;
                          
                            clsdata.start_1 = new XYZ(p1_1.X - Math.Sin(anpha) * w, p1_1.Y + Math.Cos(anpha) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X + Math.Sin(anpha) * w, p1_1.Y - Math.Cos(anpha) * w, p1_1.Z);
                            
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);                          
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);                        
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);                        
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);                          
                            func.Get_h1_beam(commandData, p1_1);
                                                    
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);

                           
                        }
                        else
                        {
                            XYZ p1 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.Point_start.Z - clsdata.height_duct / 2);
                            XYZ p1_1 = new XYZ(p1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p1.Z);
                            clsdata.p1_1 = p1_1;                         
                            clsdata.start_1 = new XYZ(p1_1.X + Math.Abs(Math.Sin(anpha)) * w, p1_1.Y + Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);
                            clsdata.start_2 = new XYZ(p1_1.X - Math.Abs(Math.Sin(anpha)) * w, p1_1.Y - Math.Abs(Math.Cos(anpha)) * w, p1_1.Z);                         
                            clsdata.h1_start_floor = func.h1_start_floor(commandData, clsdata.start_1);
                            clsdata.h2_start_floor = func.h2_start_floor(commandData, clsdata.start_2);
                           
                            clsdata.h1_start_beam = func.h1_start_beam(commandData, clsdata.start_1);
                            clsdata.h2_start_beam = func.h2_start_beam(commandData, clsdata.start_2);
                           
                            FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p1_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            
                            clsdata.Hanger_Clevis1 = hanger1;
                            clsdata.distance1 = func.Distance_start_floor(commandData, p1_1);                        
                            func.Get_h1_beam(commandData, p1_1);                                                 
                            XYZ axis_start1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z);
                            XYZ axis_end1 = new XYZ(p1_1.X, p1_1.Y, p1_1.Z + 10);
                            Line axis1 = Line.CreateBound(axis_start1, axis_end1);
                            hanger1.Location.Rotate(axis1, Math.PI - clsdata.rotate_element - Math.PI / 2);                         
                        }
                    }

                    break;
                case 4:
                    FamilySymbol symbol4 = null;
                    

                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Pipe" && sym.Name == "Standard")
                        {
                            symbol4 = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol4.IsActive)
                    {
                        symbol4.Activate();
                    }
                    if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                    {
                       XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z);
                        XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p2.Z);
                        
                        clsdata.p2_1 = p2_1;
                   
                        XYZ p21 = new XYZ(p2_1.X + clsdata.diemchen, p2_1.Y, p2_1.Z);
                        if (true)
                        {

                        }
                        FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol4, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                       clsdata.distance2 = func.Distance_end_floor(commandData, p21);
                        func.Get_h2_beam(commandData, p21);
                        clsdata.Hanger_Clevis2 = hanger2;                     
                        XYZ axis_start2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z);
                        XYZ axis_end2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z + 10);
                        Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                        hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element + Math.PI);
                    }
                    else
                    {
                       XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z);
                        XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8) - 335.5 / 304.8, p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * clsdata.A / 304.8), p2.Z);                     
                        clsdata.p2_1 = p2_1;
                       
                        XYZ p21 = new XYZ(p2_1.X + clsdata.diemchen, p2_1.Y, p2_1.Z);
                        
                        FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol4, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                      
                        
                        clsdata.distance2 = func.Distance_end_floor(commandData, p21);
                      
                        func.Get_h2_beam(commandData, p21);
                        clsdata.Hanger_Clevis2 = hanger2;
                       

                        XYZ axis_start2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z);
                        XYZ axis_end2 = new XYZ(p2_1.X + 335.5 / 304.8, p2_1.Y, p2_1.Z + 10);
                        Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                        hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element + Math.PI);
                    }
                    break;
                case 5:
                    if (e is Pipe)
                    {
                        FamilySymbol symbol1_1 = null;


                        foreach (FamilySymbol sym in symbols)
                        {

                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_1 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_1.IsActive)
                        {
                            symbol1_1.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Z - clsdata.Diameter / 2);
                           
                            clsdata.p2_1 = p2_1;
                           clsdata.end_1 = new XYZ(p2_1.X - Math.Sin(anpha) * w, p2_1.Y + Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X + Math.Sin(anpha) * w, p2_1.Y - Math.Cos(anpha) * w, p2_1.Z);
                           
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                           
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                            
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                          
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            
                            func.Get_h2_beam(commandData, p2_1);

                            clsdata.Hanger_Clevis2 = hanger2;
                         

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                        else
                        {
                           XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Z - clsdata.Diameter / 2);
                           
                            clsdata.p2_1 = p2_1;
                           
                            clsdata.end_1 = new XYZ(p2_1.X + Math.Abs(Math.Sin(anpha)) * w, p2_1.Y + Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X - Math.Abs(Math.Sin(anpha)) * w, p2_1.Y - Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                           
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                           
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                           
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_1, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                          
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                            
                            func.Get_h2_beam(commandData, p2_1);
                            clsdata.Hanger_Clevis2 = hanger2;
                          

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        FamilySymbol symbol1_2 = null;

                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol1_2 = sym as FamilySymbol;
                                break;
                            }
                        }

                        if (!symbol1_2.IsActive)
                        {
                            symbol1_2.Activate();
                        }
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                           XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Z);
                          
                            clsdata.p2_1 = p2_1;
                           
                            clsdata.end_1 = new XYZ(p2_1.X - Math.Sin(anpha) * w, p2_1.Y + Math.Cos(anpha) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X + Math.Sin(anpha) * w, p2_1.Y - Math.Cos(anpha) * w, p2_1.Z);
                           
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                           
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                           
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                           
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                           
                            func.Get_h2_beam(commandData, p2_1);

                            clsdata.Hanger_Clevis2 = hanger2;
                          

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                        else
                        {
                           XYZ p2 = new XYZ(clsdata.Point_end.X, clsdata.Point_end.Y, clsdata.Point_end.Z - clsdata.height_duct / 2);
                            XYZ p2_1 = new XYZ(p2.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A ) / 304.8), p2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p2.Z);
                          
                            clsdata.p2_1 = p2_1;
                           
                            clsdata.end_1 = new XYZ(p2_1.X + Math.Abs(Math.Sin(anpha)) * w, p2_1.Y + Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                            clsdata.end_2 = new XYZ(p2_1.X - Math.Abs(Math.Sin(anpha)) * w, p2_1.Y - Math.Abs(Math.Cos(anpha)) * w, p2_1.Z);
                           
                            clsdata.h1_end_floor = func.h1_end_floor(commandData, clsdata.end_1);
                            clsdata.h2_end_floor = func.h2_end_floor(commandData, clsdata.end_2);
                           
                            clsdata.h1_end_beam = func.h1_end_beam(commandData, clsdata.end_1);
                            clsdata.h2_end_beam = func.h2_end_beam(commandData, clsdata.end_2);
                           
                            FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2_1, symbol1_2, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                            
                            
                            clsdata.distance2 = func.Distance_end_floor(commandData, p2_1);
                          
                            func.Get_h2_beam(commandData, p2_1);
                            clsdata.Hanger_Clevis2 = hanger2;
                           

                            XYZ axis_start2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z);
                            XYZ axis_end2 = new XYZ(p2_1.X, p2_1.Y, p2_1.Z + 10);
                            Line axis2 = Line.CreateBound(axis_start2, axis_end2);
                            hanger2.Location.Rotate(axis2, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        }
                    }
                    break;
                case 6:

                    break;
                default:
                    break;
            }
             
              
            




        }
        public void Infor_Hanger(ExternalCommandData commandData, Element hanger1,Element hanger2,List<Element> lst_hanger3,int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Func func = new Func();
            switch (k)
            {
                case 0:
                   
                    if (clsdata.index_cbb==2)
                    {
                       
                        var Angle2 = hanger2.GetParameters("Duct Diameter");
                        Angle2[0].Set(clsdata.Diameter);


                        var Over2 = hanger2.GetParameters("KC tâm ống tới trần");

                        if (clsdata.lst_h2_beam_min.Count == 0)
                        {
                            Over2[0].Set(clsdata.distance2 - clsdata.h_hanger);
                        }
                        else
                        {
                            Over2[0].Set(clsdata.lst_h2_beam_min.Min() - clsdata.h_hanger);
                        }
                    }
                    else
                    {
                        var Angle1 = hanger1.GetParameters("Duct Diameter");
                        Angle1[0].Set(clsdata.Diameter);


                        var Over1 = hanger1.GetParameters("KC tâm ống tới trần");

                        if (clsdata.lst_h1_beam_min.Count == 0)
                        {
                            Over1[0].Set(clsdata.distance1 - clsdata.h_hanger);
                        }
                        else
                        {
                            Over1[0].Set(clsdata.lst_h1_beam_min.Min() - clsdata.h_hanger);
                        }
                    }    

                   
                    foreach (Element hanger in lst_hanger3)
                    {


                        var Angle = hanger.GetParameters("Duct Diameter");
                        Angle[0].Set(clsdata.Diameter);




                    }       
                    for (int i = 0; i < lst_hanger3.Count; i++)
                    {
                        var Over = lst_hanger3[i].GetParameters("KC tâm ống tới trần");

                        if (clsdata.lst_h3_beam_min_end[i] > clsdata.lst_distance3[i])
                        {
                            Over[0].Set(clsdata.lst_distance3[i] - clsdata.h_hanger);
                        }
                        else
                        {
                            Over[0].Set(clsdata.lst_h3_beam_min_end[i] - clsdata.h_hanger);
                        }

                    }
                    break;
                case 1:
                    //double a = 30 / 304.8;
                  

                    if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                    {
                        
                        if (clsdata.index_cbb ==2)
                        {
                            var length2 = hanger2.GetParameters("Chiều dài thanh U");
                            length2[0].Set(clsdata.width_duct + 55 / 304.8);
                            var h2_1 = hanger2.GetParameters("H1");

                            if (clsdata.lst_h1_end_beam_min.Count == 0)
                            {
                                h2_1[0].Set(clsdata.h1_end_floor - 30 / 304.8);
                            }
                            else
                            {
                                h2_1[0].Set(clsdata.lst_h1_end_beam_min.Min() - 30 / 304.8);
                            }

                            var h2_2 = hanger2.GetParameters("H2");

                            if (clsdata.lst_h2_end_beam_min.Count == 0)
                            {
                                h2_2[0].Set(clsdata.h2_end_floor - 30 / 304.8);
                            }
                            else
                            {
                                h2_2[0].Set(clsdata.lst_h2_end_beam_min.Min() - 30 / 304.8);
                            }
                        }
                        else
                        {
                            var length1 = hanger1.GetParameters("Chiều dài thanh U");
                            length1[0].Set(clsdata.width_duct + 55 / 304.8);
                            var h1_1 = hanger1.GetParameters("H1");

                            if (clsdata.lst_h1_start_beam_min.Count == 0)
                            {
                                h1_1[0].Set(clsdata.h1_start_floor - 30 / 304.8);
                            }
                            else
                            {
                                h1_1[0].Set(clsdata.lst_h1_start_beam_min.Min() - 30 / 304.8);
                            }
                            var h1_2 = hanger1.GetParameters("H2");

                            if (clsdata.lst_h2_start_beam_min.Count == 0)
                            {
                                h1_2[0].Set(clsdata.h2_start_floor - 30 / 304.8);
                            }
                            else
                            {
                                h1_2[0].Set(clsdata.lst_h2_start_beam_min.Min() - 30 / 304.8);
                            }
                        }    
                        
                        for (int i = 0; i < lst_hanger3.Count; i++)
                        {

                            if (clsdata.rotate_element < Math.PI)
                            {
                                if (clsdata.Point_start.Y == clsdata.Point_end.Y)
                                {
                                    var h11 = lst_hanger3[i].GetParameters("H2");
                                    if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                    {
                                        h11[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h11[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                    }

                                    var h22 = lst_hanger3[i].GetParameters("H1");

                                    if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                    {
                                        h22[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h22[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                    }
                                }
                                else
                                {
                                    var h1 = lst_hanger3[i].GetParameters("H1");
                                    if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                    {
                                        h1[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h1[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                    }

                                    var h2 = lst_hanger3[i].GetParameters("H2");

                                    if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                    {
                                        h2[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h2[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                    }
                                }  
                               
                            }
                            else
                            {
                                if (clsdata.Point_start.Y == clsdata.Point_end.Y)
                                {
                                    var h11 = lst_hanger3[i].GetParameters("H1");
                                    if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                    {
                                        h11[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h11[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                    }

                                    var h22 = lst_hanger3[i].GetParameters("H2");

                                    if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                    {
                                        h22[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h22[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                    }
                                }
                                else
                                {
                                    var h1 = lst_hanger3[i].GetParameters("H2");
                                    if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                    {
                                        h1[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h1[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                    }

                                    var h2 = lst_hanger3[i].GetParameters("H1");

                                    if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                    {
                                        h2[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                    }
                                    else
                                    {
                                        h2[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                    }
                                }    
                                
                            }


                        }
                    }
                    else
                    {
                        if (clsdata.rotate_element <= Math.PI/2)
                        {
                            
                            if (clsdata.index_cbb == 2)
                            {
                                var length2 = hanger2.GetParameters("Chiều dài thanh U");
                                length2[0].Set(clsdata.width_duct + 55 / 304.8);
                                var h2_1 = hanger2.GetParameters("H2");

                                if (clsdata.lst_h1_end_beam_min.Count == 0)
                                {
                                    h2_1[0].Set(clsdata.h1_end_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h2_1[0].Set(clsdata.lst_h1_end_beam_min.Min() - 30 / 304.8);
                                }

                                var h2_2 = hanger2.GetParameters("H1");

                                if (clsdata.lst_h2_end_beam_min.Count == 0)
                                {
                                    h2_2[0].Set(clsdata.h2_end_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h2_2[0].Set(clsdata.lst_h2_end_beam_min.Min() - 30 / 304.8);
                                }
                            }
                            else
                            {
                                var length1 = hanger1.GetParameters("Chiều dài thanh U");
                                length1[0].Set(clsdata.width_duct + 55 / 304.8);
                                var h1_1 = hanger1.GetParameters("H2");

                                if (clsdata.lst_h1_start_beam_min.Count == 0)
                                {
                                    h1_1[0].Set(clsdata.h1_start_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h1_1[0].Set(clsdata.lst_h1_start_beam_min.Min() - 30 / 304.8);
                                }
                                var h1_2 = hanger1.GetParameters("H1");

                                if (clsdata.lst_h2_start_beam_min.Count == 0)
                                {
                                    h1_2[0].Set(clsdata.h2_start_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h1_2[0].Set(clsdata.lst_h2_start_beam_min.Min() - 30 / 304.8);
                                }
                            }    
                        }
                        else
                        {
                            
                            if (clsdata.index_cbb == 2)
                            {
                                var length2 = hanger2.GetParameters("Chiều dài thanh U");
                                length2[0].Set(clsdata.width_duct + 55 / 304.8);
                                var h2_1 = hanger2.GetParameters("H1");

                                if (clsdata.lst_h1_end_beam_min.Count == 0)
                                {
                                    h2_1[0].Set(clsdata.h1_end_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h2_1[0].Set(clsdata.lst_h1_end_beam_min.Min() - 30 / 304.8);
                                }

                                var h2_2 = hanger2.GetParameters("H2");

                                if (clsdata.lst_h2_end_beam_min.Count == 0)
                                {
                                    h2_2[0].Set(clsdata.h2_end_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h2_2[0].Set(clsdata.lst_h2_end_beam_min.Min() - 30 / 304.8);
                                }
                            }
                            else
                            {
                                var length1 = hanger1.GetParameters("Chiều dài thanh U");
                                length1[0].Set(clsdata.width_duct + 55 / 304.8);
                                var h1_1 = hanger1.GetParameters("H1");

                                if (clsdata.lst_h1_start_beam_min.Count == 0)
                                {
                                    h1_1[0].Set(clsdata.h1_start_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h1_1[0].Set(clsdata.lst_h1_start_beam_min.Min() - 30 / 304.8);
                                }
                                var h1_2 = hanger1.GetParameters("H2");

                                if (clsdata.lst_h2_start_beam_min.Count == 0)
                                {
                                    h1_2[0].Set(clsdata.h2_start_floor - 30 / 304.8);
                                }
                                else
                                {
                                    h1_2[0].Set(clsdata.lst_h2_start_beam_min.Min() - 30 / 304.8);
                                }
                            }    
                        }
                        for (int i = 0; i < lst_hanger3.Count; i++)
                        {

                            if (clsdata.rotate_element > Math.PI)
                            {
                                var h1 = lst_hanger3[i].GetParameters("H1");
                                if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                {
                                    h1[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                }
                                else
                                {
                                    h1[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                }

                                var h2 = lst_hanger3[i].GetParameters("H2");

                                if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                {
                                    h2[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                }
                                else
                                {
                                    h2[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                }
                            }
                            else
                            {
                                var h1 = lst_hanger3[i].GetParameters("H2");
                                if (clsdata.lst_h1_mid_beam_min[i] == 10)
                                {
                                    h1[0].Set(clsdata.lst_h1_mid_floor_min[i] - 30 / 304.8);
                                }
                                else
                                {
                                    h1[0].Set(clsdata.lst_h1_mid_beam_min[i] - 30 / 304.8);
                                }

                                var h2 = lst_hanger3[i].GetParameters("H1");

                                if (clsdata.lst_h2_mid_beam_min[i] == 10)
                                {
                                    h2[0].Set(clsdata.lst_h2_mid_floor_min[i] - 30 / 304.8);
                                }
                                else
                                {
                                    h2[0].Set(clsdata.lst_h2_mid_beam_min[i] - 30 / 304.8);
                                }
                            }


                        }
                    }
                  


                   

                    foreach (Element hanger in lst_hanger3)
                    {

                        var lenght = hanger.GetParameters("Chiều dài thanh U");
                        lenght[0].Set(clsdata.width_duct + 55 / 304.8);

                    }
                    
                    break;

                   
                    
                default:
                    break;
            }
           
           

        }
        public void Infor_Hanger_Pick(ExternalCommandData commandData,Element hanger_pick,Element element, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Func func = new Func();

            switch (k)
            {
                case 0:
                    var Angle0 = hanger_pick.GetParameters("Duct Diameter");
                    Angle0[0].Set(clsdata.Diameter);


                    var Over0 = hanger_pick.GetParameters("KC tâm ống tới trần");

                    if (clsdata.lst_h1_beam_min.Count == 0 || clsdata.lst_h1_beam_min.Min()>clsdata.pick_foor)
                    {
                        Over0[0].Set(clsdata.pick_foor - 30 / 308.4);
                    }
                    else
                    {
                        Over0[0].Set(clsdata.lst_h1_beam_min.Min() - 30 / 308.4);
                    }
                    break;
                case 1:
                    if (element is Pipe)
                    {
                        var length1 = hanger_pick.GetParameters("Chiều dài thanh U");
                        length1[0].Set(clsdata.Diameter + 55 / 304.8);
                       
                            if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                            {
                                var h1 = hanger_pick.GetParameters("H2");

                                if (clsdata.lst_h_pick_p1_beam_min.Count == 0 || clsdata.lst_h_pick_p1_beam_min.Min() > clsdata.pick_p1_floor)
                                {
                                    h1[0].Set(clsdata.pick_p1_floor + clsdata.Diameter/2 - 30/304.8);
                                }
                                else
                                {
                                    h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() + clsdata.Diameter / 2 - 30 / 304.8);
                                }

                                var h2 = hanger_pick.GetParameters("H1");

                                if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                                {
                                    h2[0].Set(clsdata.pick_p2_floor + clsdata.Diameter / 2 - 30 / 304.8);
                                }
                                else
                                {
                                    h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() + clsdata.Diameter / 2 - 30 / 304.8);
                                }
                            }
                            else
                            {
                                var h1 = hanger_pick.GetParameters("H1");

                                if (clsdata.lst_h_pick_p1_beam_min.Count == 0)
                                {
                                    h1[0].Set(clsdata.pick_p1_floor + clsdata.Diameter / 2 - 30 / 304.8);
                                }
                                else
                                {
                                    h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() + clsdata.Diameter / 2 - 30 / 304.8);
                                }

                                var h2 = hanger_pick.GetParameters("H2");

                                if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                                {
                                    h2[0].Set(clsdata.pick_p2_floor + clsdata.Diameter / 2 - 30 / 304.8);
                                }
                                else
                                {
                                    h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() + clsdata.Diameter / 2 - 30 / 304.8);
                                }
                            }                                                                 
                    }
                    else
                    {
                        var length1 = hanger_pick.GetParameters("Chiều dài thanh U");
                        length1[0].Set(clsdata.width_duct + 55 / 304.8);
                        if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
                        {
                            var h1 = hanger_pick.GetParameters("H2");

                            if (clsdata.lst_h_pick_p1_beam_min.Count == 0 || clsdata.lst_h_pick_p1_beam_min.Min() > clsdata.pick_p1_floor)
                            {
                                h1[0].Set(clsdata.pick_p1_floor - 30 / 304.8);
                            }
                            else
                            {
                                h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() - 30 / 304.8);
                            }

                            var h2 = hanger_pick.GetParameters("H1");

                            if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                            {
                                h2[0].Set(clsdata.pick_p2_floor - 30 / 304.8);
                            }
                            else
                            {
                                h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() - 30 / 304.8);
                            }
                        }
                        else
                        {
                            var h1 = hanger_pick.GetParameters("H1");

                            if (clsdata.lst_h_pick_p1_beam_min.Count == 0)
                            {
                                h1[0].Set(clsdata.pick_p1_floor - 30 / 304.8);
                            }
                            else
                            {
                                h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() - 30 / 304.8);
                            }

                            var h2 = hanger_pick.GetParameters("H2");

                            if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                            {
                                h2[0].Set(clsdata.pick_p2_floor - 30 / 304.8);
                            }
                            else
                            {
                                h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() - 30 / 304.8);
                            }
                        }
                    }    
                    

                   
                    
                    break;
                case 2:
                    var length2 = hanger_pick.GetParameters("Chiều dài thanh U");
                    length2[0].Set(clsdata.distancee + 55/304.8);
                   
                    if (clsdata.Point_start1.Y <= clsdata.Point_end1.Y)
                    {
                        var h1 = hanger_pick.GetParameters("H1");

                        if (clsdata.lst_h_pick_p1_beam_min.Count == 0 || clsdata.lst_h_pick_p1_beam_min.Min() > clsdata.pick_p1_floor)
                        {
                            h1[0].Set(clsdata.pick_p1_floor  - 30 / 304.8);
                        }
                        else
                        {
                            h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() - 30 / 304.8);
                        }

                        var h2 = hanger_pick.GetParameters("H2");

                        if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                        {
                            h2[0].Set(clsdata.pick_p2_floor  - 30 / 304.8);
                        }
                        else
                        {
                            h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() - 30 / 304.8);
                        }
                    }
                    else
                    {
                        var h1 = hanger_pick.GetParameters("H2");

                        if (clsdata.lst_h_pick_p1_beam_min.Count == 0)
                        {
                            h1[0].Set(clsdata.pick_p1_floor  - 30 / 304.8);
                        }
                        else
                        {
                            h1[0].Set(clsdata.lst_h_pick_p1_beam_min.Min() + clsdata.Diameter1 / 2 - 30 / 304.8);
                        }

                        var h2 = hanger_pick.GetParameters("H1");

                        if (clsdata.lst_h_pick_p2_beam_min.Count == 0)
                        {
                            h2[0].Set(clsdata.pick_p2_floor  - 30 / 304.8);
                        }
                        else
                        {
                            h2[0].Set(clsdata.lst_h_pick_p2_beam_min.Min() + clsdata.Diameter2 / 2 - 30 / 304.8);
                        }
                    }
                    break;
                case 3:
                    var R1 = hanger_pick.GetParameters("R");
                    var D1 = hanger_pick.LookupParameter("D_Ubolt").AsDouble();
                    R1[0].Set(clsdata.Diameter1/2 + D1/2);
                    var L1 = hanger_pick.GetParameters("L");
                    L1[0].Set(clsdata.Diameter1 / 2 +40/304.8);
                    break;
                case 4:
                    var R2 = hanger_pick.GetParameters("R");
                    var D2 = hanger_pick.LookupParameter("D_Ubolt").AsDouble();
                    R2[0].Set(clsdata.Diameter2 / 2 + D2 / 2);
                    var L2 = hanger_pick.GetParameters("L");
                    L2[0].Set(clsdata.Diameter2 / 2+40/304.8);
                    break;
                case 6:
                    var L6 = hanger_pick.GetParameters("Chiều dài thanh U");
                    L6[0].Set(clsdata.distancee);
                    var B6 = hanger_pick.GetParameters("B");
                    clsdata.width_hanger = B6[0].AsDouble();
                    break;
                case 7:
                    var L7 = hanger_pick.GetParameters("Chiều dài thanh U");
                    L7[0].Set(clsdata.Diameter*2);
                    var B7 = hanger_pick.GetParameters("B");
                    clsdata.width_hanger = B7[0].AsDouble();
                    break;
                case 8:
                    var R8 = hanger_pick.GetParameters("R");
                    R8[0].Set(clsdata.Diameter / 2);
                    var L8 = hanger_pick.GetParameters("L");
                    L8[0].Set(clsdata.Diameter / 2 + 40 / 304.8);
                    break;
                default:
                    break;
            }

           
        }
        public void Infor_Hanger_lst_Ubolt(ExternalCommandData commandData, List<Element> lst_Ubolt)
        {
            for (int i = 0; i <lst_Ubolt.Count; i++)
            {
                var R = lst_Ubolt[i].GetParameters("R");
                R[0].Set(clsdata.lst_diameter2[i]/2);
                var L = lst_Ubolt[i].GetParameters("L");
                L[0].Set(clsdata.lst_diameter2[i]/2 + 40 / 304.8);
            }
            
        }
        }
}
