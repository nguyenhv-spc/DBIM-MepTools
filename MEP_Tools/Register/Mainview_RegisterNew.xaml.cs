using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MEP_Tools.Register
{
    /// <summary>
    /// Interaction logic for Mainview_RegisterNew.xaml
    /// </summary>
    public partial class Mainview_RegisterNew : Window
    {
        public Command_Register MainViewModel { get; set; }
        public Mainview_RegisterNew()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_Register();
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Text != "")
            {
                if (Email.Text.Contains("@"))
                {
                    if (UserName.Text != "")
                    {
                        if (Phone.Text != "")
                        {
                            if (Phone.Text.All(char.IsDigit))
                            {
                                if (Pass.Text != "")
                                {
                                    string input_mail = string.Empty, input_codecomputer = string.Empty, input_name = string.Empty, input_phone = string.Empty, input_pass = string.Empty, input_trial = string.Empty, input_company = string.Empty;
                                    string from = string.Empty, to = string.Empty, cc = string.Empty, pass = string.Empty, content = string.Empty, input_code = string.Empty;
                                    input_mail = Email.Text;
                                    input_name = UserName.Text;
                                    input_phone = Phone.Text;
                                    input_pass = Pass.Text;
                                    input_company = Company.Text;
                                    input_code = Code.Text;
                                    input_codecomputer = GetCodeKey();

                                    from = "accclone.coder@gmail.com";
                                    pass = "nprepyyldrefxyws";
                                    to = "nguyenhv.spc@gmail.com";
                                    cc = "dbimtools@gmail.com";
                                    if (Trial.IsChecked == true)
                                    {
                                        input_trial = "Trial";
                                    }
                                    else
                                    {
                                        input_trial = "User";
                                    }

                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(to);
                                    mail.CC.Add(cc);
                                    mail.From = new MailAddress(from);
                                    mail.Subject = "Register Account: " + input_name;
                                    mail.Body = "ID: " + input_codecomputer + "\n"
                                        + "Email: " + input_mail + "\n"
                                        + "Pass: " + input_pass + "\n"
                                        + "Name: " + input_name + "\n"
                                        + "Phone: " + input_phone + "\n"
                                        + "Company: " + input_company + "\n"
                                        + "Trial: " + input_trial + "\n"
                                        + "Code: " + input_code + "\n"
                                        + input_codecomputer + "|" + input_mail + "|" + input_pass + "|" + input_name + "|" + input_phone + "|" + input_company + "|" + "Login" + "\n"
                                        + input_codecomputer + "|" + "dd/mm/yyyy" + "|" + "DBIMCompany" + "\n";

                                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    smtp.EnableSsl = true;
                                    smtp.Port = 587;
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Credentials = new NetworkCredential(from, pass);
                                    try
                                    {
                                        smtp.Send(mail);
                                        MessageBox.Show("Sent successfully. Please wait for DBIM contact to you!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show(ex.Message, "Email", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter Password");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter Phone Number again");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Please enter Phone Number");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter Name");
                    }
                }
                else
                {
                    MessageBox.Show("The email format isn't correct. Please enter again");
                }
            }
            else
            {
                MessageBox.Show("Please enter Email");
            }
            
        } 
        private void MainWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string GetCodeKey()
        {
            string output = RunCMD("wmic diskdrive get serialNumber");
            string output1 = RunCMD("ipconfig");

            string[] result1 = Regex.Split(output, @"\r\r\n|[ ]{2,}");
            string[] result2 = Regex.Split(output1, @"\r\r\n|\r\n|[ ]{2,}");

            string str1 = result1[2];
            string str2 = "";
            for (int i = 0; i < result2.Length; i++)
            {
                if (result2[i].Contains("Ethernet adapter Ethernet:"))
                {
                    if (result2[i + 3] == "Connection-specific DNS Suffix")
                    {
                        str2 = result2[i + 8].Split(new char[] { ':' })[1].Split(new char[] { ' ' })[1];
                    }
                    else if (result2[i + 3] == "Media State . . . . . . . . . . . : Media disconnected")
                    {

                    }
                }
                else if (result2[i].Contains("Ethernet adapter Ethernet 2:"))
                {
                    if (result2[i + 3] == "Connection-specific DNS Suffix")
                    {
                        str2 = result2[i + 8].Split(new char[] { ':' })[1].Split(new char[] { ' ' })[1];
                    }
                    else if (result2[i + 3] == "Media State . . . . . . . . . . . : Media disconnected")
                    {

                    }
                }
            }
            string str = "";
            str = str1 + str2;
            str = Encode(str);
            return str;
        }
        public string Encode(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsNumber(input[i]) == true)
                {
                    string code = "";
                    switch (input[i])
                    {
                        case '0':
                            code = "0000";
                            break;
                        case '1':
                            code = "0001";
                            break;
                        case '2':
                            code = "0010";
                            break;
                        case '3':
                            code = "0011";
                            break;
                        case '4':
                            code = "0100";
                            break;
                        case '5':
                            code = "0101";
                            break;
                        case '6':
                            code = "0110";
                            break;
                        case '7':
                            code = "0111";
                            break;
                        case '8':
                            code = "1000";
                            break;
                        case '9':
                            code = "1001";
                            break;
                    }
                    result += code;
                }
                else
                {
                    result += input[i];
                }
            }
            return result;
        }
        private string RunCMD(string cmd)
        {
            Process cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.Arguments = "/c " + cmd;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();
            string output = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
            if (String.IsNullOrEmpty(output))
            {
                return "";
            }
            return output;
        }
    }
}
