using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using UNIEntity;

namespace UNIWebApiClient
{
    public partial class Candidatefrm : Form
    {
        private CandidateDetail _candidate;
        private CandidatePageRequired _candidatePageRequired;

        public Candidatefrm()
        {
            InitializeComponent();
            _candidate = new CandidateDetail()
            {
                ProcessedDate = DateTime.Now,
                DateOfBirth = DateTime.Now.AddYears(-15),
                Gender = "Male",
                MarritalStatus = "Unmarried",
                AdditionalQualification = string.Empty
            };
        }

        public Candidatefrm(CandidateDetail candidateDetail)
        {
            InitializeComponent();
            _candidate = candidateDetail;
        }

        private void Candidatefrm_Load(object sender, EventArgs e)
        {
            _candidatePageRequired = MethodHelper.GetServicePageDataResponse<CandidatePageRequired>("api/CandidateApi/GetPageData");
            ResetCandidate();
        }

        private void ResetCandidate()
        {
            FillComboBox();

            cmbLink.Text = _candidate.Link;
            dtpProcessedDate.Value = _candidate.ProcessedDate;

            txtName.Text = _candidate.Name;
            txtCandidateCode.Text = _candidate.CandidateCode.ToString(CultureInfo.InvariantCulture);
            rbMale.Checked = (_candidate.Gender == "Male");
            rbFemale.Checked = (_candidate.Gender == "Female");
            dtpDateOfBirth.Value = _candidate.DateOfBirth;
            txtAge.Text = _candidate.Age.ToString(CultureInfo.InvariantCulture);
            rbUnmarried.Checked = (_candidate.MarritalStatus == "Unmarried");
            rbMarried.Checked = (_candidate.MarritalStatus == "Married");
            txtMobileNo.Text = _candidate.MobileNo;
            txtLandLineNo.Text = _candidate.LandLineNo;
            txtEmail.Text = _candidate.Email;

            cmbCurrentLocation.Text = _candidate.CurrentLocation;
            cmbPreferedLocation.Text = _candidate.PreferedLocation;
            cmbQualification.Text = _candidate.Qualification;
            txtExperience.Text = _candidate.Experience.ToString(CultureInfo.InvariantCulture);
            cmbExpSlab.Text = _candidate.ExpSlap;
            cmbJobTitle.Text = _candidate.JobTitle;
            cmbCurrentIndustry.Text = _candidate.CurrentIndustry;
            cmbPreferredIndustry.Text = _candidate.PreferredIndustry;
            cmbNoticePeriod.Text = _candidate.NoticePeriod;
            txtCurrentCTC.Text = _candidate.CurrentCTC;
            txtExpectedCTC.Text = _candidate.ExpectedCTC;

            txtAttachCV.Text = _candidate.CVPath;
            txtRemark.Text = _candidate.Remarks;

            var additionalQualifications = _candidate.AdditionalQualification.Split(',');

            foreach (var addQualification in additionalQualifications)
            {
                lbAddQualification.SelectedItems.Add(addQualification);
            }

            txtName.Focus();
        }

        private void FillComboBox()
        {
            cmbLink.DataSource = _candidatePageRequired.Links;
            cmbCurrentLocation.DataSource = _candidatePageRequired.CurrentLocations;
            cmbPreferedLocation.DataSource = _candidatePageRequired.PreferedLocations;
            cmbQualification.DataSource = _candidatePageRequired.Qualifications;
            cmbCurrentIndustry.DataSource = _candidatePageRequired.CurrentIndustries;
            cmbPreferredIndustry.DataSource = _candidatePageRequired.PreferredIndustries;
            cmbNoticePeriod.DataSource = _candidatePageRequired.NoticePeriods;

            lbAddQualification.Items.AddRange(_candidatePageRequired.AdditionalQualifications.Select(x => (object)x).ToArray());
            cmbJobTitle.Items.AddRange(_candidatePageRequired.JobTitles.Select(x => (object)x).ToArray());

            //CommanFuction.AutoComplete(txtMobileNo, db.CandidateDetails.Select(x => x.MobileNo).Distinct().ToArray());

        }

        private void txtExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            MethodHelper.AcceptOnlyNumber(e);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            MethodHelper.AcceptOnlyNumber(e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAttachCV.Text = openFileDialog1.FileName;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtMobileNo.Text))
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                _candidate.Link = cmbLink.Text;
                _candidate.ProcessedDate = dtpProcessedDate.Value;

                _candidate.Name = txtName.Text;
                _candidate.CandidateCode = string.IsNullOrEmpty(txtCandidateCode.Text) ? 0 : Convert.ToInt32(txtCandidateCode.Text);
                _candidate.Gender = (rbMale.Checked) ? "Male" : "Female";
                _candidate.DateOfBirth = dtpDateOfBirth.Value;
                _candidate.Age = string.IsNullOrEmpty(txtAge.Text) ? 0 : Convert.ToInt32(txtAge.Text);
                _candidate.MarritalStatus = (rbUnmarried.Checked) ? "Unmarried" : "Married";
                _candidate.MobileNo = txtMobileNo.Text;
                _candidate.LandLineNo = txtLandLineNo.Text;
                _candidate.Email = txtEmail.Text;

                _candidate.CurrentLocation = cmbCurrentLocation.Text;
                _candidate.PreferedLocation = cmbPreferedLocation.Text;
                _candidate.Qualification = cmbQualification.Text;
                _candidate.Experience = string.IsNullOrEmpty(txtExperience.Text) ? 0 : Convert.ToInt32(txtExperience.Text);
                _candidate.ExpSlap = cmbExpSlab.Text;

                _candidate.JobTitle = cmbJobTitle.Text;
                _candidate.CurrentIndustry = cmbCurrentIndustry.Text;
                _candidate.PreferredIndustry = cmbPreferredIndustry.Text;
                _candidate.NoticePeriod = cmbNoticePeriod.Text;
                _candidate.CurrentCTC = txtCurrentCTC.Text;
                _candidate.ExpectedCTC = txtExpectedCTC.Text;

                _candidate.Remarks = txtRemark.Text;
                //_candidate.ExecutiveName = HomePage.UserDetail.UserId;
                _candidate.ModifiedDateTime = DateTime.Now;

                SaveAttachCV(_candidate);

                var addQulifications = new List<string>();
                foreach (var item in lbAddQualification.SelectedItems)
                {
                    addQulifications.Add(item.ToString());
                }
                _candidate.AdditionalQualification = string.Join(",", addQulifications.ToArray());


                var response = MethodHelper.GetServiceResponse<CandidateDetail>("api/CandidateApi/Save", _candidate);
                if (!response.IsSuccess)
                {

                }

                // if new candidate
                if (_candidate.SrNo < 1)
                {
                    _candidate = new CandidateDetail()
                    {
                        ProcessedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-15),
                        Gender = "Male",
                        MarritalStatus = "Unmarried",
                        AdditionalQualification = string.Empty
                    };

                    ResetCandidate();
                    return;
                }

                ResetCandidate();
                this.Close();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, @"Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), @"Message", MessageBoxButtons.OK);
            }
        }

        private bool ValidateForm()
        {
            //if (string.IsNullOrEmpty(cmbLink.Text))
            //{
            //    MessageBox.Show("Please Select or Enter Link", "Validation Message", MessageBoxButtons.OK);
            //    cmbLink.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please Enter Candidate name", @"Validation Message", MessageBoxButtons.OK);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCandidateCode.Text))
            {
                MessageBox.Show(@"Please Enter Candidate code", @"Validation Message", MessageBoxButtons.OK);
                txtCandidateCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAge.Text) || Convert.ToInt32(txtAge.Text) < 14)
            {
                MessageBox.Show(@"Please Select Date of Birth", @"Validation Message", MessageBoxButtons.OK);
                dtpDateOfBirth.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                MessageBox.Show(@"Please Enter Mobile Number", @"Validation Message", MessageBoxButtons.OK);
                txtMobileNo.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(txtEmail.Text))
            //{
            //    MessageBox.Show("Please Enter Email", "Validation Message", MessageBoxButtons.OK);
            //    txtEmail.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(cmbCurrentLocation.Text))
            //{
            //    MessageBox.Show("Please Select or Enter Current Location", "Validation Message", MessageBoxButtons.OK);
            //    cmbCurrentLocation.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(cmbPreferedLocation.Text))
            {
                MessageBox.Show(@"Please Select or Enter Prefered Location", @"Validation Message", MessageBoxButtons.OK);
                cmbPreferedLocation.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbQualification.Text))
            {
                MessageBox.Show(@"Please Select or Enter Qualification", @"Validation Message", MessageBoxButtons.OK);
                cmbQualification.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtExperience.Text))
            {
                MessageBox.Show(@"Please Enter Experience", @"Validation Message", MessageBoxButtons.OK);
                txtExperience.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbJobTitle.Text))
            {
                MessageBox.Show(@"Please select or Enter Job Title", @"Validation Message", MessageBoxButtons.OK);
                cmbJobTitle.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(cmbCurrentIndustry.Text))
            //{
            //    MessageBox.Show("Please select or Enter Current Industry", "Validation Message", MessageBoxButtons.OK);
            //    cmbCurrentIndustry.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(cmbPreferredIndustry.Text))
            //{
            //    MessageBox.Show("Please select or Enter Preferred Industry", "Validation Message", MessageBoxButtons.OK);
            //    cmbPreferredIndustry.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(cmbNoticePeriod.Text))
            //{
            //    MessageBox.Show("Please select or Enter Notice", "Validation Message", MessageBoxButtons.OK);
            //    cmbNoticePeriod.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtCurrentCTC.Text))
            //{
            //    MessageBox.Show("Please Enter Current CTC", "Validation Message", MessageBoxButtons.OK);
            //    txtCurrentCTC.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtExpectedCTC.Text))
            //{
            //    MessageBox.Show("Please Enter Expected CTC", "Validation Message", MessageBoxButtons.OK);
            //    txtExpectedCTC.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtNoOfCalls.Text))
            {
                MessageBox.Show(@"Please Enter No of Calls", @"Validation Message", MessageBoxButtons.OK);
                txtNoOfCalls.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(txtAttachCV.Text))
            //{
            //    MessageBox.Show("Please select CV", "Validation Message", MessageBoxButtons.OK);
            //    linkLabel1.Focus();
            //    return false;
            //}

            return true;
        }

        private void SaveAttachCV(CandidateDetail candidate)
        {
            if (string.IsNullOrEmpty(txtAttachCV.Text))
                return;

            var targetPath = string.Format("{0}/{1}/{2}", ConfigurationSettings.AppSettings["CvUploadLocation"], candidate.JobTitle, candidate.Experience);
            var targetDir = new DirectoryInfo(targetPath);
            if (!targetDir.Exists)
            {
                targetDir.Create();
            }

            var selFile = new FileInfo(txtAttachCV.Text);

            if (!selFile.Exists)
            {
                throw new FileNotFoundException(string.Format("Selected File {0} is not Found In location {0}", selFile.Name, selFile.DirectoryName));
            }

            var fileName = System.IO.Path.GetFileName(selFile.FullName);
            var destFile = System.IO.Path.Combine(targetDir.FullName, fileName);

            if (selFile.FullName == destFile)
                return;

            System.IO.File.Copy(selFile.FullName, destFile, true);
            candidate.CVPath = destFile;
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            // calculate age
            var bday = dtpDateOfBirth.Value;
            var today = DateTime.Today;
            var age = today.Year - bday.Year;

            if (bday > today.AddYears(-age))
                age--;

            txtAge.Text = age.ToString();
        }

        private void linkAddNewQualification_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //var frm = new AddQualification("Qualification");
            //frm.ShowDialog();

            //lbAddQualification.Items.Add(frm.FieldValue);
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

        private void linkAddNewJobTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //var frm = new AddQualification("Job Title");
            //frm.ShowDialog();

            //cmbJobTitle.Items.Add(frm.FieldValue);
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            //var mobileNoExist = db.CandidateDetails.Any(x => x.MobileNo == txtMobileNo.Text);

            //if (mobileNoExist)
            //{
            //    MessageBox.Show(@"Mobile Number Already Exist", @"Duplicate Error", MessageBoxButtons.OK);
            //}
        }
    }
}
