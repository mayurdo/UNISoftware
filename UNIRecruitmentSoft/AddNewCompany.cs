using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class AddNewCompany : Form
    {
        private CustomerDetail _company;
        private readonly UniDBDataContext _uniDb = new UniDBDataContext();
        readonly UniEnums.CompanyType _companyType;

        public AddNewCompany(UniEnums.CompanyType companyType)
        {
            InitializeComponent();

            _companyType = companyType;
            _company = new CustomerDetail() { ProcessedDate = DateTime.Now.Date, CompanyType = _companyType.ToString() };
        }

        public AddNewCompany(long srNo, UniEnums.CompanyType companyType)
        {
            InitializeComponent();

            _companyType = companyType;
            _company = _uniDb.CustomerDetails.Single(x => x.SrNo == srNo && x.CompanyType == _companyType.ToString());
        }

        private void AddNewCompany_Load(object sender, EventArgs e)
        {
            lblHeader.Text = (_companyType == UniEnums.CompanyType.Company) ? "Company Detail" : "Customer Detail";

            ResetCompany();
        }

        private void ResetCompany()
        {
            cmbBusinessType.Items.AddRange(_uniDb.KeyValueDetails
                                              .Where(x => x.Type == "BusinessType")
                                              .Select(c => (object)c.ValueName)
                                              .ToArray()
                                              .Concat(_uniDb.CustomerDetails.Where(x => x.CompanyType == _companyType.ToString()).Select(c => (object)c.BusinessType).ToArray())
                                                                           .Distinct()
                                                                           .OrderBy(x => x)
                                                                           .ToArray());

            cmbSector.Items.AddRange(_uniDb.KeyValueDetails
                                               .Where(x => x.Type == "Sector")
                                               .Select(c => (object)c.ValueName)
                                               .Distinct()
                                               .ToArray()
                                               .Concat(_uniDb.CustomerDetails.Where(x => x.CompanyType == _companyType.ToString()).Select(c => (object)c.Sector).ToArray())
                                                                            .Distinct()
                                                                            .OrderBy(x => x)
                                                                            .ToArray());

            cmbExpert.Items.AddRange(_uniDb.CustomerDetails.Where(x => x.CompanyType == _companyType.ToString()).OrderBy(x => x.Expert).Select(c => (object)c.Expert).Distinct().ToArray());

            txtCompanyName.Text = _company.CompanyName;
            txtOfficeAddress.Text = _company.OfficeAddress;
            txtFactoryAddress.Text = _company.FactoryAddress;
            txtContactPerson.Text = _company.ContactPerson;
            txtPhoneNo.Text = _company.PhoneNo;
            txtMobileNo.Text = _company.MobileNo;
            cmbBusinessType.Text = _company.BusinessType;
            cmbSector.Text = _company.Sector;
            cmbExpert.Text = _company.Expert;
            txtEstablishedYear.Text = (_company.EstablishedYear ?? 0).ToString(CultureInfo.InvariantCulture);
            txtAnnualTurnover.Text = _company.AnnualTurnover;
            txtNoOfEmployee.Text = (_company.NoOfEmployees ?? 0).ToString(CultureInfo.InvariantCulture);
            txtWebsite.Text = _company.Website;
            txtEmail.Text = _company.Email;
            cmbSource.Text = _company.Source;
            dtpProcessedDate.Value = _company.ProcessedDate;
            cbNetworking.Checked = _company.Networking;
            txtNote.Text = _company.Note;
            txtProfile1.Text = _company.Profile1;
            txtProfile2.Text = _company.Profile2;
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            _company.CompanyName = txtCompanyName.Text;
            _company.OfficeAddress = txtOfficeAddress.Text;
            _company.FactoryAddress = txtFactoryAddress.Text;
            _company.ContactPerson = txtContactPerson.Text;
            _company.PhoneNo = txtPhoneNo.Text;
            _company.MobileNo = txtMobileNo.Text;
            _company.BusinessType = cmbBusinessType.Text;
            _company.Sector = cmbSector.Text;
            _company.Expert = cmbExpert.Text;
            _company.AnnualTurnover = txtAnnualTurnover.Text;
            _company.EstablishedYear = string.IsNullOrEmpty(txtEstablishedYear.Text)
                                           ? 0
                                           : Convert.ToInt32(txtEstablishedYear.Text);
            _company.NoOfEmployees = string.IsNullOrEmpty(txtNoOfEmployee.Text)
                                           ? 0
                                           : Convert.ToInt32(txtNoOfEmployee.Text);
            _company.Website = txtWebsite.Text;
            _company.Email = txtEmail.Text;
            _company.Source = cmbSource.Text;
            _company.ExecutiveName = HomePage.UserDetail.UserId;
            _company.ModifiedDateTime = DateTime.Now;
            _company.ProcessedDate = dtpProcessedDate.Value;
            _company.Networking = cbNetworking.Checked;

            var targetPath = string.Format("{0}/{1}/{2}", ConfigurationSettings.AppSettings["CvUploadLocation"], "Client", _company.CompanyName);
            _company.Note = txtNote.Text;
            _company.Profile1 = CommanFuction.SaveFile(txtProfile1.Text, targetPath);
            _company.Profile2 = CommanFuction.SaveFile(txtProfile2.Text, targetPath);

            // for save
            if (_company.SrNo < 1)
            {
                var maxId = _uniDb.CustomerDetails.Any() ? _uniDb.CustomerDetails.Where(c => c.CompanyType == _companyType.ToString()).Max(c => c.SrNo) : 0;
                _company.SrNo = maxId + 1;
                _uniDb.CustomerDetails.InsertOnSubmit(_company);
                _uniDb.SubmitChanges();
                _company = new CustomerDetail() { ProcessedDate = DateTime.Now.Date, CompanyType = _companyType.ToString() };
                ResetCompany();
                return;
            }

            // for update
            _uniDb.SubmitChanges();
            ResetCompany();
            Close();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                MessageBox.Show("Please Enter Company Name", "Validation Message", MessageBoxButtons.OK);
                txtCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtOfficeAddress.Text))
            {
                MessageBox.Show("Please Enter Address", "Validation Message", MessageBoxButtons.OK);
                txtOfficeAddress.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                MessageBox.Show("Please Enter Contact Person", "Validation Message", MessageBoxButtons.OK);
                txtContactPerson.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                MessageBox.Show("Please Enter Mobile Number", "Validation Message", MessageBoxButtons.OK);
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

            if (_company.SrNo > 0)
            {
                return true;
            }

            var duplicateRecord = _uniDb.CustomerDetails.Where(x => x.CompanyType == _companyType.ToString() && x.CompanyName == txtCompanyName.Text);
            if (duplicateRecord.Count() > 0)
            {
                MessageBox.Show(string.Format("Company Name : {0} already exist", txtCompanyName.Text),
                                "Validation Message", MessageBoxButtons.OK);
                txtCompanyName.Focus();
                return false;
            }

            return true;
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

        private void txtEstablishedYear_TextChanged(object sender, EventArgs e)
        {

        }

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

        private void label17_Click(object sender, EventArgs e)
        {

        }

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
    }
}
