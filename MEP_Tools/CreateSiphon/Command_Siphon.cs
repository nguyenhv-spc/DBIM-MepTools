#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace MEP_Tools.CreateSiphon
{
    [Transaction(TransactionMode.Manual)]
    public class Command_Siphon : WPFData, IExternalCommand
    {
        public ICommand LoadFamily { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PickCommand { get; set; }

        public Command_Siphon()
        {
            LoadFamily = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                //p.Hide();
                //SingleData.Singleton.Instance.RevitData.Transaction.Start();

                //FilteredElementCollector Collector = new FilteredElementCollector(SingleData.Singleton.Instance.RevitData.Document);
                //IList<Element> familySymbols = Collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                //string check = "No";
                //foreach (FamilySymbol sym in familySymbols)
                //{
                //    if (sym.FamilyName == clsBien_Siphon.Name_Family)
                //    {
                //        clsBien_Siphon.FamilySymbol_Siphon = sym;
                //        check = "Yes";
                //        break;
                //    }
                //}      
                //if (check == "No")
                //{
                //    SingleData.Singleton.Instance.RevitData.Document.LoadFamily(clsBien_Siphon.PathFamily);

                //    FilteredElementCollector Collector1 = new FilteredElementCollector(SingleData.Singleton.Instance.RevitData.Document);
                //    IList<Element> familySymbols1 = Collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

                //    foreach (FamilySymbol sym1 in familySymbols1)
                //    {
                //        if (sym1.FamilyName == clsBien_Siphon.Name_Family)
                //        {
                //            clsBien_Siphon.FamilySymbol_Siphon = sym1;
                //            break;
                //        }
                //    }
                    
                //}
                //SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                
                //p.ShowDialog();
                
            });
            CancelCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Close();
            });
            PickCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (clsBien_Siphon.Name_Family != "")
                {
                    SingleData.Singleton.Instance.RevitData.Transaction.Start();
                    p.Hide();
                    #region 'Get family'
                    FilteredElementCollector Collector = new FilteredElementCollector(SingleData.Singleton.Instance.RevitData.Document);
                    IList<Element> familySymbols = Collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
                    FamilySymbol familySymbol = null;
                    foreach (FamilySymbol sym in familySymbols)
                    {
                        if (sym.FamilyName == clsBien_Siphon.Name_Family)
                        {
                            familySymbol = sym;
                            //FamilyInstance Siphon = SingleData.Singleton.Instance.RevitData.Document.Create.NewFamilyInstance(new XYZ(0, 0, 0), sym, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        }
                    }
                    if (!familySymbol.IsActive)
                    {
                        familySymbol.Activate();
                    }
                    #endregion
                    #region 'Get element'
                    Selection selection = SingleData.Singleton.Instance.RevitData.Selection;
                    ICollection<ElementId> selectedIds = selection.GetElementIds();
                    Func_Siphon F = new Func_Siphon();
                    if (0 == selectedIds.Count)
                    {
                        IList<Reference> list_r = new List<Reference>();
                        try
                        {
                            list_r = selection.PickObjects(ObjectType.Element);
                        }
                        catch
                        {

                        }
                        if (list_r != null)
                        {
                            IList<Pipe> list_pipe = new List<Pipe>();
                            foreach (Reference r in list_r)
                            {
                                if (r != null)
                                {
                                    Element e = SingleData.Singleton.Instance.RevitData.Document.GetElement(r);
                                    if (e is FamilyInstance)
                                    {
                                        IList<Connector> ConnectorofFI = F.FindConnectorofFamily(e as FamilyInstance);
                                        Connector Conn1 = F.FindConnectedTo(e as FamilyInstance, ConnectorofFI[0].Origin);
                                        Connector Conn2 = F.FindConnectedTo(e as FamilyInstance, ConnectorofFI[1].Origin);
                                        F.DisConnectedTo(e as FamilyInstance, ConnectorofFI[0].Origin, Conn1);
                                        F.DisConnectedTo(e as FamilyInstance, ConnectorofFI[1].Origin, Conn2);
                                        XYZ Axis = F.FindAxis(Conn1, Conn2, e as FamilyInstance);
                                        SingleData.Singleton.Instance.RevitData.Document.Delete(e.Id);
                                        FamilyInstance Siphon = SingleData.Singleton.Instance.RevitData.Document.Create.NewFamilyInstance(Conn1.Origin, familySymbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);                             
                                        IList<Connector> ConnectorofSiphon = F.FindConnectorofFamily(Siphon);
                                        Line LineAxis = Line.CreateUnbound(ConnectorofSiphon[0].Origin, new XYZ(ConnectorofSiphon[0].Origin.X, ConnectorofSiphon[0].Origin.Y, ConnectorofSiphon[0].Origin.Z + 10).Normalize());
                                        //ElementTransformUtils.RotateElement(SingleData.Singleton.Instance.RevitData.Document, Siphon.Id, LineAxis, Math.PI/4 );
                                        Siphon.Location.Rotate(LineAxis, Math.PI / 2);
                                        //ConnectorofSiphon[0].ConnectTo(Conn1);
                                        //ConnectorofSiphon[0].ConnectTo(Conn2);
                                    }
                                    else if (e is Pipe)
                                    {
                                        list_pipe.Add(e as Pipe);
                                    }
                                }

                            }
                            if (list_pipe.Count > 1 && list_pipe.Count < 3)
                            {

                            }
                            else
                            {
                                //System.Windows.MessageBox.Show("You must select 2 pipe");
                                //return;
                            }
                        }
                    }
                    else
                    {
                        IList<Pipe> list_pipe = new List<Pipe>();
                        foreach (ElementId r in selectedIds)
                        {
                            Element e = SingleData.Singleton.Instance.RevitData.Document.GetElement(r);
                            if (e is FamilyInstance)
                            {
                                IList<Connector> ConnectorofFI = F.FindConnectorofFamily(e as FamilyInstance);
                                Connector Conn1 = F.FindConnectedTo(e as FamilyInstance, ConnectorofFI[0].Origin);
                                Connector Conn2 = F.FindConnectedTo(e as FamilyInstance, ConnectorofFI[1].Origin);
                                F.DisConnectedTo(e as FamilyInstance, ConnectorofFI[0].Origin, Conn1);
                                F.DisConnectedTo(e as FamilyInstance, ConnectorofFI[1].Origin, Conn2);
                                XYZ Axis = F.FindAxis(Conn1, Conn2, e as FamilyInstance);
                                SingleData.Singleton.Instance.RevitData.Document.Delete(e.Id);
                                FamilyInstance Siphon = SingleData.Singleton.Instance.RevitData.Document.Create.NewFamilyInstance(Conn1.Origin, familySymbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                                IList<Connector> ConnectorofSiphon = F.FindConnectorofFamily(Siphon);
                                Line LineAxis = Line.CreateUnbound(ConnectorofSiphon[0].Origin, new XYZ(ConnectorofSiphon[0].Origin.X, ConnectorofSiphon[0].Origin.Y , ConnectorofSiphon[0].Origin.Z + 10).Normalize());
                                //ElementTransformUtils.RotateElement(SingleData.Singleton.Instance.RevitData.Document, Siphon.Id, LineAxis, Math.PI/4);
                                Siphon.Location.Rotate(LineAxis, Math.PI / 2);
                                //ConnectorofSiphon[0].ConnectTo(Conn1);
                                //ConnectorofSiphon[0].ConnectTo(Conn2);
                            }
                            else if (e is Pipe)
                            {
                                list_pipe.Add(e as Pipe);
                            }
                        }
                        if (list_pipe.Count > 1 && list_pipe.Count < 3)
                        {
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("You must select 2 pipe");
                            return;
                        }
                    }
                    #endregion
                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                    p.ShowDialog();
                }
                else
                {
                    System.Windows.MessageBox.Show("You must load family siphon !");
                }
            });
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            SingleData.Singleton.Instance = new SingleData.Singleton();
            SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;
            if (cls_Reg.Login == "Login")
            {
                SingleData.Singleton.Instance.WFData.TnputWindow_CreateSiphone.ShowDialog();
            }
            return Result.Succeeded;
        }

    }
}
