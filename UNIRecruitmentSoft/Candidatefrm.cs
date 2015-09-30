using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace UNIRecruitmentSoft
{
    public partial class Candidatefrm : Form
    {
        private CandidateDetail _candidate;
        private UniDBDataContext db = new UniDBDataContext();

        public Candidatefrm()
        {
            InitializeComponent();
            _candidate = new CandidateDetail()
            {
                ProcessedDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                MarritalStatus = "Unmarried",
                AdditionalQualification = string.Empty,
                Placed = false,
                Registered = false
            };

        }

        public Candidatefrm(long srNo)
        {
            InitializeComponent();
            _candidate = db.CandidateDetails.Single(x => x.SrNo == srNo);
        }

        private void Candidatefrm_Load(object sender, EventArgs e)
        {
            var dbAddQualifications = db.CandidateDetails.Where(x => x.AdditionalQualification != string.Empty).Select(x => x.AdditionalQualification);
            var addQualifications = new List<string>();
            foreach (var dbAddQualification in dbAddQualifications)
            {
                var qualifications = dbAddQualification.Split(',');
                addQualifications.AddRange(qualifications);
            }
            lbAddQualification.Items.AddRange(addQualifications.Distinct().OrderBy(x => x).ToArray());

            ResetCandidate();
        }

        private void ResetCandidate()
        {
            FillComboBox();

            cmbLink.Text = _candidate.Link;
            dtpProcessedDate.Value = _candidate.ProcessedDate;

            txtName.Text = _candidate.Name;
            txtCandidateCode.Text = _candidate.CandidateCode.ToString();
            rbMale.Checked = (_candidate.Gender == "Male");
            rbFemale.Checked = (_candidate.Gender == "Female");
            dtpDateOfBirth.Value = _candidate.DateOfBirth;
            txtAge.Text = _candidate.Age.ToString();
            rbUnmarried.Checked = (_candidate.MarritalStatus == "Unmarried");
            rbMarried.Checked = (_candidate.MarritalStatus == "Married");
            txtMobileNo.Text = _candidate.MobileNo;
            txtLandLineNo.Text = _candidate.LandLineNo;
            txtEmail.Text = _candidate.Email;

            cmbCurrentLocation.Text = _candidate.CurrentLocation;
            cmbPreferedLocation.Text = _candidate.PreferedLocation;
            cmbQualification.Text = _candidate.Qualification;
            txtExperience.Text = _candidate.Experience.ToString();
            cmbExpSlab.Text = _candidate.ExpSlap;
            cmbJobTitle.Text = _candidate.JobTitle;
            cmbCurrentIndustry.Text = _candidate.CurrentIndustry;
            cmbPreferredIndustry.Text = _candidate.PreferredIndustry;
            cmbNoticePeriod.Text = _candidate.NoticePeriod;
            txtCurrentCTC.Text = _candidate.CurrentCTC;
            txtExpectedCTC.Text = _candidate.ExpectedCTC;
            txtNoOfCalls.Text = _candidate.NoOfCalls.ToString();

            txtAttachCV.Text = _candidate.CVPath;
            txtCallLatter1.Text = _candidate.CallLetter1;
            txtCallLatter2.Text = _candidate.CallLetter2;
            txtCallLatter3.Text = _candidate.CallLetter3;
            txtCallLatter4.Text = _candidate.CallLetter4;
            txtCallLatter5.Text = _candidate.CallLetter5;

            cbPlaced.Checked = _candidate.Placed;
            cbRegistered.Checked = _candidate.Registered;
            txtRemark.Text = _candidate.Remarks;

            var additionalQualifications = (_candidate.AdditionalQualification != null)
                                                                ? _candidate.AdditionalQualification.Split(',')
                                                                : new string[] { };

            foreach (var addQualification in additionalQualifications)
            {
                lbAddQualification.SelectedItems.Add(addQualification);
            }

            txtName.Focus();
        }

        private void FillComboBox()
        {
            cmbLink.DataSource = db.CandidateDetails.Select(c => c.Link).Distinct().OrderBy(x => x);
            cmbCurrentLocation.DataSource = db.CandidateDetails.Select(c => c.CurrentLocation).Distinct().OrderBy(x => x);
            cmbPreferedLocation.DataSource = db.CandidateDetails.Select(c => c.PreferedLocation).Distinct().OrderBy(x => x);
            cmbQualification.DataSource = db.CandidateDetails.Select(c => c.Qualification).Distinct().OrderBy(x => x);
            cmbCurrentIndustry.DataSource = db.CandidateDetails.Select(c => c.CurrentIndustry).Distinct().OrderBy(x => x);
            cmbPreferredIndustry.DataSource = db.CandidateDetails.Select(c => c.PreferredIndustry).Distinct().OrderBy(x => x);
            cmbNoticePeriod.DataSource = db.CandidateDetails.Select(c => c.NoticePeriod).Distinct().OrderBy(x => x);

            cmbJobTitle.Items.AddRange(db.CandidateDetails.OrderBy(c => c.JobTitle).Select(c => (object)c.JobTitle).Distinct().ToArray());

            //CommanFuction.AutoComplete(txtMobileNo, db.CandidateDetails.Select(x => x.MobileNo).Distinct().ToArray());

        }

        private void txtExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!openFileDialog1.FileName.Contains(txtCandidateCode.Text))
                {
                    MessageBox.Show(@"File Name not match with Candidate Code, Please try again", @"Validation Message",
                        MessageBoxButtons.OK);
                    return;
                }
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
                _candidate.NoOfCalls = string.IsNullOrEmpty(txtNoOfCalls.Text) ? 0 : Convert.ToInt32(txtNoOfCalls.Text);
                _candidate.Remarks = txtRemark.Text;
                _candidate.Registered = cbRegistered.Checked;
                _candidate.Placed = cbPlaced.Checked;
                _candidate.ExecutiveName = HomePage.UserDetail.UserId;
                _candidate.ModifiedDateTime = DateTime.Now;

                SaveAttachCV(_candidate);
                _candidate.CallLetter1 = SaveCallLatter(_candidate, txtCallLatter1.Text);
                _candidate.CallLetter2 = SaveCallLatter(_candidate, txtCallLatter2.Text);
                _candidate.CallLetter3 = SaveCallLatter(_candidate, txtCallLatter3.Text);
                _candidate.CallLetter4 = SaveCallLatter(_candidate, txtCallLatter4.Text);
                _candidate.CallLetter5 = SaveCallLatter(_candidate, txtCallLatter5.Text);

                var addQulifications = new List<string>();
                foreach (var item in lbAddQualification.SelectedItems)
                {
                    addQulifications.Add(item.ToString());
                }
                _candidate.AdditionalQualification = string.Join(",", addQulifications.ToArray());

                // if new candidate
                if (_candidate.SrNo < 1)
                {
                    if (cbAutoCandidateCode.Checked)
                    {
                        var maxCandidateCode = db.CandidateDetails.Any() ? db.CandidateDetails.Select(c => c.CandidateCode).Max() : 0;
                        _candidate.CandidateCode = maxCandidateCode + 1;
                    }

                    var maxId = db.CandidateDetails.Any() ? db.CandidateDetails.Select(c => c.SrNo).Max() : 0;
                    _candidate.SrNo = maxId + 1;
                    db.CandidateDetails.InsertOnSubmit(_candidate);
                    db.SubmitChanges();
                    _candidate = new CandidateDetail()
                    {
                        ProcessedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now,
                        Gender = "Male",
                        MarritalStatus = "Unmarried",
                        AdditionalQualification = string.Empty,
                        Placed = false,
                        Registered = false
                    };

                    ResetCandidate();
                    return;
                }

                // if existing candidate
                db.SubmitChanges();
                ResetCandidate();
                this.Close();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Message", MessageBoxButtons.OK);
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
                MessageBox.Show("Please Enter Candidate name", "Validation Message", MessageBoxButtons.OK);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCandidateCode.Text) && !cbAutoCandidateCode.Checked)
            {
                MessageBox.Show("Please Enter Candidate code", "Validation Message", MessageBoxButtons.OK);
                txtCandidateCode.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(txtAge.Text) || Convert.ToInt32(txtAge.Text) < 14)
            //{
            //    MessageBox.Show("Please Select Date of Birth", "Validation Message", MessageBoxButtons.OK);
            //    dtpDateOfBirth.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                MessageBox.Show("Please Enter Mobile Number", "Validation Message", MessageBoxButtons.OK);
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

            //if (string.IsNullOrEmpty(cmbPreferedLocation.Text))
            //{
            //    MessageBox.Show("Please Select or Enter Prefered Location", "Validation Message", MessageBoxButtons.OK);
            //    cmbPreferedLocation.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(cmbQualification.Text))
            {
                MessageBox.Show("Please Select or Enter Qualification", "Validation Message", MessageBoxButtons.OK);
                cmbQualification.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtExperience.Text))
            {
                MessageBox.Show("Please Enter Experience", "Validation Message", MessageBoxButtons.OK);
                txtExperience.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(cmbJobTitle.Text))
            //{
            //    MessageBox.Show("Please select or Enter Job Title", "Validation Message", MessageBoxButtons.OK);
            //    cmbJobTitle.Focus();
            //    return false;
            //}

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
                MessageBox.Show(@"Please Enter No of Calls", "Validation Message", MessageBoxButtons.OK);
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

        private static string SaveCallLatter(CandidateDetail candidate, string sourceFileName)
        {
            if (string.IsNullOrEmpty(sourceFileName))
                return string.Empty;

            var targetPath = string.Format("{0}/{1}/{2}/{3}", ConfigurationSettings.AppSettings["CvUploadLocation"], candidate.JobTitle, candidate.Experience, candidate.Name);
            var targetDir = new DirectoryInfo(targetPath);
            if (!targetDir.Exists)
            {
                targetDir.Create();
            }

            var selFile = new FileInfo(sourceFileName);

            if (!selFile.Exists)
            {
                throw new FileNotFoundException(string.Format("Selected File {0} is not Found In location {0}", selFile.Name, selFile.DirectoryName));
            }

            var fileName = System.IO.Path.GetFileName(selFile.FullName);
            var destFile = System.IO.Path.Combine(targetDir.FullName, fileName);

            if (selFile.FullName == destFile)
            {
                return destFile;
            }

            System.IO.File.Copy(selFile.FullName, destFile, true);
            return destFile;
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
            var frm = new AddQualification("Qualification");
            frm.ShowDialog();

            lbAddQualification.Items.Add(frm.FieldValue);
        }

        private void linkSelectLatter1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCallLatter1.Text = SelectFile();
        }

        private void linkSelectLatter2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCallLatter2.Text = SelectFile();
        }

        private void linkSelectLatter3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCallLatter3.Text = SelectFile();
        }

        private void linkSelectLatter4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCallLatter4.Text = SelectFile();
        }

        private void linkSelectLatter5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCallLatter5.Text = SelectFile();
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
            var frm = new AddQualification("Job Title");
            frm.ShowDialog();

            cmbJobTitle.Items.Add(frm.FieldValue);
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            var mobileNoExist = db.CandidateDetails.Any(x => x.MobileNo == txtMobileNo.Text);

            if (mobileNoExist)
            {
                MessageBox.Show(@"Mobile Number Already Exist", @"Duplicate Error", MessageBoxButtons.OK);
            }
        }
    }
}
