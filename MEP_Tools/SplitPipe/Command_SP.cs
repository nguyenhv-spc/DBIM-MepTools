#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.VisualBasic;
#endregion

namespace MEP_Tools
{
    [Transaction(TransactionMode.Manual)]
    public class Command_SP : WPFData, IExternalCommand
    {
        public ICommand OKCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Command_SP()
        {
            OKCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                Func_SP F = new Func_SP();
                try
                {                                       
                    if (clsBien_SP.Distance_From_Start != "" && clsBien_SP.Distance_Twos_Union != "")
                {
                    p.Hide();
                    SingleData.Singleton.Instance.RevitData.Transaction.Start();                 
                    #region 'Khai báo giá trị'
                    #endregion
                    IList<Reference> refs = null;
                    try
                    {
                        refs = SingleData.Singleton.Instance.RevitData.Selection.PickObjects(ObjectType.Element);
                    }
                    catch
                    {

                    }
                    if (refs != null)
                    {
                        foreach (Reference r in refs)
                        {
                            Element Ele = SingleData.Singleton.Instance.RevitData.Document.GetElement(r);
                            double l = Convert.ToDouble(clsBien_SP.Distance_From_Start);
                            if(Ele is Pipe)
                            {
                                F.SplitPipe(Ele, SingleData.Singleton.Instance.RevitData.Document, l);
                            }
                            else if (Ele is Duct)
                            {
                                F.SplitDuct(Ele, SingleData.Singleton.Instance.RevitData.Document, l);
                            }
                            else if (Ele is CableTray)
                            {
                                F.SplitCableTray(Ele, SingleData.Singleton.Instance.RevitData.Document, l);
                            }
                            else if (Ele is Conduit)
                            {
                                F.SplitConduit(Ele, SingleData.Singleton.Instance.RevitData.Document, l);
                            }
                        }
                    }
                    SingleData.Singleton.Instance.RevitData.Transaction.Commit();
                    p.ShowDialog();
                }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
                }
            });
            CloseCommand = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Close();
            });           
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SingleData.Singleton.Instance = new SingleData.Singleton();
            SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;
            if (cls_Reg.Login == "Login")
            {
                SingleData.Singleton.Instance.WFData.InputWindow_SP.ShowDialog();
            }
            return Result.Succeeded;
        }

    }
}
