using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP_Tools
{
    public static class clsBien_C1ET2E
    {
        public static List<Connector> lst_Connector_Elbow_Old = new List<Connector>();
        public static List<Pipe> lst_Pipe_Neighbour = new List<Pipe>();
        public static List<Connector> lst_Connector_Pipe_Neighbour_Old = new List<Connector>();
        public static List<ElementId> lst_IdFit = new List<ElementId>();

        public static List<Connector> lst_Connector_Pipe1 = new List<Connector>();
        public static List<Connector> lst_Connector_Pipe2 = new List<Connector>();

    }
}
