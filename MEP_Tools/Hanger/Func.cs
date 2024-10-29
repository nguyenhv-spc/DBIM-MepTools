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
using Autodesk.Revit.DB.Mechanical;

namespace MEP_Tools.Hanger
{
    class Func
    {
        public void Get_Offset_Hanger(ExternalCommandData commandData, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilyInstance));
            ICollection<Element> collection = collector.ToElements();

            switch (k)
            {
                case 0:// Hanger clevis 
                    string filename = "D:\\AnPha BIM\\myRevitproject\\myRevitproject\\Resources\\DSC_Duct Hanger(ROD).rfa";
                    Family family = null;
                    doc.LoadFamily(filename, out family);
                    break;
                case 1:
                    string filename1 = "D:\\AnPha BIM\\myRevitproject\\myRevitproject\\Resources\\DSC_U hanger(Type3).rfa";
                    //Family family1 = null;
                    doc.LoadFamily(filename1, out family);
                    break;
                case 2:
                    clsdata.count_family2.Clear();
                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);


                    IList<Element> symbols2 = collector2.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols2)
                    {
                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                        {
                            clsdata.count_family2.Add(1);

                        }
                    }
                    if (clsdata.count_family2.Count == 0)
                    {
                        string filename2 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Hanger_Horizontal_Duct.rfa";
                        //Family family2 = null;
                        doc.LoadFamily(filename2, out family);
                    }
                    break;

                case 3:
                    clsdata.count_family1.Clear();
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);


                    IList<Element> symbols = collector1.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols)
                    {
                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Pipe" && sym.Name == "Standard")
                        {
                            clsdata.count_family1.Add(1);
                            break;
                        }
                    }
                    if (clsdata.count_family1.Count == 0)
                    {

                        string filename3 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Hanger_Horizontal_Pipe.rfa";
                        Family family3 = null;
                        doc.LoadFamily(filename3, out family3);
                    }

                    break;
                case 4:
                    clsdata.count_family2.Clear();
                    FilteredElementCollector collector4 = new FilteredElementCollector(doc);
                    IList<Element> symbols4 = collector4.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols4)
                    {
                        if (sym.FamilyName == "DBIM Ubolt_Horizontal" && sym.Name == "H_Pipes_1")
                        {
                            clsdata.count_family2.Add(1);

                        }
                    }
                    if (clsdata.count_family2.Count == 0)
                    {

                        string filename4 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Ubolt_Horizontal.rfa";
                        Family family4 = null;
                        doc.LoadFamily(filename4, out family4);


                    }
                    break;
                case 6:
                    clsdata.count_family2.Clear();
                    FilteredElementCollector collector6 = new FilteredElementCollector(doc);


                    IList<Element> symbols6 = collector6.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols6)
                    {
                        if (sym.FamilyName == "DBIM Hanger_Vertical_Pipe" && sym.Name == "U")
                        {
                            clsdata.count_family2.Add(1);

                        }
                    }
                    if (clsdata.count_family2.Count == 0)
                    {

                        string filename6 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Hanger_Vertical_Pipe.rfa";
                        Family family6 = null;
                        doc.LoadFamily(filename6, out family6);
                        break;


                    }
                    break;
                case 7:
                    clsdata.count_family2.Clear();
                    FilteredElementCollector collector7 = new FilteredElementCollector(doc);


                    IList<Element> symbols7 = collector7.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols7)
                    {
                        if (sym.FamilyName == "DBIM Ubolt_Vertical" && sym.Name == "DBIM Ubolt_Vertical")
                        {
                            clsdata.count_family2.Add(1);

                        }
                    }
                    if (clsdata.count_family2.Count == 0)
                    {

                        string filename7 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Ubolt_Vertical.rfa";
                        Family family7 = null;
                        doc.LoadFamily(filename7, out family7);


                    }
                    break;
                case 8:
                    clsdata.count_family2.Clear();
                    FilteredElementCollector collector8 = new FilteredElementCollector(doc);


                    IList<Element> symbols8 = collector8.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                    foreach (FamilySymbol sym in symbols8)
                    {
                        if (sym.FamilyName == "DBIM Hanger_Vertical_Duct" && sym.Name == "DBIM Hanger_Vertical_Duct")
                        {
                            clsdata.count_family2.Add(1);

                        }
                    }
                    if (clsdata.count_family2.Count == 0)
                    {

                        string filename8 = "C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Family_Hanger\\DBIM Hanger_Vertical_Duct.rfa";
                        Family family8 = null;
                        doc.LoadFamily(filename8, out family8);


                    }
                    break;
                default:
                    break;
            }







            string symbName = String.Empty;

        }
        public void Infor_Hanger(ExternalCommandData commandData, Element hanger)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> collection = collector.OfCategory(BuiltInCategory.OST_GenericModel).ToElements();

            var height = hanger.GetParameters("F");
            height[0].Set(70 / 304.8);

        }
        public void Infor_Element(ExternalCommandData commandData, Element element, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            switch (k)
            {
                case 0:
                    if (element is Pipe)
                    {
                        clsdata.Diameter = element.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
                        clsdata.lst_diameter.Add(clsdata.Diameter);
                        Pipe pipe = element as Pipe;
                        Line pip_location = (pipe.Location as LocationCurve).Curve as Line;

                        clsdata.width_duct = clsdata.Diameter;

                        XYZ start = pip_location.GetEndPoint(0);
                        XYZ end = pip_location.GetEndPoint(1);
                        if (start.X == end.X)
                        {
                            if (start.Y < end.Y)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }
                        else if (start.Y == end.Y)
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }
                        else
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }


                        LocationPoint locPoint;
                        locPoint = element.Location as LocationPoint;
                        XYZ vector_pipe = (start - end).Normalize();

                        double rotate = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        clsdata.rotate_element = rotate;
                        double h = Math.Abs(start.Z - end.Z);
                        double length_pipe = start.DistanceTo(end);
                        clsdata.length_element = length_pipe;
                        double Ang_slope = Math.Tanh(h / length_pipe);
                        clsdata.Ang_slope = Ang_slope;

                    }
                    else if (element is MEPCurve)

                    {
                        MEPCurve duct = element as MEPCurve;
                        Line duct_lo = (duct.Location as LocationCurve).Curve as Line;
                        clsdata.width_duct = duct.Width;
                        clsdata.height_duct = duct.Height;
                        XYZ start1 = duct_lo.GetEndPoint(0);
                        XYZ end1 = duct_lo.GetEndPoint(1);
                        if (start1.X == end1.X)
                        {
                            if (start1.Y < end1.Y)
                            {
                                clsdata.Point_start = start1;
                                clsdata.Point_end = end1;
                            }
                            else
                            {
                                clsdata.Point_start = end1;
                                clsdata.Point_end = start1;
                            }
                        }
                        else if (start1.X < end1.X)
                        {
                            clsdata.Point_start = start1;
                            clsdata.Point_end = end1;
                        }
                        else
                        {
                            clsdata.Point_start = end1;
                            clsdata.Point_end = start1;
                        }

                        LocationPoint locPoint1;
                        locPoint1 = element.Location as LocationPoint;
                        XYZ vector_duct = (start1 - end1).Normalize();

                        double rotate1 = Math.Abs(vector_duct.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        clsdata.rotate_element = rotate1;
                        double h1 = Math.Abs(start1.Z - end1.Z);
                        double length_duct = start1.DistanceTo(end1);
                        clsdata.length_element = length_duct;
                        double Ang_slope_duct = Math.Tanh(h1 / length_duct);
                        clsdata.Ang_slope = Ang_slope_duct;
                    }
                    clsdata.lst_point_start.Add(clsdata.Point_start);
                    clsdata.lst_point_end.Add(clsdata.Point_end);
                    Lst_Beam_Limited(commandData, 0);
                    Lst_Floor_Limited(commandData, 0);
                    break;
                case 1:
                    if (element is Pipe)
                    {

                        Pipe pipe = element as Pipe;
                        Line pip_location = (pipe.Location as LocationCurve).Curve as Line;

                        XYZ start = pip_location.GetEndPoint(0);
                        XYZ end = pip_location.GetEndPoint(1);
                        if (start.X == end.X)
                        {
                            if (start.Y < end.Y)
                            {
                                clsdata.Point_start1 = start;
                                clsdata.Point_end1 = end;
                            }
                            else
                            {
                                clsdata.Point_start1 = end;
                                clsdata.Point_end1 = start;
                            }
                        }
                        else if (start.Y == end.Y)
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start1 = start;
                                clsdata.Point_end1 = end;
                            }
                            else
                            {
                                clsdata.Point_start1 = end;
                                clsdata.Point_end1 = start;
                            }
                        }
                        else
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start1 = start;
                                clsdata.Point_end1 = end;
                            }
                            else
                            {
                                clsdata.Point_start1 = end;
                                clsdata.Point_end1 = start;
                            }
                        }


                        LocationPoint locPoint;
                        locPoint = element.Location as LocationPoint;
                        XYZ vector_pipe = (start - end).Normalize();

                        double rotate = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        clsdata.rotate_element = rotate;
                        double h = Math.Abs(start.Z - end.Z);
                        double length_pipe = start.DistanceTo(end);
                        clsdata.length_element = length_pipe;
                        double Ang_slope = Math.Tanh(h / length_pipe);
                        clsdata.Ang_slope = Ang_slope;
                        Lst_Beam_Limited(commandData, 1);
                        Lst_Floor_Limited(commandData, 1);
                    }
                    break;
                case 2:
                    if (element is Pipe)
                    {

                        Pipe pipe = element as Pipe;
                        Line pip_location = (pipe.Location as LocationCurve).Curve as Line;

                        XYZ start = pip_location.GetEndPoint(0);
                        XYZ end = pip_location.GetEndPoint(1);
                        if (start.X == end.X)
                        {
                            if (start.Y < end.Y)
                            {
                                clsdata.Point_start2 = start;
                                clsdata.Point_end2 = end;
                            }
                            else
                            {
                                clsdata.Point_start2 = end;
                                clsdata.Point_end2 = start;
                            }
                        }
                        else if (start.Y == end.Y)
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start2 = start;
                                clsdata.Point_end2 = end;
                            }
                            else
                            {
                                clsdata.Point_start2 = end;
                                clsdata.Point_end2 = start;
                            }
                        }
                        else
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start2 = start;
                                clsdata.Point_end2 = end;
                            }
                            else
                            {
                                clsdata.Point_start2 = end;
                                clsdata.Point_end2 = start;
                            }
                        }


                        LocationPoint locPoint;
                        locPoint = element.Location as LocationPoint;
                        XYZ vector_pipe = (start - end).Normalize();

                        double rotate = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        clsdata.rotate_element = rotate;
                        double h = Math.Abs(start.Z - end.Z);
                        double length_pipe = start.DistanceTo(end);
                        clsdata.length_element = length_pipe;
                        double Ang_slope = Math.Tanh(h / length_pipe);
                        clsdata.Ang_slope = Ang_slope;
                        Lst_Beam_Limited(commandData, 2);
                        Lst_Floor_Limited(commandData, 2);
                    }
                    break;
                case 3:
                    if (element is Pipe)
                    {

                        Pipe pipe = element as Pipe;
                        Line pip_location = (pipe.Location as LocationCurve).Curve as Line;

                        XYZ start = pip_location.GetEndPoint(0);
                        XYZ end = pip_location.GetEndPoint(1);
                        if (clsdata.Point_start1.X == clsdata.Point_start2.X)
                        {
                            if (clsdata.Point_start1.Y < clsdata.Point_start2.Y)
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start1;
                                clsdata.Point_ver_start2 = clsdata.Point_start2;
                            }
                            else
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start2;
                                clsdata.Point_ver_start2 = clsdata.Point_start1;
                            }
                        }
                        else if (clsdata.Point_start1.Y == clsdata.Point_start2.Y)
                        {
                            if (clsdata.Point_start1.X < clsdata.Point_start2.X)
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start1;
                                clsdata.Point_ver_start2 = clsdata.Point_start2;
                            }
                            else
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start2;
                                clsdata.Point_ver_start2 = clsdata.Point_start1;
                            }
                        }
                        else
                        {
                            if (clsdata.Point_start1.X < clsdata.Point_start2.X)
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start1;
                                clsdata.Point_ver_start2 = clsdata.Point_start2;
                            }
                            else
                            {
                                clsdata.Point_ver_start1 = clsdata.Point_start2;
                                clsdata.Point_ver_start2 = clsdata.Point_start1;
                            }
                        }


                        LocationPoint locPoint;
                        locPoint = element.Location as LocationPoint;
                        XYZ vector_pipe = (start - end).Normalize();

                        double rotate = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        clsdata.rotate_element = rotate;
                        double h = Math.Abs(start.Z - end.Z);
                        double length_pipe = start.DistanceTo(end);
                        clsdata.length_element = length_pipe;
                        double Ang_slope = Math.Tanh(h / length_pipe);
                        clsdata.Ang_slope = Ang_slope;
                        Lst_Beam_Limited(commandData, 2);
                        Lst_Floor_Limited(commandData, 2);
                    }
                    break;
                case 4:
                    if (element is MEPCurve)
                    {
                        Line duct_location = (element.Location as LocationCurve).Curve as Line;

                        XYZ start = duct_location.GetEndPoint(0);
                        XYZ end = duct_location.GetEndPoint(1);
                        XYZ vector_pipe = (clsdata.lst_point_duct[0] - clsdata.lst_point_duct[1]).Normalize();
                        clsdata.rotate_duct = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                        if (start.X == end.X)
                        {
                            if (start.Y < end.Y)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }
                        else if (start.Y == end.Y)
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }
                        else
                        {
                            if (start.X < end.X)
                            {
                                clsdata.Point_start = start;
                                clsdata.Point_end = end;
                            }
                            else
                            {
                                clsdata.Point_start = end;
                                clsdata.Point_end = start;
                            }
                        }

                    }
                    break;
                default:
                    break;
            }


        }
        public void PlaceFamily(ExternalCommandData commandData, Element e, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            double anpha = Math.PI - clsdata.rotate_element;
            double anpha2 = clsdata.rotate_element;
            double w = clsdata.width_duct / 2 + 11 / 304.8;
            // Find Family 
            FilteredElementCollector collector = new FilteredElementCollector(doc);


            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

            FamilySymbol symbol = null;
            switch (k)
            {
                case 0:
                    if (e is Pipe)
                    {
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
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe - 2 * clsdata.A / 308.4) / (clsdata.distance / 308.4);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i < count; i++)
                            {

                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                              , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                             , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }



                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());

                            }
                        }
                        else
                        {
                            for (int i = 1; i < count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);


                                }
                                else
                                {

                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);


                                }

                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());
                            }
                        }
                    }
                    break;
                case 1:
                    if (e is Pipe)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe - 2 * clsdata.A / 308.4) / (clsdata.distance / 308.4);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i < count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i < count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        double count_hanger = (length_pipe - 2 * clsdata.A / 308.4) / (clsdata.distance / 308.4);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i < count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i < count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    break;
                case 2:
                    if (e is Pipe)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Pipe" && sym.Name == "Standard")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        double a = 335.5 / 304.8;

                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count; i++)
                            {

                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                              , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                             , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }



                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());

                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);


                                }
                                else
                                {

                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);


                                }

                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());
                            }
                        }
                    }

                    break;
                case 3:
                    if (e is Pipe)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                       , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p1_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);

                                XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    break;
                case 4:
                    if (e is Pipe)
                    {
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
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count_final; i++)
                            {

                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                              , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                             , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }



                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());

                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);


                                }
                                else
                                {

                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);


                                }

                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X + a, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X + a, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());
                            }
                        }
                    }
                    break;
                case 5:
                    if (e is Pipe)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                         , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }

                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                      , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);
                                }
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    else if (e is MEPCurve)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        double length_pipe = clsdata.Point_start.DistanceTo(clsdata.Point_end);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }
                        //double a = 335.5 / 304.8;
                        if (clsdata.Point_start.Y < clsdata.Point_end.Y)
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);

                                XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                          , clsdata.p2_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                XYZ p1 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());

                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count_final; i++)
                            {
                                XYZ point = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z);
                                double distance = point.DistanceTo(clsdata.p2_1);

                                XYZ point1 = new XYZ(clsdata.p2_1.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                        , clsdata.p2_1.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p2_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                clsdata.lst_point.Add(point1);
                                //XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                //XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                XYZ p1 = new XYZ(point1.X + Math.Abs(Math.Sin(anpha2)) * w, point1.Y + Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                XYZ p2 = new XYZ(point1.X - Math.Abs(Math.Sin(anpha2)) * w, point1.Y - Math.Abs(Math.Cos(anpha2)) * w, point1.Z);
                                clsdata.lst_point_1.Add(p1);
                                clsdata.lst_point_2.Add(p2);
                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element - Math.PI / 2);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p);
                                List<double> min = Get_h3_beam(commandData, p);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min.Min());
                            }
                            foreach (XYZ p in clsdata.lst_point_1)
                            {
                                List<double> min_floor = h1_mid_floor(commandData, p);
                                List<double> min_beam = h1_mid_beam(commandData, p);
                                clsdata.lst_h1_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h1_mid_beam_min.Add(min_beam.Min());


                            }
                            foreach (XYZ p in clsdata.lst_point_2)
                            {
                                List<double> min_floor = h2_mid_floor(commandData, p);
                                List<double> min_beam = h2_mid_beam(commandData, p);
                                clsdata.lst_h2_mid_floor_min.Add(min_floor.Min());
                                clsdata.lst_h2_mid_beam_min.Add(min_beam.Min());

                            }
                        }
                    }
                    break;
                default:
                    break;
            }




        }

        public void PlaceFamily_Paralell(ExternalCommandData commandData, Element e, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            double anpha = Math.PI - clsdata.rotate_element;
            double anpha2 = clsdata.rotate_element;
            double w = clsdata.width_duct / 2 + 11 / 304.8;
            // Find Family 
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
            FamilySymbol symbol = null;
            switch (k)
            {
                case 1:
                    if (e is Pipe)
                    {
                        foreach (FamilySymbol sym in symbols)
                        {
                            if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                            {
                                symbol = sym as FamilySymbol;
                                break;
                            }
                        }


                        double length_pipe = clsdata.Point_start2.DistanceTo(clsdata.Point_end2);
                        clsdata.length_pipe = length_pipe;
                        double count_hanger = (length_pipe * 304.8 - clsdata.A) / (clsdata.distance);
                        double count_real = Math.Truncate(count_hanger);
                        int count = Convert.ToInt32(count_real);
                        double count_end = (count_hanger - count_real) * clsdata.distance;
                        double count_final;
                        if (count_end > clsdata.distance)
                        {
                            count_final = count + 1;
                        }
                        else if (count_end < 200 / 304.8)
                        {
                            count_final = count - 1;
                        }
                        else
                        {
                            count_final = count;
                        }

                        if (!symbol.IsActive)
                        {
                            symbol.Activate();
                        }
                        if (clsdata.Point_start2.Y < clsdata.Point_end2.Y)
                        {
                            for (int i = 1; i <= count; i++)
                            {

                                XYZ point = new XYZ(clsdata.mid_point.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.mid_point.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.mid_point.Z);
                                double distance = point.DistanceTo(clsdata.mid_point);
                                if (clsdata.Point_start2.Z < clsdata.Point_end2.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.mid_point.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                              , clsdata.mid_point.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.mid_point.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }
                                else
                                {
                                    XYZ point1 = new XYZ(clsdata.mid_point.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                             , clsdata.mid_point.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.mid_point.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                }



                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);


                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());

                            }
                        }
                        else
                        {
                            for (int i = 1; i <= count; i++)
                            {
                                XYZ point = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                               , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z);
                                double distance = point.DistanceTo(clsdata.p1_1);
                                if (clsdata.Point_start.Z < clsdata.Point_end.Z)
                                {
                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z + distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);


                                }
                                else
                                {

                                    XYZ point1 = new XYZ(clsdata.p1_1.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * i * clsdata.distance / 304.8)
                            , clsdata.p1_1.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * i * clsdata.distance / 304.8, clsdata.p1_1.Z - distance * Math.Tan(clsdata.Ang_slope));
                                    clsdata.lst_point.Add(point1);
                                    XYZ p1 = new XYZ(point1.X - Math.Sin(anpha) * w, point1.Y - Math.Cos(anpha) * w, point1.Z);
                                    XYZ p2 = new XYZ(point1.X + Math.Sin(anpha) * w, point1.Y + Math.Cos(anpha) * w, point1.Z);
                                    clsdata.lst_point_1.Add(p1);
                                    clsdata.lst_point_2.Add(p2);


                                }

                            }
                            foreach (XYZ p in clsdata.lst_point)
                            {
                                FamilyInstance hanger = doc.Create.NewFamilyInstance(p, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                clsdata.lst_Hanger_Clevis3.Add(hanger);
                                XYZ axis_start = new XYZ(p.X, p.Y, p.Z);
                                XYZ axis_end = new XYZ(p.X, p.Y, p.Z + 10);
                                XYZ p1 = new XYZ(p.X + clsdata.diemchen, p.Y, p.Z);
                                Line axis = Line.CreateBound(axis_start, axis_end);
                                hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                                List<double> min_floor = Get_Floor_link_Revit(commandData, p1);
                                List<double> min_beam = Get_h3_beam(commandData, p1);
                                clsdata.lst_distance3.Add(min_floor.Min());
                                clsdata.lst_h3_beam_min_end.Add(min_beam.Min());
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        public List<double> Get_Floor_link_Revit(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Floor_Limited.Count != 0)
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;
                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);

                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    result.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 308.4;
                                    result.Add(distance);
                                }

                            }


                        }
                    }
                }
            }
            else
            {
                double distance = 5000 / 308.4;
                result.Add(distance);
            }
            return result;
        }

        public void Get_h1_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();

            foreach (Element eleframing in clsdata.lst_Beam_Limited)
            {
                BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                XYZ v = p1 - p2;

                XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                XYZ v1 = (p2 - p1).Normalize();
                XYZ v2 = (p2 - p3).Normalize();
                double h = (p1.Z - p.Z);
                XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                Line line1 = Line.CreateBound(p, p_1);
                Face face = null;
                Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                geomOptions.View = doc.ActiveView;
                geomOptions.ComputeReferences = true;
                GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                foreach (GeometryObject geomObj in faceGeom)
                {
                    Solid geomSolid = geomObj as Solid;
                    if (null != geomSolid)
                    {
                        foreach (Face geomFace in geomSolid.Faces)
                        {
                            face = geomFace;
                            double A = v1.Y * v2.Z - v2.Y * v1.Z;
                            double B = v1.Z * v2.X - v2.Z * v1.Z;
                            double C = v1.X * v2.Y - v2.X * v1.Y;
                            double D = -A * p1.X - B * p1.Y - C * p1.Z;
                            string result1 = geomFace.Intersect(line1).ToString();
                            if (result1 == "Overlap")
                            {
                                double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                clsdata.lst_h1_beam_min.Add(distance);
                            }
                        }
                    }
                }

            }



        }

        public void getIntersec(Line line, List<Face> lst_face)
        {
            foreach (Face f in lst_face)
            {
                IntersectionResultArray results;

                SetComparisonResult result
                  = f.Intersect(line, out results);


                if (results == null || results.Size != 1)
                {

                }
                else
                {
                    IntersectionResult iResult
                  = results.get_Item(0);
                    clsdata.lst_Intersec.Add(iResult.XYZPoint);
                }
            }

        }
        public double Distance_start_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_distance_start_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);

                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_distance_start_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_distance_start_floor.Add(distance);
                                }
                            }
                        }
                    }

                }
            }
               
            return clsdata.lst_distance_start_floor.Min();
        }

        public double Distance_end_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_distance_end_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);

                    if (p.Z < p1.Z && (p1.Z - p.Z) < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        clsdata.lst_distance_end_floor.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 5000 / 304.8;
                                        clsdata.lst_distance_end_floor.Add(distance);
                                    }

                                }
                            }
                        }
                    }

                }
            }
                
            return clsdata.lst_distance_end_floor.Min();
        }

        public void Get_h2_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();

            if (clsdata.lst_Beam_Limited.Count == 0)
            {
                clsdata.lst_h2_beam_min.Add(10);
            }
            else
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);

                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h2_beam_min.Add(distance);
                                }
                            }
                        }
                    }

                }
            }
                


        }

        public List<double> Get_h3_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {

                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);

                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    result.Add(distance);
                                }
                                else
                                {
                                    double distance = 100;
                                    result.Add(distance);
                                }

                            }


                        }
                    }


                }
            }
            else
            {
                double distance = 100;
                result.Add(distance);
            }

            return result;


        }

        public void Place_Family_Pick_point(ExternalCommandData commandData, Element element, XYZ p, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Func func = new Func();
            // Find Family 
            FilteredElementCollector collector = new FilteredElementCollector(doc);


            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

            FamilySymbol symbol = null;
            FamilySymbol symbol3 = null;
            FamilySymbol symbol4 = null;
            FamilySymbol symbol6 = null;
            FamilySymbol symbol7 = null;
            FamilySymbol symbol8 = null;
            FamilySymbol symbol9 = null;
            FamilySymbol symbol10 = null;
            FamilySymbol symbol11 = null;
            FamilySymbol symbol12 = null;
            FamilySymbol symbol13 = null;
            List<Parameter> name = new List<Parameter>();
            switch (k)
            {
                case 0:
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

                    XYZ p1 = new XYZ(p.X - 323.5 / 304.8, p.Y, p.Z);
                    FamilyInstance hanger = doc.Create.NewFamilyInstance(p1, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.Hanger_pick = hanger;
                    XYZ axis_end = new XYZ(p.X, p.Y, p1.Z + 10);
                    Line axis = Line.CreateBound(p, axis_end);
                    hanger.Location.Rotate(axis, Math.PI - clsdata.rotate_element + Math.PI);
                    break;
                case 1:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                        {
                            symbol = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol.IsActive)
                    {
                        symbol.Activate();
                    }
                    if (element is Pipe)
                    {
                        XYZ p11 = new XYZ(p.X, p.Y, p.Z - clsdata.Diameter / 2);
                        FamilyInstance hanger1 = doc.Create.NewFamilyInstance(p11, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                        clsdata.Hanger_pick = hanger1;
                        XYZ axis_end1 = new XYZ(p.X, p.Y, p11.Z + 10);
                        Line axis1 = Line.CreateBound(p, axis_end1);
                        hanger1.Location.Rotate(axis1, -clsdata.rotate_element - Math.PI / 2);

                    }
                    else
                    {
                        XYZ p2 = new XYZ(p.X, p.Y, p.Z - clsdata.height_duct / 2);
                        FamilyInstance hanger2 = doc.Create.NewFamilyInstance(p2, symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        clsdata.Hanger_pick = hanger2;
                        XYZ axis_end2 = new XYZ(p2.X, p2.Y, p2.Z + 10);
                        Line axis2 = Line.CreateBound(p2, axis_end2);
                        hanger2.Location.Rotate(axis2, -clsdata.rotate_element + 3 * Math.PI / 2);
                    }

                    break;
                case 3:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Horizontal_Duct" && sym.Name == "U")
                        {
                            symbol3 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol3.IsActive)
                    {
                        symbol3.Activate();
                    }
                    FamilyInstance hanger3 = doc.Create.NewFamilyInstance(p, symbol3, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.Hanger_pick = hanger3;
                    XYZ axis_start3 = new XYZ(p.X, p.Y, p.Z);
                    XYZ axis_end3 = new XYZ(p.X, p.Y, p.Z + 10);
                    Line axis3 = Line.CreateBound(axis_start3, axis_end3);
                    hanger3.Location.Rotate(axis3, Math.PI - clsdata.rotate_element - Math.PI / 2);
                    clsdata.pick_p1_floor = func.h_pick_p1_floor(commandData, clsdata.pick_1);
                    clsdata.pick_p2_floor = func.h_pick_p2_floor(commandData, clsdata.pick_2);
                    clsdata.pick_p1_beam = func.h_pick_p1_beam(commandData, clsdata.pick_1);
                    clsdata.pick_p2_beam = func.h_pick_p2_beam(commandData, clsdata.pick_2);
                    break;
                case 4:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Horizontal" && sym.Name == "H_Pipes_1")
                        {
                            symbol4 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol4.IsActive)
                    {
                        symbol4.Activate();
                    }
                    FamilyInstance hanger4 = doc.Create.NewFamilyInstance(clsdata.p_center_pipelong, symbol4, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.ubolt1 = hanger4;
                    XYZ axis_start4 = new XYZ(clsdata.p_center_pipelong.X, clsdata.p_center_pipelong.Y, clsdata.p_center_pipelong.Z);
                    XYZ axis_end4 = new XYZ(clsdata.p_center_pipelong.X, clsdata.p_center_pipelong.Y, clsdata.p_center_pipelong.Z + 10);
                    Line axis4 = Line.CreateBound(axis_start4, axis_end4);
                    hanger4.Location.Rotate(axis4, Math.PI - clsdata.rotate_element - Math.PI / 2);
                    FamilyInstance hanger5 = doc.Create.NewFamilyInstance(clsdata.p_center_pipeshort, symbol4, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.ubolt2 = hanger5;
                    XYZ axis_start5 = new XYZ(clsdata.p_center_pipeshort.X, clsdata.p_center_pipeshort.Y, clsdata.p_center_pipeshort.Z);
                    XYZ axis_end5 = new XYZ(clsdata.p_center_pipeshort.X, clsdata.p_center_pipeshort.Y, clsdata.p_center_pipeshort.Z + 10);
                    Line axis5 = Line.CreateBound(axis_start5, axis_end5);
                    hanger5.Location.Rotate(axis5, Math.PI - clsdata.rotate_element - Math.PI / 2);
                    break;
                case 5:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Horizontal" && sym.Name == "H_Pipes_1")
                        {
                            symbol4 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol4.IsActive)
                    {
                        symbol4.Activate();
                    }
                    foreach (var point in clsdata.lst_poin_intersect)
                    {
                        FamilyInstance hangerr = doc.Create.NewFamilyInstance(point, symbol4, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        XYZ axis_startt = new XYZ(point.X, point.Y, point.Z);
                        XYZ axis_endd = new XYZ(point.X, point.Y, point.Z + 10);
                        Line axiss = Line.CreateBound(axis_startt, axis_endd);
                        hangerr.Location.Rotate(axiss, Math.PI - clsdata.rotate_element - Math.PI / 2);
                        clsdata.lst_hanger_Ubolt.Add(hangerr);
                    }
                    break;
                case 6:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Vertical_Pipe" && sym.Name == "U")
                        {
                            symbol6 = sym as FamilySymbol;
                            break;
                        }




                    }

                    if (!symbol6.IsActive)
                    {
                        symbol6.Activate();
                    }
                    FamilyInstance hanger6 = doc.Create.NewFamilyInstance(p, symbol6, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.Hanger_pick = hanger6;

                    XYZ axis_end6 = new XYZ(p.X, p.Y, p.Z + 10);
                    Line axis6 = Line.CreateBound(p, axis_end6);
                    if (clsdata.Diameter1 < clsdata.Diameter2)
                    {
                        hanger6.Location.Rotate(axis6, -clsdata.rotate_element);
                    }
                    else
                    {
                        hanger6.Location.Rotate(axis6, -clsdata.rotate_element + Math.PI);
                    }



                    break;
                case 7:

                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Vertical" && sym.Name == "DBIM Ubolt_Vertical")
                        {
                            symbol7 = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol7.IsActive)
                    {

                        symbol7.Activate();
                    }
                    XYZ p_chen1 = new XYZ(clsdata.p_center_p1.X, clsdata.p_center_p1.Y, clsdata.p_center_p1.Z - clsdata.width_hanger / 2);
                    FamilyInstance hanger71 = doc.Create.NewFamilyInstance(p_chen1, symbol7, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.ubolt_ngang1 = hanger71;
                    XYZ axis_end7 = new XYZ(clsdata.p_center_p1.X, clsdata.p_center_p1.Y, clsdata.p_center_p1.Z + 10);
                    Line axis7 = Line.CreateBound(clsdata.p_center_p1, axis_end7);
                    if (clsdata.Diameter1 < clsdata.Diameter2)
                    {
                        hanger71.Location.Rotate(axis7, -clsdata.rotate_element - Math.PI / 2 + Math.PI);

                    }
                    else if (clsdata.Diameter1 == clsdata.Diameter2)
                    {
                        hanger71.Location.Rotate(axis7, -clsdata.rotate_element + Math.PI / 2 + Math.PI);
                    }
                    else
                    {
                        hanger71.Location.Rotate(axis7, -clsdata.rotate_element + Math.PI / 2 + Math.PI);
                    }

                    LocationPoint local = hanger71.Location as LocationPoint;
                    clsdata.copy_rotate = local.Rotation;

                    XYZ p_chen2 = new XYZ(clsdata.p_center_p2.X, clsdata.p_center_p2.Y, clsdata.p_center_p2.Z - clsdata.width_hanger / 2);
                    FamilyInstance hanger72 = doc.Create.NewFamilyInstance(p_chen2, symbol7, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.ubolt_ngang2 = hanger72;
                    XYZ axis_end72 = new XYZ(clsdata.p_center_p2.X, clsdata.p_center_p2.Y, clsdata.p_center_p2.Z + 10);
                    Line axis72 = Line.CreateBound(clsdata.p_center_p2, axis_end72);
                    if (clsdata.Diameter1 < clsdata.Diameter2)
                    {
                        hanger72.Location.Rotate(axis72, -clsdata.rotate_element - Math.PI / 2 + Math.PI);
                    }
                    else if (clsdata.Diameter1 == clsdata.Diameter2)
                    {
                        hanger72.Location.Rotate(axis72, -clsdata.rotate_element + Math.PI / 2 + Math.PI);
                    }
                    else
                    {
                        hanger72.Location.Rotate(axis72, -clsdata.rotate_element + Math.PI / 2 + Math.PI);
                    }

                    break;
                case 8:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Vertical" && sym.Name == "DBIM Ubolt_Vertical")
                        {
                            symbol8 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol8.IsActive)
                    {
                        symbol8.Activate();
                    }
                    foreach (var point in clsdata.lst_poin_intersect)
                    {
                        XYZ p_chen = new XYZ(point.X, point.Y, point.Z - clsdata.width_hanger / 2);
                        FamilyInstance hangerr = doc.Create.NewFamilyInstance(p_chen, symbol8, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        XYZ axis_startt = new XYZ(point.X, point.Y, point.Z);
                        XYZ axis_endd = new XYZ(point.X, point.Y, point.Z + 10);
                        Line axiss = Line.CreateBound(axis_startt, axis_endd);

                        hangerr.Location.Rotate(axiss, clsdata.copy_rotate /*2*Math.PI - clsdata.rotate_element - Math.PI / 2 + Math.PI*/);
                        clsdata.lst_hanger_Ubolt.Add(hangerr);
                    }
                    break;
                case 9:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Vertical_Pipe" && sym.Name == "U")
                        {
                            symbol9 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol9.IsActive)
                    {
                        symbol9.Activate();
                    }
                    XYZ pp = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y - clsdata.Diameter / 2, clsdata.pick_point.Z);
                    FamilyInstance hanger9 = doc.Create.NewFamilyInstance(pp, symbol9, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.Hanger_pick = hanger9;
                    XYZ p111 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z);
                    XYZ vector_pipe = (clsdata.pick_point - p111).Normalize();
                    double rotate = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                    clsdata.angle = rotate;
                    XYZ axis_end9 = new XYZ(pp.X, pp.Y, pp.Z + 10);
                    Line axis9 = Line.CreateBound(pp, axis_end9);
                    //hanger9.Location.Rotate(axis9,rotate );

                    break;
                case 10:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Vertical" && sym.Name == "DBIM Ubolt_Vertical")
                        {
                            symbol10 = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol10.IsActive)
                    {
                        symbol10.Activate();
                    }
                    XYZ p_chen10 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z - clsdata.width_hanger / 2);
                    FamilyInstance hanger10 = doc.Create.NewFamilyInstance(p_chen10, symbol10, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.ubolt_ngang1 = hanger10;
                    XYZ axis_end10 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z + 10);
                    Line axis10 = Line.CreateBound(p_chen10, axis_end10);
                    hanger10.Location.Rotate(axis10, -clsdata.angle);
                    break;
                case 11:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Vertical_Duct" && sym.Name == "DBIM Hanger_Vertical_Duct")
                        {
                            symbol11 = sym as FamilySymbol;
                            break;
                        }
                    }

                    if (!symbol11.IsActive)
                    {
                        symbol11.Activate();
                    }
                    XYZ p_chen11 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z);
                    FamilyInstance hanger11 = doc.Create.NewFamilyInstance(p_chen11, symbol11, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.hanger_ongdung = hanger11;
                    XYZ axis_end11 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z + 10);
                    Line axis11 = Line.CreateBound(p_chen11, axis_end11);
                    hanger11.Location.Rotate(axis11, -clsdata.rotate_duct);
                    break;
                case 12:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Hanger_Vertical_Pipe" && sym.Name == "U")
                        {
                            symbol12 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol12.IsActive)
                    {
                        symbol12.Activate();
                    }

                    FamilyInstance hanger12 = doc.Create.NewFamilyInstance(p, symbol12, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    clsdata.Hanger_pick = hanger12;
                    XYZ p12 = new XYZ(clsdata.Point_start.X, clsdata.Point_start.Y, clsdata.pick_point.Z);
                    XYZ axis_end12 = new XYZ(p.X, p.Y, p.Z + 10);
                    Line axis12 = Line.CreateBound(p, axis_end12);
                    XYZ vector_pipe12 = (p12 - p).Normalize();
                    double rotate12 = Math.Abs(vector_pipe12.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ));
                    hanger12.Location.Rotate(axis12, Math.PI - rotate12 + Math.PI / 2);
                    clsdata.angle = rotate12;

                    break;
                case 13:
                    foreach (FamilySymbol sym in symbols)
                    {

                        if (sym.FamilyName == "DBIM Ubolt_Vertical" && sym.Name == "DBIM Ubolt_Vertical")
                        {
                            symbol13 = sym as FamilySymbol;
                            break;
                        }


                    }

                    if (!symbol13.IsActive)
                    {
                        symbol13.Activate();
                    }
                    foreach (var point in clsdata.lst_poin_intersect)
                    {
                        XYZ p_chen = new XYZ(point.X, point.Y, point.Z - clsdata.width_hanger / 2);
                        FamilyInstance hangerr = doc.Create.NewFamilyInstance(p_chen, symbol13, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        XYZ axis_startt = new XYZ(point.X, point.Y, point.Z);
                        XYZ axis_endd = new XYZ(point.X, point.Y, point.Z + 10);
                        Line axiss = Line.CreateBound(axis_startt, axis_endd);

                        hangerr.Location.Rotate(axiss, clsdata.copy_rotate/*2*Math.PI - clsdata.rotate_element - Math.PI / 2*/ );
                        clsdata.lst_hanger_Ubolt.Add(hangerr);
                    }

                    break;
                default:
                    break;
            }

        }

        public XYZ Intersec_point(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            XYZ p_center = new XYZ();


            XYZ v_pt = (clsdata.Point_start - clsdata.Point_end).Normalize();
            double D = -v_pt.X * clsdata.pick_point.X - v_pt.Y * clsdata.pick_point.Y - v_pt.Z * clsdata.pick_point.Z;
            double distance = Math.Abs(clsdata.Point_start.X * v_pt.X + clsdata.Point_start.Y * v_pt.Y + clsdata.Point_start.Z * v_pt.Z + D) / Math.Sqrt(v_pt.X * v_pt.X + v_pt.Y * v_pt.Y + v_pt.Z * v_pt.Z);
            double z = distance * Math.Cos(clsdata.Ang_slope);
            double anpha = Math.PI - clsdata.rotate_element;
            double w = clsdata.width_duct / 2 + 11 / 304.8;

            if (clsdata.Point_start.Y <= clsdata.Point_end.Y)
            {
                if (clsdata.Point_start.Z <= clsdata.Point_end.Z)
                {
                    p_center = new XYZ(clsdata.Point_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start.Z + Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);

                }
                else
                {
                    p_center = new XYZ(clsdata.Point_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start.Z - Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }


            }
            else
            {
                if (clsdata.Point_start.Z <= clsdata.Point_end.Z)
                {
                    p_center = new XYZ(clsdata.Point_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start.Z + Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }
                else
                {
                    p_center = new XYZ(clsdata.Point_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start.Z - Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }
            }




            return p_center;
        }


        public XYZ Intersec_point_paralell(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            XYZ p_center = new XYZ();


            XYZ v_pt = (clsdata.Point_start2 - clsdata.Point_end2).Normalize();
            double D = -v_pt.X * clsdata.pick_point.X - v_pt.Y * clsdata.pick_point.Y - v_pt.Z * clsdata.pick_point.Z;
            double distance = Math.Abs(clsdata.Point_start2.X * v_pt.X + clsdata.Point_start2.Y * v_pt.Y + clsdata.Point_start2.Z * v_pt.Z + D) / Math.Sqrt(v_pt.X * v_pt.X + v_pt.Y * v_pt.Y + v_pt.Z * v_pt.Z);
            double z = distance * Math.Cos(clsdata.Ang_slope);
            double anpha = Math.PI - clsdata.rotate_element;
            double w = clsdata.width_duct / 2 + 11 / 304.8;

            if (clsdata.Point_start2.Y <= clsdata.Point_end2.Y)
            {
                if (clsdata.Point_start2.Z <= clsdata.Point_end2.Z)
                {
                    p_center = new XYZ(clsdata.Point_start2.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start2.Z + Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);

                }
                else
                {
                    p_center = new XYZ(clsdata.Point_start2.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start2.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start2.Z - Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }


            }
            else
            {
                if (clsdata.Point_start2.Z <= clsdata.Point_end2.Z)
                {
                    p_center = new XYZ(clsdata.Point_start2.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start2.Z + Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }
                else
                {
                    p_center = new XYZ(clsdata.Point_start2.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * z)
                , clsdata.Point_start2.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * z), clsdata.Point_start2.Z - Math.Sin(clsdata.Ang_slope) * distance);
                    clsdata.pick_1 = new XYZ(p_center.X + Math.Sin(anpha) * w, p_center.Y - Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                    clsdata.pick_2 = new XYZ(p_center.X - Math.Sin(anpha) * w, p_center.Y + Math.Cos(anpha) * w, p_center.Z - clsdata.height_duct / 2);
                }
            }




            return p_center;
        }
        public double h_pick_p1_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_h_pick_p1_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {

                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h_pick_p1_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h_pick_p1_floor.Add(distance); ;
                                }
                            }


                        }
                    }




                }
            }
            return clsdata.lst_h_pick_p1_floor.Min();
        }

        public double h_pick_p2_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
             if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_h_pick_p2_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h_pick_p2_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h_pick_p2_floor.Add(distance); ;
                                }
                            }


                        }
                    }

                }
            }

            return clsdata.lst_h_pick_p2_floor.Min();
        }

        public double h_pick_p1_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkType)).ToElements();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {

                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h_pick_p1_beam.Add(distance);
                                }
                                else
                                {
                                    double distance = 1;
                                    clsdata.lst_h_pick_p1_beam.Add(distance);
                                }


                            }


                        }
                    }


                }
                foreach (double distance in clsdata.lst_h_pick_p1_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h_pick_p1_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 1;
                clsdata.lst_h_pick_p1_beam.Add(distance);
            }

            return clsdata.lst_h_pick_p1_beam.Min();


        }

        public double h_pick_p2_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkType)).ToElements();

            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {

                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h_pick_p2_beam.Add(distance);
                                }
                                else
                                {
                                    double distance = 1;
                                    clsdata.lst_h_pick_p2_beam.Add(distance);
                                }


                            }


                        }
                    }


                }
                foreach (double distance in clsdata.lst_h_pick_p2_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h_pick_p2_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 1;
                clsdata.lst_h_pick_p2_beam.Add(distance);
            }
            return clsdata.lst_h_pick_p2_beam.Min();


        }

        public double h1_start_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count ==0)
            {
                clsdata.lst_h1_start_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h1_start_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h1_start_floor.Add(distance); ;
                                }
                            }


                        }
                    }


                }
            }    
            





            return clsdata.lst_h1_start_floor.Min();
        }

        public double h2_start_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_h2_start_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h2_start_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h2_start_floor.Add(distance); ;
                                }
                            }


                        }
                    }


                }
            }    
                
            return clsdata.lst_h2_start_floor.Min();
        }

        public double h1_end_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_h1_end_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h1_end_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h1_end_floor.Add(distance); ;
                                }
                            }


                        }
                    }


                }
            }
                






            return clsdata.lst_h1_end_floor.Min();
        }

        public double h2_end_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                clsdata.lst_h2_end_floor.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    clsdata.lst_h2_end_floor.Add(distance);
                                }
                                else
                                {
                                    double distance = 5000 / 304.8;
                                    clsdata.lst_h2_end_floor.Add(distance); ;
                                }
                            }


                        }
                    }


                }
            }
               






            return clsdata.lst_h2_end_floor.Min();
        }

        public double h1_start_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        clsdata.lst_h1_start_beam.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 1;
                                        clsdata.lst_h1_start_beam.Add(distance);
                                    }
                                    face.GetBoundingBox();

                                }


                            }
                        }
                    }
                    else
                    {
                        double distance = 1;
                        clsdata.lst_h1_start_beam.Add(distance);
                    }
                }
                foreach (double distance in clsdata.lst_h1_start_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h1_start_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 100;
                clsdata.lst_h1_start_beam.Add(distance);
            }


            return clsdata.lst_h1_start_beam.Min();


        }

        public double h1_end_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        clsdata.lst_h1_end_beam.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 1;
                                        clsdata.lst_h1_end_beam.Add(distance);
                                    }
                                    face.GetBoundingBox();

                                }


                            }
                        }
                    }
                    else
                    {
                        double distance = 1;
                        clsdata.lst_h1_end_beam.Add(distance);
                    }
                }
                foreach (double distance in clsdata.lst_h1_end_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h1_end_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 100;
                clsdata.lst_h1_end_beam.Add(distance);
            }






            return clsdata.lst_h1_end_beam.Min();


        }

        public double h2_start_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();

            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        clsdata.lst_h2_start_beam.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 1;
                                        clsdata.lst_h2_start_beam.Add(distance);
                                    }
                                    face.GetBoundingBox();

                                }


                            }
                        }
                    }
                    else
                    {
                        double distance = 1;
                        clsdata.lst_h2_start_beam.Add(distance);
                    }
                }
                foreach (double distance in clsdata.lst_h2_start_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h2_start_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 100;
                clsdata.lst_h2_start_beam.Add(distance);
            }


            return clsdata.lst_h2_start_beam.Min();


        }

        public double h2_end_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();

            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        clsdata.lst_h2_end_beam.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 1;
                                        clsdata.lst_h2_end_beam.Add(distance);
                                    }
                                    face.GetBoundingBox();

                                }


                            }
                        }
                    }
                    else
                    {
                        double distance = 1;
                        clsdata.lst_h2_end_beam.Add(distance);
                    }
                }
                foreach (double distance in clsdata.lst_h2_end_beam)
                {
                    if (distance != 1)
                    {
                        clsdata.lst_h2_end_beam_min.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 100;
                clsdata.lst_h2_end_beam.Add(distance);
            }

            return clsdata.lst_h2_end_beam.Min();


        }

        public List<double> h1_mid_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        result.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 10;
                                        result.Add(distance);
                                    }

                                }


                            }
                        }


                    }
                    else
                    {
                        double distance = 10;
                        result.Add(distance);
                    }
                }
            }
            else
            {

                double distance = 10;
                result.Add(distance);
            }

            return result;


        }

        public List<double> h2_mid_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            if (clsdata.lst_Beam_Limited.Count != 0)
            {
                foreach (Element eleframing in clsdata.lst_Beam_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);
                    if (p1.Z > p.Z && h < 5000 / 304.8)
                    {
                        Face face = null;
                        Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                        geomOptions.View = doc.ActiveView;
                        geomOptions.ComputeReferences = true;
                        GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                        foreach (GeometryObject geomObj in faceGeom)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {
                                foreach (Face geomFace in geomSolid.Faces)
                                {
                                    face = geomFace;
                                    clsdata.lst_face_floor.Add(geomFace);
                                    double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                    double B = v1.Z * v2.X - v2.Z * v1.Z;
                                    double C = v1.X * v2.Y - v2.X * v1.Y;
                                    double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                    string result1 = geomFace.Intersect(line1).ToString();
                                    if (result1 == "Overlap")
                                    {
                                        double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                        result.Add(distance);
                                    }
                                    else
                                    {
                                        double distance = 10;
                                        result.Add(distance);
                                    }

                                }


                            }
                        }


                    }
                    else
                    {
                        double distance = 10;
                        result.Add(distance);
                    }
                }
            }
            else
            {
                double distance = 10;
                result.Add(distance);
            }


            return result;


        }

        public List<double> h1_mid_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkType)).ToElements();


            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                result.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    result.Add(distance);
                                }
                                else
                                {
                                    double distance = 10;
                                    result.Add(distance);
                                }

                            }


                        }
                    }


                }
            }



                






            return result;


        }
        public List<double> h2_mid_floor(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkType)).ToElements();


            if (clsdata.lst_Floor_Limited.Count == 0)
            {
                result.Add(10);
            }
            else
            {
                foreach (Element eleFloor in clsdata.lst_Floor_Limited)
                {
                    BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                    XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                    XYZ v = p1 - p2;

                    XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                    XYZ v1 = (p2 - p1).Normalize();
                    XYZ v2 = (p2 - p3).Normalize();
                    double h = (p1.Z - p.Z);
                    XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 10000);
                    Line line1 = Line.CreateBound(p, p_1);
                    Face face = null;
                    Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                    geomOptions.View = doc.ActiveView;
                    geomOptions.ComputeReferences = true;
                    GeometryElement faceGeom = eleFloor.get_Geometry(geomOptions);
                    foreach (GeometryObject geomObj in faceGeom)
                    {
                        Solid geomSolid = geomObj as Solid;
                        if (null != geomSolid)
                        {
                            foreach (Face geomFace in geomSolid.Faces)
                            {
                                face = geomFace;
                                clsdata.lst_face_floor.Add(geomFace);
                                double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                double B = v1.Z * v2.X - v2.Z * v1.Z;
                                double C = v1.X * v2.Y - v2.X * v1.Y;
                                double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                string result1 = geomFace.Intersect(line1).ToString();
                                if (result1 == "Overlap")
                                {
                                    double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                    result.Add(distance);
                                }
                                else
                                {
                                    double distance = 10;
                                    result.Add(distance);
                                }

                            }


                        }
                    }
                }
            }

                

            return result;


        }

        public double Get_distance_start_beam(ExternalCommandData commandData, XYZ p)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> result = new List<double>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkType)).ToElements();
            if (elems.Count != 0)
            {
                foreach (Element e in elems)

                {

                    RevitLinkType linkType = e as RevitLinkType;

                    String s = String.Empty;

                    foreach (Document linkedDoc in uiapp.Application.Documents)

                    {
                        FilteredElementCollector collector_framing = new FilteredElementCollector(linkedDoc);
                        ICollection<Element> collection_framing = collector_framing.OfCategory(BuiltInCategory.OST_StructuralFraming).WhereElementIsNotElementType().ToElements();

                        foreach (Element eleframing in collection_framing)
                        {

                            BoundingBoxXYZ boundingBox = eleframing.get_BoundingBox(null);
                            XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
                            XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
                            XYZ v = p1 - p2;

                            XYZ p3 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
                            XYZ v1 = (p2 - p1).Normalize();
                            XYZ v2 = (p2 - p3).Normalize();
                            double h = (p1.Z - p.Z);
                            XYZ p_1 = new XYZ(p.X, p.Y, p.Z + 100);
                            Line line1 = Line.CreateBound(p, p_1);

                            if (p.Z < p1.Z && (p1.Z - p.Z) < 5000 / 304.8)
                            {
                                Face face = null;
                                Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
                                geomOptions.View = doc.ActiveView;
                                geomOptions.ComputeReferences = true;
                                GeometryElement faceGeom = eleframing.get_Geometry(geomOptions);
                                foreach (GeometryObject geomObj in faceGeom)
                                {
                                    Solid geomSolid = geomObj as Solid;
                                    if (null != geomSolid)
                                    {
                                        foreach (Face geomFace in geomSolid.Faces)
                                        {
                                            face = geomFace;
                                            clsdata.lst_face_floor.Add(geomFace);
                                            double A = v1.Y * v2.Z - v2.Y * v1.Z;
                                            double B = v1.Z * v2.X - v2.Z * v1.Z;
                                            double C = v1.X * v2.Y - v2.X * v1.Y;
                                            double D = -A * p1.X - B * p1.Y - C * p1.Z;
                                            string result1 = geomFace.Intersect(line1).ToString();
                                            if (result1 == "Overlap")
                                            {
                                                double distance = Math.Abs(p.X * A + p.Y * B + p.Z * C + D) / Math.Sqrt(A * A + B * B + C * C);
                                                clsdata.lst_distance_start_beam.Add(distance);
                                            }


                                        }


                                    }
                                }
                            }


                        }


                        foreach (double distance in clsdata.lst_distance_start_beam)
                        {
                            if (distance != 1)
                            {
                                clsdata.lst_distance_start_beam_min.Add(distance);
                            }
                        }

                    }
                }

            }
            else
            {
                double distance = 1;
                clsdata.lst_h1_beam.Add(distance);
            }
            if (clsdata.lst_h1_beam.Count == 0)
            {
                clsdata.lst_h1_beam.Add(1);
            }
            return clsdata.lst_h1_beam.Min();


        }

        public void Lst_Floor_Limited(ExternalCommandData commandData, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            clsdata.lst_Floor_Limited.Clear();
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> revitlinks = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RvtLinks).WhereElementIsNotElementType().ToElements();
            if (revitlinks.Count != 0)
            {
                foreach (RevitLinkInstance link in clsdata.lst_linkselect)
                {
                    //if (link.Name == clsdata.select_revit)
                    //{
                    try
                    {
                        Document linkDoc = link.GetLinkDocument();
                        FilteredElementCollector collLinked = new FilteredElementCollector(linkDoc);
                        IList<Element> linkedFloor = collLinked.OfClass(typeof(Floor)).ToElements();

                        foreach (Element eleFloor in linkedFloor)
                        {
                            BoundingBoxXYZ boundingBox = eleFloor.get_BoundingBox(null);
                            XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);




                            switch (k)
                            {
                                case 0:
                                    if (clsdata.Point_start.Z < p1.Z && (p1.Z - clsdata.Point_start.Z) < 5000 / 304.8)
                                    {
                                        Get_Face_Floor(commandData, eleFloor, clsdata.Point_start, clsdata.Point_end);
                                    }
                                    break;
                                case 1:
                                    if (clsdata.Point_start1.Z < p1.Z && (p1.Z - clsdata.Point_start1.Z) < 5000 / 304.8)
                                    {
                                        Get_Face_Floor(commandData, eleFloor, clsdata.Point_start1, clsdata.Point_end1);
                                    }
                                    break;
                                case 2:
                                    if (clsdata.Point_start2.Z < p1.Z && (p1.Z - clsdata.Point_start2.Z) < 5000 / 304.8)
                                    {
                                        Get_Face_Floor(commandData, eleFloor, clsdata.Point_start2, clsdata.Point_end2);
                                    }
                                    break;
                                default:
                                    break;


                            }


                        }
                    }
                    catch
                    {

                    }

                }
            }

        }

        public void Lst_Beam_Limited(ExternalCommandData commandData, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

            //  .OfClass(typeof(RevitLinkInstance)).ToElements();
            IList<Element> revitlinks = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RvtLinks).WhereElementIsNotElementType().ToElements();
            if (revitlinks.Count != 0)
            {
                foreach (RevitLinkInstance link in revitlinks)
                {
                    foreach (var item in clsdata.lst_select_link)
                    {
                        if (item == link.Name)
                        {
                            clsdata.lst_linkselect.Add(link);
                        }
                    }
                }
                foreach (RevitLinkInstance link in clsdata.lst_linkselect)
                {
                    clsdata.lst_Beam_Limited.Clear();
                    Document linkDoc = link.GetLinkDocument();
                    //using (FilteredElementCollector linkFraming = new FilteredElementCollector(linkDoc)
                    //    .OfCategory(BuiltInCategory.OST_StructuralFraming).WhereElementIsNotElementType())
                    FilteredElementCollector collector_framing = new FilteredElementCollector(linkDoc);
                    ICollection<Element> collection_framing = collector_framing.OfCategory(BuiltInCategory.OST_StructuralFraming).WhereElementIsNotElementType().ToElements();
                    {

                        foreach (Element eleBeam in collection_framing)
                        {
                            try
                            {
                                BoundingBoxXYZ boundingBox = eleBeam.get_BoundingBox(null);
                                XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z);


                                switch (k)
                                {
                                    case 0:
                                        double h = (p1.Z - clsdata.Point_start.Z);
                                        if (h < 5000 / 308.4 && h > 0)
                                        {
                                            Get_Face_Beam(commandData, eleBeam, clsdata.Point_start, clsdata.Point_end);
                                        }
                                        break;
                                    case 1:
                                        double h1 = (p1.Z - clsdata.Point_start1.Z);
                                        if (h1 < 5000 / 308.4 && h1 > 0)
                                        {
                                            Get_Face_Beam(commandData, eleBeam, clsdata.Point_start1, clsdata.Point_end1);
                                        }
                                        break;
                                    case 2:
                                        double h2 = (p1.Z - clsdata.Point_start2.Z);
                                        if (h2 < 5000 / 308.4 && h2 > 0)
                                        {
                                            Get_Face_Beam(commandData, eleBeam, clsdata.Point_start2, clsdata.Point_end2);
                                        }
                                        break;
                                    default:
                                        break;
                                }


                            }
                            catch (Exception)
                            {

                            }
                        }
                    }




                }
            }
        }

        public string Get_Intersec_Line(ExternalCommandData commandData, XYZ A, XYZ B, Element element)
        {
            string check = "aa";
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            //double A1 = B.Y - A.Y;
            //double B1 = B.X - A.X;
            //double C1 = -A.X * B.Y + A.X * A.Y + B.X * A.Y - A.X * A.Y;
            //double A2 = D.Y - C.Y;
            //double B2 = D.X - C.X;
            //double C2 = -C.X * D.Y + C.X * C.Y + D.X * C.Y - C.X * C.Y;
            try
            {


                Curve beam_location = (element.Location as LocationCurve).Curve;
                var startPoint = beam_location.GetEndPoint(0);
                var endPoint = beam_location.GetEndPoint(1);
                XYZ p1 = new XYZ(A.X, A.Y, 0);
                XYZ p2 = new XYZ(B.X, B.Y, 0);
                XYZ p3 = new XYZ(startPoint.X, startPoint.Y, 0);
                XYZ p4 = new XYZ(endPoint.X, endPoint.Y, 0);
                if (p3.X != p4.X && p3.Y != p4.Y)
                {
                    Line l1 = Line.CreateBound(p1, p2);
                    Line l2 = Line.CreateBound(p3, p4);
                    check = l1.Intersect(l2).ToString();

                }
            }
            catch (Exception)
            {

            }
            return check;
        }

        public void Get_Face_Beam(ExternalCommandData commandData, Element element, XYZ p_start, XYZ p_end)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            BoundingBoxXYZ boundingBox = element.get_BoundingBox(null);
            XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
            XYZ p2 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Max.Z);
            XYZ mid_point = (p_start + p_end) / 2;
            XYZ p_offset1 = new XYZ(p_start.X, p_start.Y, p1.Z);
            XYZ p_offset2 = new XYZ(p_end.X, p_end.Y, p1.Z);
            double a = 2000 / 304.8;
            Line line_location2;
            Line line_location3;
            XYZ p_1 = new XYZ();
            XYZ p_2 = new XYZ();
            if (p_start.Y <= p_end.Y)
            {
                XYZ p11 = new XYZ(mid_point.X + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * a, mid_point.Y - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element)) * a, boundingBox.Min.Z);
                XYZ p22 = new XYZ(mid_point.X - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element)) * a, mid_point.Y + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element)) * a, boundingBox.Min.Z);
                if (p_start.X == p_end.X)
                {
                    p_1 = new XYZ(p_start.X, p_start.Y - a, boundingBox.Min.Z);
                    p_2 = new XYZ(p_end.X, p_end.Y + a, boundingBox.Min.Z);

                }
                else
                {
                    p_1 = new XYZ(p_start.X - a, p_start.Y, boundingBox.Min.Z);
                    p_2 = new XYZ(p_end.X + a, p_end.Y, boundingBox.Min.Z);
                }

                line_location2 = Line.CreateBound(p11, p22);

            }
            else
            {
                XYZ p11 = new XYZ(mid_point.X + Math.Sin(Math.PI - clsdata.rotate_element) * a, mid_point.Y - Math.Cos(Math.PI - clsdata.rotate_element) * a, boundingBox.Min.Z);
                XYZ p22 = new XYZ(mid_point.X - Math.Sin(Math.PI + clsdata.rotate_element) * a, mid_point.Y + Math.Cos(Math.PI - clsdata.rotate_element) * a, boundingBox.Min.Z);
                line_location2 = Line.CreateBound(p11, p22);
                if (p_start.X == p_end.X)
                {
                    p_1 = new XYZ(p_start.X, p_start.Y - a, boundingBox.Min.Z);
                    p_2 = new XYZ(p_end.X, p_end.Y + a, boundingBox.Min.Z);

                }
                else
                {
                    p_1 = new XYZ(p_start.X - a, p_start.Y, boundingBox.Min.Z);
                    p_2 = new XYZ(p_end.X + a, p_end.Y, boundingBox.Min.Z);
                }

            }

            Line line_location1 = Line.CreateBound(p_offset2, p_offset1);
            line_location3 = Line.CreateBound(p_1, p_2);

            //Face face = null;
            List<double> count = new List<double>();
            Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
            geomOptions.View = doc.ActiveView;
            geomOptions.ComputeReferences = true;
            GeometryElement faceGeom = element.get_Geometry(geomOptions);
            foreach (GeometryObject geomObj in faceGeom)
            {
                Solid geomSolid = geomObj as Solid;
                if (null != geomSolid)
                {
                    foreach (Face geomFace in geomSolid.Faces)
                    {
                        string result1 = geomFace.Intersect(line_location1).ToString();
                        string result2 = geomFace.Intersect(line_location2).ToString();
                        string result3 = geomFace.Intersect(line_location3).ToString();
                        if (result3 == "Overlap" || result2 == "Overlap" || result1 == "Overlap")
                        {
                            count.Add(1);
                        }

                    }
                }
            }
            if (count.Count != 0)
            {
                clsdata.lst_Beam_Limited.Add(element);
            }

        }

        public void Get_Face_Floor(ExternalCommandData commandData, Element element, XYZ p_start, XYZ p_end)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            BoundingBoxXYZ boundingBox = element.get_BoundingBox(null);
            XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z);
            XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
            XYZ p_offset1 = new XYZ(p_start.X, p_start.Y, p1.Z);
            XYZ p_offset2 = new XYZ(p_end.X, p_end.Y, p1.Z);
            Line line_location = Line.CreateBound(p_offset1, p_offset2);
            //Face face = null;
            List<double> count = new List<double>();
            Autodesk.Revit.DB.Options geomOptions = new Autodesk.Revit.DB.Options();
            geomOptions.View = doc.ActiveView;
            geomOptions.ComputeReferences = true;
            GeometryElement faceGeom = element.get_Geometry(geomOptions);
            string check = string.Empty;
            if (p_start.X <= p2.X && p_start.Y <= p2.Y && p_start.X >= p1.X && p_start.Y >= p1.Y)
            {
                if (p_end.X <= p2.X && p_end.Y <= p2.Y && p_end.X >= p1.X && p_end.Y >= p1.Y)
                {
                    check = "True";
                }
            }
            foreach (GeometryObject geomObj in faceGeom)
            {
                Solid geomSolid = geomObj as Solid;
                if (null != geomSolid)
                {
                    foreach (Face geomFace in geomSolid.Faces)
                    {

                        string result = geomFace.Intersect(line_location).ToString();
                        if (result == "Overlap" /*||check =="True"*/|| result == "Subset")
                        {
                            count.Add(1);
                        }

                    }
                }
            }
            if (count.Count != 0)
            {
                clsdata.lst_Floor_Limited.Add(element);
            }
        }

        public void Get_Link_revit(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> elems = collector.OfCategory(BuiltInCategory.OST_RvtLinks)

              .OfClass(typeof(RevitLinkInstance)).ToElements();

            clsdata.lst_Link_revit = new List<string>();
            foreach (Element link in elems)
            {
                clsdata.lst_Link_revit.Add(link.Name);
            }

        }
        public XYZ intersect_twoline(ExternalCommandData commandData, XYZ A, XYZ B, XYZ C)
        {

            double A1 = B.Y - A.Y;
            double B1 = -B.X + A.X;
            double C1 = -A.X * (-A.Y + B.Y) + A.Y * (B.X - A.X);
            double A2 = A.X - B.X;
            double B2 = A.Y - B.Y;
            double C2 = -C.X * (-B.X + A.X) + C.Y * (B.Y - A.Y);
            double x = (B1 * C2 - B2 * C1) / (A1 * B2 - A2 * B1);
            double y = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);
            XYZ point = new XYZ(x, y, A.Z);
            return point;
        }
        public XYZ Get_two_pipe(ExternalCommandData commandData, Element pipe1, Element pipe2, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);


            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

            Line pip_location1 = (pipe1.Location as LocationCurve).Curve as Line;
            double l1 = pip_location1.Length;
            Line pip_location2 = (pipe2.Location as LocationCurve).Curve as Line;
            double l2 = pip_location2.Length;
            XYZ inter = new XYZ();
            XYZ vector = new XYZ();
            XYZ vector_pipe = new XYZ();
            XYZ start1 = pip_location1.GetEndPoint(0);
            XYZ end1 = pip_location1.GetEndPoint(1);
            XYZ start2 = pip_location2.GetEndPoint(0);
            XYZ end2 = pip_location2.GetEndPoint(1);
            XYZ p1 = new XYZ();
            XYZ p2 = new XYZ();
            XYZ p3 = new XYZ();
            XYZ p4 = new XYZ();
            XYZ p_center = new XYZ();
            double y1 = 0;
            double y2 = 0;
            //double x1 = 0;
            //double x2 = 0;
            double kc, kc2, rotate;

            double a = clsdata.rotate_element;
            double Diameter1 = pipe1.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
            double Diameter2 = pipe2.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
            clsdata.Diameter1 = Diameter1;
            clsdata.Diameter2 = Diameter2;
            XYZ pp1 = new XYZ();
            XYZ pp2 = new XYZ();

            switch (k)
            {
                case 1:
                    p_center = Intersec_point_paralell(commandData);

                    y1 = Math.Round(clsdata.Point_start1.Y, 4);
                    y2 = Math.Round(clsdata.Point_end1.Y, 4);
                    inter = intersect_twoline(commandData, start1, end1, p_center);
                    if (y1 == y2)
                    {
                        p1 = new XYZ(p_center.X, p_center.Y + (Diameter2 / 2), p_center.Z - Diameter2 / 2);
                        p2 = new XYZ(inter.X, inter.Y - (Diameter1 / 2), inter.Z - Diameter1 / 2);
                        p3 = new XYZ(p_center.X, p_center.Y - (Diameter2 / 2), p_center.Z - Diameter2 / 2);
                        p4 = new XYZ(inter.X, inter.Y + (Diameter1 / 2), inter.Z - Diameter1 / 2);
                    }
                    else if (y1 < y2)
                    {
                        p1 = new XYZ(p_center.X - (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y + (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                        p2 = new XYZ(inter.X + (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y - (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);
                        p3 = new XYZ(p_center.X + (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y - (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                        p4 = new XYZ(inter.X - (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y + (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);

                    }
                    else
                    {
                        p1 = new XYZ(p_center.X - (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y - (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                        p2 = new XYZ(inter.X + (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y + (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);
                        p3 = new XYZ(p_center.X + (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y + (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                        p4 = new XYZ(inter.X - (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y - (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);

                    }
                    clsdata.p_center_pipelong = inter;
                    clsdata.p_center_pipeshort = p_center;
                    if (p1.DistanceTo(p2) > p3.DistanceTo(p4))
                    {
                        clsdata.pick_1 = p1;
                        clsdata.pick_2 = p2;
                        clsdata.mid_point = (p1 + p2) / 2;
                    }
                    else
                    {
                        clsdata.pick_1 = p3;
                        clsdata.pick_2 = p4;
                        clsdata.mid_point = (p3 + p4) / 2;
                    }
                    break;
                case 2:
                    p_center = new XYZ(clsdata.Point_start1.X, clsdata.Point_start1.Y, clsdata.pick_point.Z);
                    inter = new XYZ(clsdata.Point_start2.X, clsdata.Point_start2.Y, clsdata.pick_point.Z);
                    clsdata.p_center_p1 = p_center;
                    clsdata.p_center_p2 = inter;
                    vector = (p_center - inter).Normalize();
                    kc = p_center.DistanceTo(inter);
                    kc2 = Math.Abs(Diameter1 / 2 - Diameter2 / 2);
                    rotate = Math.Atan(Math.Abs(kc2 / kc));
                    vector_pipe = (p_center - inter).Normalize();

                    clsdata.rotate_element = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ)) + rotate;
                    #region"test"
                    if (Diameter1 < Diameter2)
                    {
                        XYZ vector1 = new XYZ(-vector.Y, vector.X, vector_pipe.Z);
                        Transform rot = Transform.CreateRotationAtPoint(XYZ.BasisZ, -rotate, p_center);
                        XYZ inter_new = rot.OfPoint(inter);
                        Line line = Line.CreateBound(p_center, inter_new);
                        Transform tf = Transform.CreateTranslation(Diameter1 / 2 * vector1);
                        Curve myNewCurve = line.CreateTransformed(tf);
                        pp1 = myNewCurve.GetEndPoint(0);
                        pp2 = myNewCurve.GetEndPoint(1);
                        clsdata.mid_point = (pp1 + pp2) / 2;
                    }
                    else
                    {
                        XYZ vector1 = new XYZ(vector.Y, -vector.X, vector_pipe.Z);
                        Transform rot = Transform.CreateRotationAtPoint(XYZ.BasisZ, -rotate, inter);
                        XYZ p_center_new = rot.OfPoint(p_center);
                        Transform tf = Transform.CreateTranslation(Diameter2 / 2 * vector1);
                        Line line = Line.CreateBound(p_center_new, inter);
                        Curve myNewCurve = line.CreateTransformed(tf);
                        pp1 = myNewCurve.GetEndPoint(0);
                        pp2 = myNewCurve.GetEndPoint(1);
                        clsdata.mid_point = (pp1 + pp2) / 2;

                    }

                    #endregion
                    clsdata.mid_point1 = clsdata.mid_point;
                    break;
                case 3:
                    p_center = new XYZ(clsdata.Point_start1.X, clsdata.Point_start1.Y, clsdata.pick_point.Z);
                    inter = new XYZ(clsdata.Point_start2.X, clsdata.Point_start2.Y, clsdata.pick_point.Z);
                    clsdata.p_center_p1 = p_center;
                    clsdata.p_center_p2 = inter;
                    vector = (inter - p_center).Normalize();
                    kc = p_center.DistanceTo(inter);
                    kc2 = Math.Abs(Diameter1 / 2 - Diameter2 / 2);
                    rotate = Math.Atan(Math.Abs(kc2 / kc));
                    vector_pipe = (inter - p_center).Normalize();

                    clsdata.rotate_element = Math.Abs(vector_pipe.AngleOnPlaneTo(XYZ.BasisX, XYZ.BasisZ)) + rotate;

                    if (Diameter1 < Diameter2)
                    {
                        XYZ vector1 = new XYZ(vector.Y, -vector.X, vector_pipe.Z);
                        Transform rot = Transform.CreateRotationAtPoint(XYZ.BasisZ, -rotate, inter);
                        XYZ pcenter_new = rot.OfPoint(p_center);
                        Line line = Line.CreateBound(inter, pcenter_new);
                        Transform tf = Transform.CreateTranslation(-Diameter2 / 2 * vector1);
                        Curve myNewCurve = line.CreateTransformed(tf);
                        pp1 = myNewCurve.GetEndPoint(0);
                        pp2 = myNewCurve.GetEndPoint(1);
                        clsdata.mid_point = (pp1 + pp2) / 2;
                    }
                    else
                    {
                        XYZ vector1 = new XYZ(-vector.Y, +vector.X, vector_pipe.Z);
                        Transform rot = Transform.CreateRotationAtPoint(XYZ.BasisZ, -rotate, p_center);
                        XYZ inter_new = rot.OfPoint(inter);
                        Transform tf = Transform.CreateTranslation(-Diameter1 / 2 * vector1);
                        Line line = Line.CreateBound(inter_new, p_center);
                        Curve myNewCurve = line.CreateTransformed(tf);
                        pp1 = myNewCurve.GetEndPoint(0);
                        pp2 = myNewCurve.GetEndPoint(1);
                        clsdata.mid_point = (pp1 + pp2) / 2;

                    }
                    clsdata.mid_point2 = clsdata.mid_point;
                    break;
                default:
                    break;
            }
            clsdata.distancee = p_center.DistanceTo(inter) + Diameter1 + Diameter2;
            return inter;
        }

        public XYZ Get_two_pipe_along(ExternalCommandData commandData, Element pipe1, Element pipe2, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);


            IList<Element> symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

            Line pip_location1 = (pipe1.Location as LocationCurve).Curve as Line;
            double l1 = pip_location1.Length;
            Line pip_location2 = (pipe2.Location as LocationCurve).Curve as Line;
            double l2 = pip_location2.Length;
            double Diameter1 = pipe1.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
            double Diameter2 = pipe2.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER).AsDouble();
            clsdata.Diameter1 = Diameter1;
            clsdata.Diameter2 = Diameter2;
            XYZ inter = new XYZ();

            XYZ start1 = pip_location1.GetEndPoint(0);
            XYZ end1 = pip_location1.GetEndPoint(1);
            XYZ start2 = pip_location2.GetEndPoint(0);
            XYZ end2 = pip_location2.GetEndPoint(1);
            XYZ p1 = new XYZ();
            XYZ p2 = new XYZ();
            XYZ p3 = new XYZ();
            XYZ p4 = new XYZ();
            XYZ p_center = new XYZ();
            switch (k)
            {
                case 1:
                    if (clsdata.Point_start2.Y <= clsdata.Point_end2.Y)
                    {
                        XYZ p_start = new XYZ(clsdata.Point_start2.X, clsdata.Point_start2.Y, clsdata.Point_start2.Z);
                        p_center = new XYZ(p_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Z);

                    }
                    else
                    {
                        XYZ p_start = new XYZ(clsdata.Point_start2.X, clsdata.Point_start2.Y, clsdata.Point_start2.Z - Diameter2 / 2);
                        p_center = new XYZ(p_start.X + Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Z);

                    }
                    clsdata.p1_1 = p_center;
                    break;
                case 2:
                    if (clsdata.Point_start2.Y <= clsdata.Point_end2.Y)
                    {
                        XYZ p_start = new XYZ(clsdata.Point_end2.X, clsdata.Point_end2.Y, clsdata.Point_end2.Z - Diameter2 / 2);
                        p_center = new XYZ(p_start.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Y - Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Z);
                    }
                    else
                    {
                        XYZ p_start = new XYZ(clsdata.Point_end2.X, clsdata.Point_end2.Y, clsdata.Point_end2.Z - clsdata.height_duct / 2);
                        p_center = new XYZ(p_start.X - Math.Abs(Math.Cos(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Y + Math.Abs(Math.Sin(Math.PI - clsdata.rotate_element) * (clsdata.A) / 304.8), p_start.Z);

                    }
                    clsdata.p1_1 = p_center;
                    break;
                default:
                    break;
            }

            clsdata.p_center_pipeshort = p_center;
            double y1 = Math.Round(clsdata.Point_start1.Y, 4);
            double y2 = Math.Round(clsdata.Point_end1.Y, 4);
            inter = intersect_twoline(commandData, start1, end1, p_center);

            clsdata.p_center_pipelong = inter;
            double a = clsdata.rotate_element;

            if (y1 == y2)
            {
                p1 = new XYZ(p_center.X, p_center.Y + (Diameter2 / 2), p_center.Z - Diameter2 / 2);
                p2 = new XYZ(inter.X, inter.Y - (Diameter1 / 2), inter.Z - Diameter1 / 2);
                p3 = new XYZ(p_center.X, p_center.Y - (Diameter2 / 2), p_center.Z - Diameter2 / 2);
                p4 = new XYZ(inter.X, inter.Y + (Diameter1 / 2), inter.Z - Diameter1 / 2);
            }
            else if (y1 < y2)
            {
                p1 = new XYZ(p_center.X - (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y + (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                p2 = new XYZ(inter.X + (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y - (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);
                p3 = new XYZ(p_center.X + (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y - (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                p4 = new XYZ(inter.X - (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y + (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);

            }
            else
            {
                p1 = new XYZ(p_center.X - (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y - (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                p2 = new XYZ(inter.X + (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y + (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);
                p3 = new XYZ(p_center.X + (Diameter2 / 2) * Math.Abs(Math.Sin(a)), p_center.Y + (Diameter2 / 2) * Math.Abs(Math.Cos(a)), p_center.Z - Diameter2 / 2);
                p4 = new XYZ(inter.X - (Diameter1 / 2) * Math.Abs(Math.Sin(a)), inter.Y - (Diameter1 / 2) * Math.Abs(Math.Cos(a)), inter.Z - Diameter1 / 2);

            }

            if (p1.DistanceTo(p2) > p3.DistanceTo(p4))
            {
                clsdata.pick_1 = p1;
                clsdata.pick_2 = p2;
                clsdata.mid_point = (p1 + p2) / 2;
            }
            else
            {
                clsdata.pick_1 = p3;
                clsdata.pick_2 = p4;
                clsdata.mid_point = (p3 + p4) / 2;
            }
            clsdata.distancee = clsdata.pick_2.DistanceTo(clsdata.pick_1);
            return inter;
        }

        public double Get_direct_Duct(ExternalCommandData commandData, Element element, int k)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            MEPCurve duct = element as MEPCurve;
            Line duct_lo = (duct.Location as LocationCurve).Curve as Line;
            clsdata.width_duct = duct.Width;
            clsdata.height_duct = duct.Height;
            BoundingBoxXYZ boundingBox = element.get_BoundingBox(null);
            XYZ p1 = new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z);
            XYZ p2 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
            double denta_x = p2.DistanceTo(p1);
            double angle1, angle2, duong_cheo;
            double angle = 0;
            switch (k)
            {
                case 1:
                    if (clsdata.width_duct > clsdata.height_duct)
                    {
                        angle1 = Math.Atan(clsdata.width_duct / clsdata.height_duct);
                        duong_cheo = Math.Sqrt(clsdata.width_duct * clsdata.width_duct / 4 + clsdata.height_duct * clsdata.height_duct / 4);
                        angle2 = Math.Acos(denta_x / 2 / duong_cheo);
                        angle = angle1 - angle2;
                    }
                    else
                    {
                        angle1 = Math.Atan(clsdata.height_duct / clsdata.width_duct);
                        duong_cheo = Math.Sqrt(clsdata.width_duct * clsdata.width_duct / 4 + clsdata.height_duct * clsdata.height_duct / 4);
                        angle2 = Math.Acos(denta_x / 2 / duong_cheo);
                        angle = angle1 + angle2;
                    }
                    if (angle1 == Math.PI || angle1 == Math.PI / 2)
                    {
                        angle = 0;
                    }
                    break;
                case 2:
                    if (clsdata.width_duct > clsdata.height_duct)
                    {
                        angle1 = Math.Atan(clsdata.width_duct / clsdata.height_duct);
                        duong_cheo = Math.Sqrt(clsdata.width_duct * clsdata.width_duct / 4 + clsdata.height_duct * clsdata.height_duct / 4);
                        angle2 = Math.Acos(denta_x / 2 / duong_cheo);
                        angle = -angle1 + angle2;
                    }
                    else
                    {
                        angle1 = Math.Atan(clsdata.height_duct / clsdata.width_duct);
                        duong_cheo = Math.Sqrt(clsdata.width_duct * clsdata.width_duct / 4 + clsdata.height_duct * clsdata.height_duct / 4);
                        angle2 = Math.Acos(denta_x / 2 / duong_cheo);
                        angle = angle1 - angle2;
                    }
                    if (angle1 == Math.PI || angle1 == Math.PI / 2)
                    {
                        angle = 0;
                    }
                    break;
                default:
                    break;
            }
            return angle;
        }
        public XYZ Get_Point_Circle(ExternalCommandData commandData, XYZ p1, XYZ p2, XYZ p_center)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            XYZ vector1 = (p1 - p2).Normalize();
            XYZ p_chen = new XYZ();
            XYZ vector2 = new XYZ(vector1.Y, -vector1.X, vector1.Z);
            double c = p_center.Y * vector1.X - p_center.X * vector1.Y;
            double x_new = p_center.X + 1;
            double y_new = (-c - vector1.Y * x_new) / vector1.X;
            XYZ p_new = new XYZ(x_new, y_new, p_center.Z);
            Line line = Line.CreateBound(p_center, p_new);
            Transform tf1 = Transform.CreateTranslation(clsdata.Diameter / 2 * vector2);
            Curve myNewCurve1 = line.CreateTransformed(tf1);
            XYZ pp11 = myNewCurve1.GetEndPoint(0);
            XYZ pp21 = myNewCurve1.GetEndPoint(1);
            Transform tf = Transform.CreateTranslation(-clsdata.Diameter / 2 * vector2);
            Curve myNewCurve2 = line.CreateTransformed(tf);
            XYZ pp12 = myNewCurve2.GetEndPoint(0);
            XYZ pp22 = myNewCurve2.GetEndPoint(1);
            if (pp11.DistanceTo(p1) < pp12.DistanceTo(p1))
            {
                p_chen = pp11;
            }
            else
            {
                p_chen = pp12;
            }

            return p_chen;
        }
    }
}
