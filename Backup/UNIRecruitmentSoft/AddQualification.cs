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
    public partial class AddQualification : Form
    {

        public string Qualification { get; set; }

        public AddQualification()
        {
            InitializeComponent();
            Qualification = string.Empty;
        }

        private void txtAddQualification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            Qualification = txtAddQualification.Text;
            this.Close();
        }
    }
}
