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
namespace MEP_Tools.Register
{
    /// <summary>
    /// Interaction logic for MainView_PW.xaml
    /// </summary>
    public partial class MainView_PW : Window
    {
        public Command_Register MainViewModel { get; set; }
        public MainView_PW()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_Register();
        }

        private void SendInfor_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Text != "")
            {
                    if (Phone.Text != "")
                    {
                        if (Phone.Text.All(char.IsDigit))
                        {
                                string input_mail = string.Empty, input_phone = string.Empty;
                                string from = string.Empty,cc = string.Empty, to = string.Empty, pass = string.Empty;
                                input_mail = Email.Text;
                                input_phone = Phone.Text;

                                from = "accclone.coder@gmail.com";
                                pass = "nprepyyldrefxyws";
                                to = "nguyenhv.spc@gmail.com";
                                cc = "dbimtools@gmail.com";

                                MailMessage mail = new MailMessage();
                                mail.To.Add(to);
                                mail.CC.Add(cc);
                                mail.From = new MailAddress(from);
                                mail.Subject = "Forget Password:" ;
                                mail.Body = input_mail + "\n" + input_phone;
                        
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                smtp.EnableSsl = true;
                                smtp.Port = 587;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Credentials = new NetworkCredential(from, pass);
                                try
                                {
                                    smtp.Send(mail);
                                    MessageBox.Show("Send successfully!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message, "Email", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
