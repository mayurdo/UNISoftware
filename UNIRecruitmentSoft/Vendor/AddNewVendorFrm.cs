using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace UNIRecruitmentSoft.Vendor
{
    public partial class AddNewVendorFrm : Form
    {
        private VendorDetail _vendor;
        private readonly bool _isNewRecord;
        private readonly UniDBDataContext _uniDb = new UniDBDataContext();
        private readonly UniEnums.VendorType _vendorType;

        public AddNewVendorFrm(UniEnums.VendorType vendorType)
        {
            InitializeComponent();
            _vendor = new VendorDetail() { VendorType = vendorType.ToString(), ProcessedDate = DateTime.Now.Date };
            _isNewRecord = true;
            _vendorType = vendorType;
        }

        public AddNewVendorFrm(long srNo, UniEnums.VendorType vendorType)
        {
            InitializeComponent();

            _vendorType = vendorType;
            _vendor = _uniDb.VendorDetails.Single(x => x.SrNo == srNo
                                                       && x.VendorType == vendorType.ToString());
            _isNewRecord = false;
        }

        #region Form & controls Events
        private void AddNewVendorFrm_Load(object sender, EventArgs e)
        {
            lblHeader.Text = (_vendorType == UniEnums.VendorType.VisitingCard) ? "Visiting Card Detail" : "VTC Detail";

            if (_vendorType == UniEnums.VendorType.VisitingCard)
            {
                cbType.Visible = false;
                lblType.Visible = false;
                lblTypeValidationMark.Visible = false;
                lblDesignation.Visible = true;
                txtDesignation.Visible = true;
            }
            else
            {
                cbType.Visible = true;
                lblType.Visible = true;
                lblTypeValidationMark.Visible = true;
                lblDesignation.Visible = false;
                txtDesignation.Visible = false;
            }

            ResetControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            SaveRecord();

            _vendor = new VendorDetail() { VendorType = _vendorType.ToString(), ProcessedDate = DateTime.Now.Date };
            ResetControls();

            if (!_isNewRecord)
                Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCompanyName.Text))
            {
                this.Close();
                return;
            }

            var dilogResult = MessageBox.Show(@"Do you want to save data before closing.", @"Save Message", MessageBoxButtons.YesNo);

            if (dilogResult != DialogResult.Yes)
            {
                this.Close();
                return;
            }

            buttonSave_Click(sender, e);
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        #endregion


        #region overrid methods

        private void ResetDropDown()
        {
            cmbBusinessType.Items.Clear();
            cmbSector.Items.Clear();
            cmbExpert.Items.Clear();

            cmbBusinessType.Items.AddRange(_uniDb.KeyValueDetails
                                               .Where(x => x.Type == "BusinessType")
                                               .Select(c => (object)c.ValueName)
                                               .ToArray()
                                               .Concat(_uniDb.VendorDetails.Where(x => x.VendorType == _vendorType.ToString()).Select(c => (object)c.BusinessType).ToArray())
                                                                            .Distinct()
                                                                            .OrderBy(x => x)
                                                                            .ToArray());

            cmbSector.Items.AddRange(_uniDb.KeyValueDetails
                                               .Where(x => x.Type == "Sector")
                                               .Select(c => (object)c.ValueName)
                                               .Distinct()
                                               .ToArray()
                                               .Concat(_uniDb.VendorDetails.Where(x => x.VendorType == _vendorType.ToString()).Select(c => (object)c.Sector).ToArray())
                                                                            .Distinct()
                                                                            .OrderBy(x => x)
                                                                            .ToArray());

            cmbExpert.Items.AddRange(_uniDb.VendorDetails.Where(x => x.VendorType == _vendorType.ToString()).OrderBy(x => x.Expert).Select(c => (object)c.Expert).Distinct().ToArray());
        }

        protected void ResetControls()
        {
            ResetDropDown();

            txtCompanyName.Text = _vendor.CompanyName;
            txtAddress.Text = _vendor.OfficeAddress;
            txtContactPerson.Text = _vendor.ContactPerson;
            txtPhoneNo.Text = _vendor.PhoneNo;
            txtMobileNo.Text = _vendor.MobileNo;
            txtEmail.Text = _vendor.Email;
            txtWebsite.Text = _vendor.Website;
            cmbBusinessType.Text = _vendor.BusinessType;
            cmbSector.Text = _vendor.Sector;
            cmbExpert.Text = _vendor.Expert;
            txtSource.Text = _vendor.Source;
            dtpProcessedDate.Value = _vendor.ProcessedDate;
            txtNote.Text = _vendor.Note;
            txtProfile1.Text = _vendor.Profile1;
            txtProfile2.Text = _vendor.Profile2;
            cbNetworking.Checked = _vendor.Networking;
            txtDesignation.Text = _vendor.Designation;

            if (cbType.Visible)
                cbType.Text = _vendor.VendorType;

            if (_vendor.SrNo == 0)
                cbType.Text = string.Empty;
        }

        protected bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                MessageBox.Show("Please Enter Company Name", "Validation Message", MessageBoxButtons.OK);
                txtCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Please Enter Address", "Validation Message", MessageBoxButtons.OK);
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                MessageBox.Show("Please Enter Contact Person", "Validation Message", MessageBoxButtons.OK);
                txtContactPerson.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please Enter Email Id", "Validation Message", MessageBoxButtons.OK);
                txtMobileNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbBusinessType.Text))
            {
                MessageBox.Show("Please Select or Enter Business Type", "Validation Message", MessageBoxButtons.OK);
                cmbBusinessType.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbSector.Text))
            {
                MessageBox.Show("Please Select or Enter Sector", "Validation Message", MessageBoxButtons.OK);
                cmbSector.Focus();
                return false;
            }

            if (_vendorType != UniEnums.VendorType.VisitingCard && string.IsNullOrEmpty(cbType.Text))
            {
                MessageBox.Show("Please Select Type", "Validation Message", MessageBoxButtons.OK);
                cbType.Focus();
                return false;
            }

            return true;
        }

        protected void SaveRecord()
        {
            _vendor.CompanyName = txtCompanyName.Text;
            _vendor.OfficeAddress = txtAddress.Text;
            _vendor.ContactPerson = txtContactPerson.Text;
            _vendor.PhoneNo = txtPhoneNo.Text;
            _vendor.MobileNo = txtMobileNo.Text;
            _vendor.Email = txtEmail.Text;
            _vendor.Website = txtWebsite.Text;
            _vendor.BusinessType = cmbBusinessType.Text;
            _vendor.Sector = cmbSector.Text;
            _vendor.Expert = cmbExpert.Text;
            _vendor.Source = txtSource.Text;
            _vendor.ExecutiveName = HomePage.UserDetail.UserId;
            _vendor.ModifiedDateTime = DateTime.Now;
            _vendor.ProcessedDate = dtpProcessedDate.Value;
            _vendor.Note = txtNote.Text;
            _vendor.Networking = cbNetworking.Checked;
            _vendor.Designation = txtDesignation.Text;

            if (cbType.Visible)
                _vendor.VendorType = cbType.Text;

            var targetPath = string.Format("{0}/{1}/{2}", ConfigurationSettings.AppSettings["CvUploadLocation"], "VendorProfile", _vendor.CompanyName);
            _vendor.Profile1 = CommanFuction.SaveFile(txtProfile1.Text, targetPath);
            _vendor.Profile2 = CommanFuction.SaveFile(txtProfile2.Text, targetPath);

            // for save
            if (_vendor.SrNo < 1)
            {
                var query = _uniDb.VendorDetails.Where(x => x.VendorType == _vendorType.ToString());
                var maxId = query.Any() ? query.Max(c => c.SrNo) : 0;
                _vendor.SrNo = maxId + 1;
                _uniDb.VendorDetails.InsertOnSubmit(_vendor);
                _uniDb.SubmitChanges();
            }
            else
            {
                // for update
                _uniDb.SubmitChanges();
            }
        }

        #endregion


        #region select file

        private string SelectFile()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }

            return string.Empty;
        }
        private void linkSelectLatter1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtProfile1.Text = SelectFile();
        }

        private void linkSelectLatter2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtProfile2.Text = SelectFile();
        }

        #endregion

        private void linkAddNewBusinessType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new AddQualification("Business Type");
            frm.ShowDialog();

            cmbBusinessType.Items.Add(frm.FieldValue);
            cmbBusinessType.Text = frm.FieldValue;
        }

        private void linkAddNewSector_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new AddQualification("Sector");
            frm.ShowDialog();

            cmbSector.Items.Add(frm.FieldValue);
            cmbSector.Text = frm.FieldValue;
        }

        private void linkAddNewExpert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new AddQualification("Expert");
            frm.ShowDialog();

            cmbExpert.Items.Add(frm.FieldValue);
            cmbExpert.Text = frm.FieldValue;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDropDown();
        }
    }
}