using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace MEP_Tools
{
    class SingleData
    {
        public class Singleton
        {
            public static Singleton Instance
            { // Thể hiện tĩnh của Singleton   Remove featured image
                get;
                set;
            }

            private RevitData revitData;
            private WPFData wpfData;
            public RevitData RevitData
            {
                get
                {
                    if (revitData == null) revitData = new RevitData();
                    return revitData;
                }
            }
            public WPFData WFData
            {
                get
                {
                    if (wpfData == null) wpfData = new WPFData();
                    return wpfData;
                }
            }

            // thuộc tính thành viên khác …
        }
    }
}
