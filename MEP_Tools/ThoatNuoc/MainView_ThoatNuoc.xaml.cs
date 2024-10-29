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

namespace MEP_Tools.ThoatNuoc
{
    /// <summary>
    /// Interaction logic for MainView_ThoatNuoc.xaml
    /// </summary>
    public partial class MainView_ThoatNuoc : Window
    {

        public Command_Drainage1 MainViewModel { get; set; }
        public MainView_ThoatNuoc()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_Drainage1();
        }

        private void btn_case1_Click(object sender, RoutedEventArgs e)
        {
            if (Slope_Case1.Text != "")
            {
                try
                {
                    //cls_ThoatNuoc.Slope = Convert.ToDouble(Slope_Case1.Text);
                    cls_ThoatNuoc.Check_Error = "No";
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter the slope of pipes again !");
                    cls_ThoatNuoc.Check_Error = "Yes";
                }
            }
            else
            {
                MessageBox.Show("Please enter the slope of pipes !");
                cls_ThoatNuoc.Check_Error = "Yes";
            }   
        }

        private void btn_case2_Click(object sender, RoutedEventArgs e)
        {
            if (Slope_Case2.Text != "")
            {
                try
                {
                    //cls_ThoatNuoc.Slope = Convert.ToDouble(Slope_Case2.Text);
                    cls_ThoatNuoc.Check_Error = "No";
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter the slope of pipes again !");
                    cls_ThoatNuoc.Check_Error = "Yes";
                }
            }
            else
            {
                MessageBox.Show("Please enter the slope of pipes !");
                cls_ThoatNuoc.Check_Error = "Yes";
            }
        }

        private void btn_case3_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_case4_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btn_case5_Click(object sender, RoutedEventArgs e)
        {
            if (Slope_Case5.Text != "")
            {
                try
                {
                    //cls_ThoatNuoc.Slope = Convert.ToDouble(Slope_Case5.Text);
                    cls_ThoatNuoc.Check_Error = "No";
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter the slope of pipes again !");
                    cls_ThoatNuoc.Check_Error = "Yes";
                }
            }
            else
            {
                MessageBox.Show("Please enter the slope of pipes !");
                cls_ThoatNuoc.Check_Error = "Yes";
            }
        }

        private void btn_help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=hFyHA6D_dyk&ab_channel=DbimConstructionCompany");
        }
    }
}
