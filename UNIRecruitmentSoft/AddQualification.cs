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

        //public string Qualification { get; set; }

        public string FieldText { get; set; }

        public string FieldValue { get; set; }

        public AddQualification(string fieldText)
        {
            InitializeComponent();
            //Qualification = string.Empty;
            FieldText = fieldText;
            FieldValue = string.Empty;
        }

        private void txtAddQualification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            //Qualification = txtAddQualification.Text;
            FieldValue = txtAddQualification.Text;
            this.Close();
        }

        private void AddQualification_Load(object sender, EventArgs e)
        {
            label1.Text = FieldText;
        }
    }
}
