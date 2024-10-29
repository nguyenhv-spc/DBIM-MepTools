using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools.ThoatNuoc
{
    public static class cls_ThoatNuoc
    {
        public static double Slope = 0;
        public static string Check_Error = "";
        public static ElementId Id_move = null;
        public static ElementId Id_Tee1 = null;
        public static ElementId Id_Tee2 = null;
        public static ElementId Id_Tee3 = null;
        public static ElementId Id_Tee4 = null;
        public static ElementId Id_Tee5 = null;
        public static List<ElementId> list_Id_Tee6 = new List<ElementId>();
        public static List<ElementId> list_Id_Tee7 = new List<ElementId>();
        public static List<ElementId> list_Id_error = new List<ElementId>();
        public static ElementId Id_pipe_new_1 = null;
        public static ElementId Id_pipe_new_2 = null;
        public static ElementId Id_pipe_new_case7 = null;
        public static ElementId Id_Tee8 = null;
        public static ElementId Id_Tee9 = null;
        public static ElementId Id_Tee10 = null;
        public static ElementId Id_Tee11 = null;
        public static XYZ u_move = null;
        public static double kc_move = 0;
    }
}
