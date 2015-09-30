using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft.UserMng
{
    public partial class UserDetailFrm : Form
    {
        UniDBDataContext uniDb;
        private IList<UserDetail> _userDetails;
        private UserDetail _userDetail;

        public UserDetailFrm()
        {
            InitializeComponent();
            _userDetail = new UserDetail();
        }

        private void UserDetailFrm_Load(object sender, EventArgs e)
        {
            BindGridView();
            ResetAndBindPageControls();
        }

        private void BindGridView()
        {
            uniDb = new UniDBDataContext();
            _userDetails = uniDb.UserDetails.ToList();
            dataGridView1.DataSource = _userDetails;
        }

        private void ResetAndBindPageControls()
        {
            lbModule.DataSource = Enum.GetNames(typeof(UniEnums.Modules)).ToList();

            txtName.Text = _userDetail.ExecutiveName;
            txtMobileNo.Text = _userDetail.MobileNo;
            txtEmail.Text = _userDetail.EmailId;
            txtUserName.Text = _userDetail.UserId;
            txtPassword.Text = _userDetail.Password;

            if (string.IsNullOrEmpty(_userDetail.Modules))
                return;

            var modules = _userDetail.Modules.Split(',');
            foreach (var module in modules)
            {
                lbModule.SelectedItems.Add(module);
            }
        }

        private void SetControlNonEditForAdmin()
        {
            var isAdmin = txtUserName.Text == @"admin";

            txtName.Enabled = !isAdmin;
            txtUserName.Enabled = !isAdmin;
            lbModule.Enabled = !isAdmin;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please Enter Executive name", "Validation Message", MessageBoxButtons.OK);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Please Enter User name", "Validation Message", MessageBoxButtons.OK);
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please Enter Password", "Validation Message", MessageBoxButtons.OK);
                txtPassword.Focus();
                return false;
            }

            if (lbModule.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please Select Module", "Validation Message", MessageBoxButtons.OK);
                lbModule.Focus();
                return false;
            }

            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                _userDetail.ExecutiveName = txtName.Text;
                _userDetail.MobileNo = txtMobileNo.Text;
                _userDetail.EmailId = txtEmail.Text;
                _userDetail.UserId = txtUserName.Text;
                _userDetail.Password = txtPassword.Text;

                var addModules = new List<string>();
                foreach (var item in lbModule.SelectedItems)
                {
                    addModules.Add(item.ToString());
                }
                _userDetail.Modules = string.Join(",", addModules.ToArray());


                if (_userDetail.Id < 1)
                {
                    var maxId = _userDetails.Any() ? _userDetails.Max(x => x.Id) : 0;
                    _userDetail.Id = maxId + 1;
                    uniDb.UserDetails.InsertOnSubmit(_userDetail);
                    uniDb.SubmitChanges();
                }
                else
                {
                    uniDb.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY"))
                {
                    MessageBox.Show(
                        @"User name '" + txtUserName.Text + @"' already exist, Please enter another user name",
                        "Message", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK);                    
                }
            }
            finally
            {

                BindGridView();
            }
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

            _userDetail = uniDb.UserDetails.Single(x => x.Id == srNo);

            ResetAndBindPageControls();

            SetControlNonEditForAdmin();
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbAddQualification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
