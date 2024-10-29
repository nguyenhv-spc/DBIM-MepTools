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
    public partial class Contact : Form
    {
        public Contact()
        {          
            InitializeComponent();
        }

        private void Contact_Load(object sender, EventArgs e)
        {       
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_YT.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC1cLSXzlDQyU36baqy3XaTA");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_FB.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.facebook.com/DBIM.VN");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_web.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://dbim.vn/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_FB.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.facebook.com/DBIM.VN");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_web.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://dbim.vn/");
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Specify that the link was visited.
            this.lb_Link_YT.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC1cLSXzlDQyU36baqy3XaTA");
        }
    }
}
