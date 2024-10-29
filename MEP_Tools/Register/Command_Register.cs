#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using xNet;
#endregion

namespace MEP_Tools
{
    [Transaction(TransactionMode.Manual)]
    public class Command_Register : WPFData, IExternalCommand
    {
        public ICommand GetCode { get; set; }
        public ICommand Cancel { get; set; }
        public Command_Register()
        {
            Cancel = new RelayCommand<System.Windows.Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Close();
            });
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                if (cls_Reg.Login == "Login")
                {
                    SingleData.Singleton.Instance = new SingleData.Singleton();
                    SingleData.Singleton.Instance.WFData.InputWindow_user.ShowDialog();
                    if (cls_Reg.Login == "")
                    {
                        cls_Reg.panel_thoatnuoc.Visible = false;
                        cls_Reg.panel_mep.Visible = false;
                        cls_Reg.panel_arc.Visible = false;

                    }
                }
                else
                {
                    SingleData.Singleton.Instance = new SingleData.Singleton();
                    SingleData.Singleton.Instance.RevitData.UIApplication = commandData.Application;
                    SingleData.Singleton.Instance.WFData.InputWindow_Register.ShowDialog();
                    cls_Reg.Str_CheckCode = CheckCodeKey(cls_Reg.Str_Code);
                    if (cls_Reg.Login == "Login")
                    {

                        if (cls_Reg.Str_CheckCode == "OK")
                        {
                            cls_Reg.panel_thoatnuoc.Visible = true;
                            cls_Reg.panel_mep.Visible = true;
                            cls_Reg.panel_arc.Visible = true;

                        }
                        else
                        {
                            cls_Reg.panel_thoatnuoc.Visible = false;
                            cls_Reg.panel_mep.Visible = false;
                            cls_Reg.panel_arc.Visible = false;

                        }
                    }
                    else
                    {
                        cls_Reg.panel_thoatnuoc.Visible = false;
                        cls_Reg.panel_mep.Visible = false;
                        cls_Reg.panel_arc.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n" + "Contact: " + cls_Contact.sdt + " or Email: " + cls_Contact.email);
            }
            return Result.Succeeded;
        }
        public string CheckCodeKey(string str)
        {
            HttpRequest Check = new HttpRequest();
            string text = "";
            string result = "";
            try
            {
                text = Check.Get(cls_Contact.web_date, null).ToString();
                Match match = Regex.Match(text.ToString(), str + ".*?(?=DBIMCompany)");
                if (match != Match.Empty)
                {
                    if (CheckDate(match) == "HH")
                    {
                        result = "KO";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
                else
                {
                    result = "KO";
                }
            }
            catch (Exception)
            {
                result = "KO";
            }

            return result;
        }
        public string CheckDate(Match match)
        {
            string result = "";
            string[] date = match.ToString().Split(new char[] { '|' });
            string day = date[1].Split(new char[] { '/' })[0];
            string month = date[1].Split(new char[] { '/' })[1];
            string year = date[1].Split(new char[] { '/' })[2];
            System.DateTime now = new System.DateTime(Convert.ToInt32(cls_Reg.year_real), Convert.ToInt32(cls_Reg.month_real), Convert.ToInt32(cls_Reg.day_real));
            System.DateTime then = new System.DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            System.TimeSpan diff1 = then.Subtract(now);

            int days = (int)Math.Ceiling(diff1.TotalDays);
            if (days <= 0)
            {
                result = "HH";
            }
            return result;
        }
    }
}
