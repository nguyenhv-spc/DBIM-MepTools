#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace MEP_Tools
{
    [Transaction(TransactionMode.Manual)]
    public class Command_C1ET2E : WPFData, IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            #region 'Khai báo giá trị'
            clsBien_C1ET2E.lst_IdFit.Clear();
            string DK = "";
            string DKK = "";
            List<string> lst_Dk = new List<string>();
            List<string> lst_Dkk = new List<string>();
            List<FamilyInstance> lst_F = new List<FamilyInstance>();
            #endregion
            // Modify document within a transaction
            try
            {           
                if (cls_Reg.Login == "Login")
                {
                    using (Transaction tx = new Transaction(doc))
                    {
                        tx.Start("Add-In");
                        Selection selection = uidoc.Selection;
                        ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
                        Func_C1ET2E F = new Func_C1ET2E();
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
                                    //clsBien_C1ET2E.lst_Connector_Elbow_Old.Clear();
                                    if (r != null)
                                    {
                                        Element e = doc.GetElement(r);
                                        if (e is FamilyInstance)
                                        {
                                            clsBien_C1ET2E.lst_Connector_Elbow_Old = F.List_Connector_Elbow_Old(e);
                                            clsBien_C1ET2E.lst_Pipe_Neighbour = F.List_Pipe_Neighbor(e);
                                            clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old = F.Get_Connecter_Pipe_Neighbor(clsBien_C1ET2E.lst_Pipe_Neighbour, clsBien_C1ET2E.lst_Connector_Elbow_Old);
                                            doc.Delete(e.Id);
                                            ElementId PipeTypeId = F.GetPipeTypeId(clsBien_C1ET2E.lst_Pipe_Neighbour[0]);
                                            ElementId LvId = F.GetLevelId(clsBien_C1ET2E.lst_Pipe_Neighbour[0]);
                                            Pipe Pipe_new = F.CreatePipe(doc, PipeTypeId, LvId, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old);
                                            FamilyInstance Fiting1 = F.CreateFitting(doc, Pipe_new, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old[0]);
                                            FamilyInstance Fiting2 = F.CreateFitting(doc, Pipe_new, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old[1]);
                                            DK = F.DirOffset(clsBien_C1ET2E.lst_Pipe_Neighbour);
                                            lst_Dk.Add(DK);
                                            if (DK == "OffsetZ")
                                            {
                                                F.Offset_Fitting(Fiting1, Fiting2);
                                            }
                                            else
                                            {
                                                DKK = F.Offset_Fitting_NotZ(doc, clsBien_C1ET2E.lst_Pipe_Neighbour, Fiting1);
                                                lst_Dkk.Add(DKK);
                                                lst_F.Add(Fiting1);
                                            }
                                        }
                                        else if (e is Pipe)
                                        {
                                            list_pipe.Add(e as Pipe);
                                        }
                                    }
                                    
                                }
                                if (list_pipe.Count > 1 && list_pipe.Count < 3)
                                {
                                    F.Align2Pipe(doc, list_pipe[0], list_pipe[1]);
                                    F.Connect2Pipe(doc, list_pipe[0], list_pipe[1]);
                                }
                                else
                                {
                                    //System.Windows.MessageBox.Show("You must select 2 pipe");
                                    //return Result.Failed;
                                }
                            }
                        }
                        else
                        {
                            IList<Pipe> list_pipe = new List<Pipe>();
                            foreach (ElementId r in selectedIds)
                            {
                                //clsBien_C1ET2E.lst_Connector_Elbow_Old.Clear();
                                Element e = doc.GetElement(r);
                                if (e is FamilyInstance)
                                {
                                    clsBien_C1ET2E.lst_Connector_Elbow_Old = F.List_Connector_Elbow_Old(e);
                                    clsBien_C1ET2E.lst_Pipe_Neighbour = F.List_Pipe_Neighbor(e);
                                    clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old = F.Get_Connecter_Pipe_Neighbor(clsBien_C1ET2E.lst_Pipe_Neighbour, clsBien_C1ET2E.lst_Connector_Elbow_Old);
                                    doc.Delete(e.Id);
                                    ElementId PipeTypeId = F.GetPipeTypeId(clsBien_C1ET2E.lst_Pipe_Neighbour[0]);
                                    ElementId LvId = F.GetLevelId(clsBien_C1ET2E.lst_Pipe_Neighbour[0]);
                                    Pipe Pipe_new = F.CreatePipe(doc, PipeTypeId, LvId, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old);
                                    FamilyInstance Fiting1 = F.CreateFitting(doc, Pipe_new, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old[0]);
                                    FamilyInstance Fiting2 = F.CreateFitting(doc, Pipe_new, clsBien_C1ET2E.lst_Connector_Pipe_Neighbour_Old[1]);
                                    DK = F.DirOffset(clsBien_C1ET2E.lst_Pipe_Neighbour);
                                    lst_Dk.Add(DK);
                                    if (DK == "OffsetZ")
                                    {
                                        F.Offset_Fitting(Fiting1, Fiting2);
                                    }
                                    else
                                    {
                                        DKK = F.Offset_Fitting_NotZ(doc, clsBien_C1ET2E.lst_Pipe_Neighbour, Fiting1);
                                        lst_Dkk.Add(DKK);
                                        lst_F.Add(Fiting1);
                                    }
                                }
                                else if (e is Pipe)
                                {
                                    list_pipe.Add(e as Pipe);
                                }
                            }
                            if (list_pipe.Count > 1 && list_pipe.Count < 3)
                            {
                                F.Align2Pipe(doc, list_pipe[0], list_pipe[1]);
                                F.Connect2Pipe(doc, list_pipe[0], list_pipe[1]);
                            }
                            else
                            {
                                //System.Windows.MessageBox.Show("You must select 2 pipe");
                                //return Result.Failed;
                            }
                        }
                        tx.Commit();
                    }
                    using (Transaction tx = new Transaction(doc))
                    {
                        tx.Start("Move Fit");
                        Func_C1ET2E F = new Func_C1ET2E();
                        F.MoveElement(doc, lst_Dk, lst_Dkk, lst_F);
                        tx.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: "+ cls_Contact.sdt + " or Email: " + cls_Contact.email);                
            }

            return Result.Succeeded;
        }

    }
}
