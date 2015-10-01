using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNIWebSiteReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                //string result = client.DownloadString(txtName.Text);
                //// TODO: do something with the downloaded result from the remote
                //// web site

                //textBox1.Text = result;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var str = textBox1.Text;


            var strLines = str.Split(new[] {'\n'});
            txtHeading.Text = strLines[0].Trim();
                //str.Substring(0, str.IndexOf(Environment.NewLine, StringComparison.Ordinal));

            var secoundLineWords = strLines[1].Split(',');
            txtCompanyName.Text = secoundLineWords[0].Trim();
            txtExperience.Text = secoundLineWords[1].Trim();
            txtLocation.Text = secoundLineWords[2].Trim();
            txtCity.Text = secoundLineWords[3].Trim();

            var role = strLines[2].Substring(("Role: ").Length);
            txtRole.Text = role.Trim();

            var keySkills = strLines[3].Substring(("Key Skills: ").Length);
            txtKeySkils.Text = keySkills.Trim();
        }
    }
}
