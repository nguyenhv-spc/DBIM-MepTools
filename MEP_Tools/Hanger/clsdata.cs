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
namespace MEP_Tools.Hanger
{
    public class clsdata
    {
        #region"data pipe"
        public static Element Hanger_Clevis1, Hanger_pick; // Infor start hanger 
        public static Element Hanger_Clevis2;// Infor end hanger
        public static List<Element> lst_Hanger_Clevis3 = new List<Element>();// Infor mid hanger 
        public static List<Element> lst_Floor_Limited = new List<Element>();// Infor mid hanger 
        public static List<Element> lst_Beam_Limited = new List<Element>();// Infor mid hanger 
        public static List<Element> lst_Floor_Limited_end = new List<Element>();// Infor mid hanger 
        public static List<Element> lst_Beam_Limited_end = new List<Element>();// Infor mid hanger 
        public static List<ElementId> lst_id_beam = new List<ElementId>();// list BeamId set 
        public static List<ElementId> lst_id_floor = new List<ElementId>();// list FloorId set 
        public static List<string> lst_Link_revit = new List<string>();// Infor mid hanger
        public static List<string> lst_select_link = new List<string>();// Infor mid hanger
        public static XYZ Point_start, Point_end, p1_1, p2_1, pick_point, p1_d, p1_c;
        public static double G, F, A, with_bar, distance, Ang_slope, Diameter, index_cbb, rotate_element, Real_width, length_element, length_pipe;
        public static double diemchen = 335.5 / 304.8;
        public static List<XYZ> lst_point = new List<XYZ>();
        public static List<XYZ> lst_pick_point = new List<XYZ>();
        public static List<XYZ> lst_point_chen = new List<XYZ>();
        public static List<Face> lst_face_floor = new List<Face>();
        public static List<Curve> lst_Curve = new List<Curve>();
        public static List<double> lst_distance = new List<double>(); // list distance from mid point to list floor in link revit 
        public static List<double> count_family1 = new List<double>();
        public static List<double> count_family2 = new List<double>();
        public static double distance1, distance2, pick_foor;// Distance from hanger to floor 
        public static double distance3, distance4, pick_beam;// Distance from hanger to framing
        public static List<double> lst_distance3 = new List<double>();// Distance from mid hanger to floor
        public static List<double> lst_distance_min = new List<double>();
        public static List<XYZ> lst_Intersec = new List<XYZ>();
        public static List<double> lst_distance_start_floor = new List<double>();// list distance form start point to list floor in link revit 
        public static List<double> lst_distance_end_floor = new List<double>();
        public static List<double> lst_distance_start_beam = new List<double>();// list distance form start point to list floor in link revit 
        public static List<double> lst_distance_start_beam_min = new List<double>();
        public static List<double> lst_distance_end_beam = new List<double>();
        public static List<FamilyInstance> lst_beam_link = new List<FamilyInstance>();
        public static List<double> lst_h1_beam = new List<double>();// distance from to hanger to framing = 0 
        public static List<double> lst_h2_beam = new List<double>();// list distance form start point to list floor in link revit 
        public static List<double> lst_h3_beam = new List<double>();
        public static List<double> lst_h1_beam_min = new List<double>();
        public static List<double> lst_h2_beam_min = new List<double>();
        public static List<double> lst_h3_beam_min = new List<double>();
        public static List<double> lst_h3_beam_min_end = new List<double>();
        public static int index_page;
        public static double h_hanger = 30 / 308.4;
        public static double width_duct, height_duct;
        public static string family_name, select_revit, family_name1, family_name2;
        public static List<RevitLinkInstance> lst_linkselect = new List<RevitLinkInstance>();
        #endregion
        #region"data hanger duct"
        public static double pick_p1_floor, pick_p2_floor, pick_p1_beam, pick_p2_beam;
        public static List<double> lst_h_pick_p1_floor = new List<double>();
        public static List<double> lst_h_pick_p2_floor = new List<double>();
        public static XYZ pick_1 = new XYZ();
        public static XYZ pick_2 = new XYZ();
        public static List<double> lst_h_pick_p1_beam = new List<double>();
        public static List<double> lst_h_pick_p1_beam_min = new List<double>();
        public static List<double> lst_h_pick_p2_beam = new List<double>();
        public static List<double> lst_h_pick_p2_beam_min = new List<double>();
        public static List<double> lst_h1_start_floor = new List<double>();
        public static List<double> lst_h2_start_floor = new List<double>();
        public static List<double> lst_h1_end_floor = new List<double>();
        public static List<double> lst_h2_end_floor = new List<double>();
        public static List<double> lst_h1_start_beam = new List<double>();
        public static List<double> lst_h1_start_beam_min = new List<double>();
        public static List<double> lst_h1_end_beam = new List<double>();
        public static List<double> lst_h1_end_beam_min = new List<double>();
        public static List<double> lst_h2_start_beam = new List<double>();
        public static List<double> lst_h2_start_beam_min = new List<double>();
        public static List<double> lst_h2_end_beam = new List<double>();
        public static List<double> lst_h2_end_beam_min = new List<double>();
        public static double h1_start_floor;
        public static double h2_start_floor;
        public static double h1_end_floor;
        public static double h2_end_floor;
        public static double h1_start_beam;
        public static double h2_start_beam;
        public static double h1_end_beam;
        public static double h2_end_beam;
        public static XYZ start_1 = new XYZ();
        public static XYZ start_2 = new XYZ();
        public static XYZ end_1 = new XYZ();
        public static XYZ end_2 = new XYZ();
        public static List<XYZ> lst_point_1 = new List<XYZ>();
        public static List<XYZ> lst_point_2 = new List<XYZ>();
        public static List<double> lst_h1_mid_beam_min = new List<double>();
        public static List<double> lst_h2_mid_beam_min = new List<double>();
        public static List<double> lst_h1_mid_floor_min = new List<double>();
        public static List<double> lst_h2_mid_floor_min = new List<double>();
        public static List<BoundingBoxXYZ> boudingbox = new List<BoundingBoxXYZ>();
        public static List<string> overlap = new List<string>();
        #endregion
        #region "data Parallel"
        public static List<XYZ> lst_point_start = new List<XYZ>();
        public static List<XYZ> lst_point_end = new List<XYZ>();
        public static List<XYZ> lst_poin_intersect = new List<XYZ>();
        public static List<double> lst_diameter = new List<double>();
        public static List<double> lst_lenght = new List<double>();
        public static XYZ pickpoint = new XYZ();
        public static double distancee, Diameter1, Diameter2, width_hanger, angle, angle_duct, distance_wall, Diameter_ublot, rotate_duct, copy_rotate;
        public static XYZ Point_start1 = new XYZ();
        public static XYZ Point_start2 = new XYZ();
        public static XYZ Point_end1 = new XYZ();
        public static XYZ Point_end2 = new XYZ();
        public static XYZ p_center_pipeshort = new XYZ();
        public static XYZ p_center_pipelong = new XYZ();
        public static XYZ mid_point = new XYZ();
        public static Element ubolt1, ubolt2, ubolt_ngang1, ubolt_ngang2, hanger_ongdung, pipe1;
        public static List<Element> lst_hanger_Ubolt = new List<Element>();
        public static List<double> lst_diameter2 = new List<double>();
        public static XYZ Point_ver_start1 = new XYZ();
        public static XYZ Point_ver_start2 = new XYZ();
        public static XYZ p_center_p1 = new XYZ();
        public static XYZ p_center_p2 = new XYZ();
        public static int cbb_sosanh;
        public static List<XYZ> lst_p_diameter = new List<XYZ>();
        public static List<Element> lst_MidPipe = new List<Element>();
        public static XYZ mid_point1 = new XYZ();
        public static XYZ mid_point2 = new XYZ();
        public static XYZ picked1 = new XYZ();
        public static XYZ picked2 = new XYZ();
        public static List<ElementId> lst_id_hanger = new List<ElementId>();
        public static List<XYZ> lst_point_duct = new List<XYZ>();


        #endregion

        public static string Check_hori = "";
        public static string Check_veri = "";

        public static int count1 = 0;
        public static int count2 = 0;

        public static ExternalCommandData cmData;
        public static int grap_width;
        public static int grap_height;


    }
}
