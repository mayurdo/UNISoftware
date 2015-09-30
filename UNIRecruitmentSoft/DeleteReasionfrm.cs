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
    public partial class DeleteReasionfrm : Form
    {
        public static string DeleteReasion { get; set; }


        public DeleteReasionfrm()
        {
            InitializeComponent();
        }

        private void DeleteReasionfrm_Load(object sender, EventArgs e)
        {

        }

        private void txtDeleteReasion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (string.IsNullOrEmpty(txtDeleteReasion.Text))
            {
                MessageBox.Show("Please enter delete reasion", "Message", MessageBoxButtons.OK);
                return;
            }

            DeleteReasion = txtDeleteReasion.Text;
            this.Close();
        }

       
    }
}
