using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace MEP_Tools.FeedBack
{
    /// <summary>
    /// Interaction logic for MainView_FeedBack.xaml
    /// </summary>
    public partial class MainView_FeedBack : Window
    {
        public MainView_FeedBack()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Text != "")
            {
                if (Email.Text.Contains("@"))
                {
                    if (Name.Text != "")
                    {
                        if (Phone.Text != "")
                        {
                            if (Phone.Text.All(char.IsDigit))
                            {

                                string input_mail = string.Empty, input_name = string.Empty, input_phone = string.Empty, input_DongGop = string.Empty, input_ND = string.Empty;
                                    string from = string.Empty, to = string.Empty, cc = string.Empty, pass = string.Empty, content = string.Empty;
                                    input_mail = Email.Text;
                                    input_name = Name.Text;
                                    input_phone = Phone.Text;
                                    input_ND = ND.Text;

                                    from = "accclone.coder@gmail.com";
                                    pass = "nprepyyldrefxyws";
                                    cc = "nguyenhv.spc@gmail.com";
                                    to = "dbimtools@gmail.com";

                                    if (Tick.IsChecked == true)
                                    {
                                        input_DongGop = "True";
                                    }
                                    else
                                    {
                                        input_DongGop = "False";
                                    }

                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(to);
                                    mail.CC.Add(cc);
                                    mail.From = new MailAddress(from);
                                    mail.Subject = "FeedBack: " + input_name;
                                    mail.Body = "Email: " + input_mail + "\n"
                                    + "Name: " + input_name + "\n"
                                    + "Phone: " + input_phone + "\n"
                                    + "Đề Xuất Tool Mới: " + input_DongGop + "\n"
                                    + "ND: " + "\n" + input_ND ;

                                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    smtp.EnableSsl = true;
                                    smtp.Port = 587;
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Credentials = new NetworkCredential(from, pass);
                                    try
                                    {
                                        smtp.Send(mail);
                                        MessageBox.Show("Sent successfully. Thank you for responding to us !!", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                                    this.Close();
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

        private void Main_FeedBack_Loaded(object sender, RoutedEventArgs e)
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
