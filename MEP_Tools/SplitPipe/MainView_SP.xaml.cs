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

namespace MEP_Tools.SplitPipe
{
    /// <summary>
    /// Interaction logic for MainView_SP.xaml
    /// </summary>
    public partial class MainView_SP : Window
    {
        public Command_SP MainViewModel { get; set; }
        public MainView_SP()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_SP();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DFS.Text != "")
            {
                if (DFS.Text.All(char.IsDigit))
                {
                    if (DTU.Text != "")
                    {
                        if (DTU.Text.All(char.IsDigit))
                        {
                            clsBien_SP.Distance_From_Start = DFS.Text;
                            clsBien_SP.Distance_Twos_Union = DTU.Text;
                            if (FS.IsChecked  == true)
                            {
                                clsBien_SP.FromEdge = "Start";
                            }
                            else
                            {
                                clsBien_SP.FromEdge = "End";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong Parameter !!");
                            clsBien_SP.Distance_From_Start = "";
                            clsBien_SP.Distance_Twos_Union = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter the distance among unions !!");
                        clsBien_SP.Distance_From_Start = "";
                        clsBien_SP.Distance_Twos_Union = "";
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Parameter !!");
                    clsBien_SP.Distance_From_Start = "";
                    clsBien_SP.Distance_Twos_Union = "";
                }    
            }
            else
            {
                MessageBox.Show("Enter the distance from unions to the edge !!");
                clsBien_SP.Distance_From_Start = "";
                clsBien_SP.Distance_Twos_Union = "";
            }    
        }

        private void MainWPF_SP_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=FGZmb6nP9mw&ab_channel=DbimConstructionCompany");

        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            ((ToolTip)btn.ToolTip).IsOpen = false;
        }
    
    }
}
