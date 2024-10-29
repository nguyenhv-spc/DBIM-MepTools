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


namespace MEP_Tools.SA
{
    /// <summary>
    /// Interaction logic for MainView_SA.xaml
    /// </summary>
    public partial class MainView_SA : Window
    {
        public Command_SmartAvoid MainViewModel { get; set; }
        public MainView_SA()
        {
            InitializeComponent();
            this.DataContext = MainViewModel = new Command_SmartAvoid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list_Link.Items.Clear();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            this.Topmost = true;

            foreach (var item in cls_SA.List_LinkRevitFirst)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Content = item.Name;
                list_Link.Items.Add(checkbox);
            }
            if (Check_Diameter.IsChecked == true)
            {
                Limit_Dia.IsEnabled = true;
            }
            else
            {
                Limit_Dia.IsEnabled = false;
            }

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

            cls_SA.CheckPipe = "";
            cls_SA.CheckDuct = "";
            cls_SA.CheckCableTray = "";
            if (txt_H.Text.All(char.IsDigit))
            {
                cls_SA.Offset_H = Convert.ToDouble(txt_H.Text) / 304.8;
            }
            if (txt_W.Text.All(char.IsDigit))
            {
                cls_SA.Offset_W = Convert.ToDouble(txt_W.Text) / 304.8;
            }

            if (Limit_Dia.IsEnabled == true)
            {
                if (Limit_Dia.Text.All(char.IsDigit))
                {
                    cls_SA.Limit_Dia = Convert.ToDouble(Limit_Dia.Text) / 304.8;
                }
            }
            else if (Limit_Dia.IsEnabled == false)
            {
                cls_SA.Limit_Dia = 0;
            }
            
            if (Check_Pipe.IsChecked == true)
            {
                cls_SA.CheckPipe = "Checked";
            }
            if (Check_Duct.IsChecked == true)
            {
                cls_SA.CheckDuct = "Checked";
            }
            if (Check_Cabletray.IsChecked == true)
            {
                cls_SA.CheckCableTray = "Checked";
            }

            cls_SA.List_LinkRevitSelect.Clear();
            foreach (CheckBox item in list_Link.Items)
            {
                if (item.IsChecked == true)
                {
                    foreach (Element itemm in cls_SA.List_LinkRevitFirst)
                    {
                        if (itemm.Name == item.Content.ToString())
                        {
                            cls_SA.List_LinkRevitSelect.Add(itemm);
                            break;
                        }
                    }
                }
            }
        }
        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            //if (txt_D.Text.All(char.IsDigit))
            //{
            //    cls_SA.Offset_D = Convert.ToDouble(txt_D.Text) / 304.8;
            //}
            cls_SA.CheckPipe = "";
            cls_SA.CheckDuct = "";
            cls_SA.CheckCableTray = "";
            if (txt_H.Text.All(char.IsDigit))
            {
                cls_SA.Offset_H = Convert.ToDouble(txt_H.Text) / 304.8;
            }
            if (txt_W.Text.All(char.IsDigit))
            {
                cls_SA.Offset_W = Convert.ToDouble(txt_W.Text) / 304.8;
            }
            if (Limit_Dia.IsEnabled == true)
            {
                if (Limit_Dia.Text.All(char.IsDigit))
                {
                    cls_SA.Limit_Dia = Convert.ToDouble(Limit_Dia.Text) / 304.8;
                }
            }
            else if (Limit_Dia.IsEnabled == false)
            {
                cls_SA.Limit_Dia = 0;
            }
            if (Check_Pipe.IsChecked == true)
            {
                cls_SA.CheckPipe = "Checked";
            }
            if (Check_Duct.IsChecked == true)
            {
                cls_SA.CheckDuct = "Checked";
            }
            if (Check_Cabletray.IsChecked == true)
            {
                cls_SA.CheckCableTray = "Checked";
            }

            cls_SA.List_LinkRevitSelect.Clear();
            foreach (CheckBox item in list_Link.Items)
            {
                if (item.IsChecked == true)
                {
                    foreach (Element itemm in cls_SA.List_LinkRevitFirst)
                    {
                        if (itemm.Name == item.Content.ToString())
                        {
                            cls_SA.List_LinkRevitSelect.Add(itemm);
                            break;
                        }
                    }
                }
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Where do you want to save the file?";
            string filename = null;
            if (saveFileDialog.ShowDialog() == true)
            {
                filename = saveFileDialog.FileName;
            }
            if (List_Collision.Items.Count !=0)
            {
                if (filename != null)
                {
                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(filename);
                    foreach (var item in List_Collision.Items)
                    {
                        SaveFile.WriteLine(item);
                    }

                    SaveFile.Close();

                    MessageBox.Show("Saved!");
                }        
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            List_Collision.Items.Clear();
            cls_SA.List_IDWall.Clear();
            cls_SA.CheckPipe = "";
            cls_SA.CheckDuct = "";
            cls_SA.CheckCableTray = "";
            if (Check_Pipe.IsChecked == true)
            {
                cls_SA.CheckPipe = "Checked";
            }
            if (Check_Duct.IsChecked == true)
            {
                cls_SA.CheckDuct = "Checked";
            }
            if (Check_Cabletray.IsChecked == true)
            {
                cls_SA.CheckCableTray = "Checked";
            }
            cls_SA.List_LinkRevitSelect.Clear();
            foreach (CheckBox item in list_Link.Items)
            {
                if (item.IsChecked == true)
                {
                    foreach (Element itemm in cls_SA.List_LinkRevitFirst)
                    {
                        if (itemm.Name == item.Content.ToString())
                        {
                            cls_SA.List_LinkRevitSelect.Add(itemm);
                            break;
                        }
                    }
                }
            }
            if (cls_SA.List_LinkRevitSelect.Count == 0)
            {
                MessageBox.Show("No link selected !");
            }
        }

        

        private void Check_Diameter_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Diameter.IsChecked == true)
            {
                Limit_Dia.IsEnabled = true;
            }
            else
            {
                Limit_Dia.IsEnabled = false;
            }
        }

        private void List_Collision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CommandBinding
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=UUmNhkC05m8&ab_channel=DbimConstructionCompany");
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            ((ToolTip)btn.ToolTip).IsOpen = false;
        }

    }
    
}
