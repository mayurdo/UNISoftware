using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class HomePage : Form
    {
        public static string ExecutiveName { get; set; }

        public HomePage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loginfrm frm = new Loginfrm(string.Empty);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                this.Close();

            ExecutiveName = frm.ExecutiveName;
        }

        private void pnlCandidateDetails_MouseClick(object sender, MouseEventArgs e)
        {
            CandidateReport frm = new CandidateReport();
            frm.ShowDialog();
        }
    }
}
