using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using UNIEntity;

namespace UNIWebApiClient
{
    public partial class Loginfrm : Form
    {
        public bool IsLogedIn { get; set; }

        //public string ExecutiveName { get; set; }

        public UserDetail UserDetail { get; set; }

        public Loginfrm(UserDetail userDetail)
        {
            InitializeComponent();
            //ExecutiveName = userDetail.UserId;
            UserDetail = userDetail;
            IsLogedIn = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            ValidateUser();

            //if (txtPassword.Text.ToUpper() == ConfigurationSettings.AppSettings["password"].ToUpper())
            //{
            //    ExecutiveName = txtExecutiveName.Text;
            //    IsLogedIn = true;
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("You have enter wrong password ", "Message", MessageBoxButtons.OK);
            //}
        }

        private void Loginfrm_Load(object sender, EventArgs e)
        {
            txtExecutiveName.Text = UserDetail.UserId;
            txtExecutiveName.Enabled = string.IsNullOrEmpty(txtExecutiveName.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateUser();
        }

        private void ValidateUser()
        {
            if (string.IsNullOrEmpty(txtExecutiveName.Text))
            {
                MessageBox.Show("Please Enter Executive Name", "Message", MessageBoxButtons.OK);
                return;
            }

            if (UserDetail.SrNo <= 0)
            {
                var uniDb = new UniDBDataContext();
                UserDetail = uniDb.UserDetails.SingleOrDefault(x => x.UserId == txtExecutiveName.Text);
            }

            if (UserDetail == null)
            {
                MessageBox.Show("You have enter wrong User Name", "Message", MessageBoxButtons.OK);
                return;
            }

            if (UserDetail.Password.ToUpper() == txtPassword.Text.ToUpper())
            {
                IsLogedIn = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("You have enter wrong password ", "Message", MessageBoxButtons.OK);
            }

        }
    }
}
