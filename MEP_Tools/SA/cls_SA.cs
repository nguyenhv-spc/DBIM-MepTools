using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;

namespace MEP_Tools
{
    public static class cls_SA
    {     
        public static List<Element> List_LinkRevitFirst = new List<Element>();
        public static List<Element> List_LinkRevitSelect = new List<Element>();
        public static Wall WallSelcet = null;
        public static List<Pipe> List_PipeinLink = new List<Pipe>();
        public static List<Duct> List_DuctinLink = new List<Duct>();
        public static List<CableTray> List_CableTrayinLink = new List<CableTray>();
        public static double Offset_D = 0;
        public static double Offset_H = 0;
        public static double Offset_W = 0;
        public static double Limit_Dia = 0;
        public static string CheckPipe = string.Empty;
        public static string CheckDuct = string.Empty;
        public static string CheckCableTray = string.Empty;
        public static string CheckCircle = string.Empty;
        public static string CheckRec = string.Empty;
        public static List<Pipe> List_PipeIntersec = new List<Pipe>();
        public static List<Wall> List_WallIntersecPipe = new List<Wall>();
        public static List<Duct> List_DuctIntersec = new List<Duct>();
        public static List<Wall> List_WallIntersecDuct = new List<Wall>();
        public static List<CableTray> List_CableTrayIntersec = new List<CableTray>();
        public static List<Wall> List_WallIntersecCableTray = new List<Wall>();
        public static List<ElementId> List_IDWall = new List<ElementId>();
    }
}
