using System;
using System.Collections.Generic;
using System.IO;
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

namespace MEP_Tools.Register
{
    /// <summary>
    /// Interaction logic for MainView_User.xaml
    /// </summary>
    public partial class MainView_User : Window
    {
        public MainView_User()
        {
            InitializeComponent();
        }

        private void MainWPF_Loaded(object sender, RoutedEventArgs e)
        {
         
            user_name.Content = cls_Reg.User_Name;
            user_email.Content = cls_Reg.User_Email;
            user_phone.Content = cls_Reg.User_PhoneNumber;
            user_company.Content = cls_Reg.User_Company;
            DayLeft.Content = cls_Reg.dayleft;
            if (cls_Reg.dayleft != "")
            {
                if (Convert.ToInt32(cls_Reg.dayleft) <= 0)
                {
                    Status.Content = "Expired";
                    DayLeft.Content = "0";
                    Status.Foreground = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else
                {
                    Status.Content = "Activated";
                    Status.Foreground = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
            }
            else
            {
                    Status.Content = "Expired";
                    DayLeft.Content = "0";
                    Status.Foreground = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 0, 0));
            }

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Log_out_Click(object sender, RoutedEventArgs e)
        {
            cls_Reg.Login = "";
            string path = @"C:\\ProgramData\\Autodesk\\Revit\\Addins\\" + cls_Contact.version + "\\DBIM Tools\\Login.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
