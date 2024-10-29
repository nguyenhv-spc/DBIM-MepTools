using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEP_Tools
{
    public partial class Warnings : Form
    {
        int day;
        public Warnings(int days)
        {
            day = days;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Warnings_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (day <= 0)
            {
                label1.Text = "Tool has expired! \nPlease contact DBIM to reactivate!";
            }
            else if (day <= 5)
            {
                    label1.Text = "Tool only has " + day + " days left !\nPlease contact DBIM to reactivate!";
                }
            }
        }
}
