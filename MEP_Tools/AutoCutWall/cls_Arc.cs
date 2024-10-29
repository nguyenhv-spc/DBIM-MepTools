using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools
{
    public class cls_Arc
    {
        public static List<Element> List_LinkRevitFirst = new List<Element>();
        public static List<Element> List_LinkRevitSelect = new List<Element>();
        public static ElementId ID_NewWall = null;
        public static string baseoffset = string.Empty;
        public static string topoffset = string.Empty;
        public static string Unconnectedheight = string.Empty;
    }
}
