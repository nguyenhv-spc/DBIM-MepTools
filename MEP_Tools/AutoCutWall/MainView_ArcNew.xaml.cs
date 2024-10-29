using Autodesk.Revit.DB;
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

namespace MEP_Tools.AutoCutWall
{
    /// <summary>
    /// Interaction logic for MainView_ArcNew.xaml
    /// </summary>
    public partial class MainView_ArcNew : Window
    {
        public Command_AutoCutWall MainViewModel { get; set; }
        public MainView_ArcNew()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_AutoCutWall();
        }

        private void MainWPF_Loaded(object sender, RoutedEventArgs e)
        {
            list_Link.Items.Clear();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            this.Topmost = true;

            foreach (var item in cls_Arc.List_LinkRevitFirst)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Content = item.Name;
                list_Link.Items.Add(checkbox);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cls_Arc.List_LinkRevitSelect.Clear();
            foreach (CheckBox item in list_Link.Items)
            {
                if (item.IsChecked == true)
                {
                    foreach (Element itemm in cls_Arc.List_LinkRevitFirst)
                    {
                        if (itemm.Name == item.Content.ToString())
                        {
                            cls_Arc.List_LinkRevitSelect.Add(itemm);
                            break;
                        }
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MainWPF_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
