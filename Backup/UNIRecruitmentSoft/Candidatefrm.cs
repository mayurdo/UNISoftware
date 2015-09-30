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
        private readonly bool isNewCandidate;

        public Candidatefrm()
        {
            InitializeComponent();
            _candidate = new CandidateDetail()
            {
                ProcessedDate = DateTime.Now,
                DateOfBirth = DateTime.Now.AddYears(-15),
                Gender = "Male",
                MarritalStatus = "Unmarried",
                AdditionalQualification = string.Empty,
                Placed = false,
                Registered = false
            };

            isNewCandidate = true;
        }

        public Candidatefrm(long srNo)
        {
            InitializeComponent();
            _candidate = db.CandidateDetails.Single(x => x.SrNo == srNo);
            isNewCandidate = false;
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
            txtCallLatter1.Text = _candidate.CallLatter1;
            txtCallLatter2.Text = _candidate.CallLatter2;
            txtCallLatter3.Text = _candidate.CallLatter3;
            txtCallLatter4.Text = _candidate.CallLatter4;
            txtCallLatter5.Text = _candidate.CallLatter5;

            cbPlaced.Checked = _candidate.Placed;
            cbRegistered.Checked = _candidate.Registered;
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
            cmbLink.DataSource = db.CandidateDetails.Select(c => c.Link).Distinct();
            cmbCurrentLocation.DataSource = db.CandidateDetails.Select(c => c.CurrentLocation).Distinct();
            cmbPreferedLocation.DataSource = db.CandidateDetails.Select(c => c.PreferedLocation).Distinct();
            cmbQualification.DataSource = db.CandidateDetails.Select(c => c.Qualification).Distinct();
            cmbJobTitle.DataSource = db.CandidateDetails.Select(c => c.JobTitle).Distinct();
            cmbCurrentIndustry.DataSource = db.CandidateDetails.Select(c => c.CurrentIndustry).Distinct();
            cmbPreferredIndustry.DataSource = db.CandidateDetails.Select(c => c.PreferredIndustry).Distinct();
            cmbNoticePeriod.DataSource = db.CandidateDetails.Select(c => c.NoticePeriod).Distinct();
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
                txtAttachCV.Text = openFileDialog1.FileName;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.Close();
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
                _candidate.ExecutiveName = HomePage.ExecutiveName;

                SaveAttachCV(_candidate);
                _candidate.CallLatter1 = SaveCallLatter(_candidate, txtCallLatter1.Text);
                _candidate.CallLatter2 = SaveCallLatter(_candidate, txtCallLatter2.Text);
                _candidate.CallLatter3 = SaveCallLatter(_candidate, txtCallLatter3.Text);
                _candidate.CallLatter4 = SaveCallLatter(_candidate, txtCallLatter4.Text);
                _candidate.CallLatter5 = SaveCallLatter(_candidate, txtCallLatter5.Text);

                var addQulifications = new List<string>();
                foreach (var item in lbAddQualification.SelectedItems)
                {
                    addQulifications.Add(item.ToString());
                }
                _candidate.AdditionalQualification = string.Join(",", addQulifications.ToArray());

                // if new candidate
                if (_candidate.SrNo < 1)
                {
                    var maxID = db.CandidateDetails.Count() > 0 ? db.CandidateDetails.Select(c => c.SrNo).Max() : 0;
                    _candidate.SrNo = maxID + 1;
                    db.CandidateDetails.InsertOnSubmit(_candidate);
                    db.SubmitChanges();
                    _candidate = new CandidateDetail()
                    {
                        ProcessedDate = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-15),
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
            if (string.IsNullOrEmpty(cmbLink.Text))
            {
                MessageBox.Show("Please Select or Enter Link", "Validation Message", MessageBoxButtons.OK);
                cmbLink.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please Enter Candidate name", "Validation Message", MessageBoxButtons.OK);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAge.Text) || Convert.ToInt32(txtAge.Text) < 14)
            {
                MessageBox.Show("Please Select Date of Birth", "Validation Message", MessageBoxButtons.OK);
                dtpDateOfBirth.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                MessageBox.Show("Please Enter Mobile Number", "Validation Message", MessageBoxButtons.OK);
                txtMobileNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please Enter Email", "Validation Message", MessageBoxButtons.OK);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbCurrentLocation.Text))
            {
                MessageBox.Show("Please Select or Enter Current Location", "Validation Message", MessageBoxButtons.OK);
                cmbCurrentLocation.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbPreferedLocation.Text))
            {
                MessageBox.Show("Please Select or Enter Prefered Location", "Validation Message", MessageBoxButtons.OK);
                cmbPreferedLocation.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbQualification.Text))
            {
                MessageBox.Show("Please Select or Enter Qualification", "Validation Message", MessageBoxButtons.OK);
                cmbQualification.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbJobTitle.Text))
            {
                MessageBox.Show("Please Enter Job Title", "Validation Message", MessageBoxButtons.OK);
                cmbJobTitle.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtExperience.Text))
            {
                MessageBox.Show("Please Enter Experience", "Validation Message", MessageBoxButtons.OK);
                txtExperience.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbCurrentIndustry.Text))
            {
                MessageBox.Show("Please select or Enter Current Industry", "Validation Message", MessageBoxButtons.OK);
                cmbCurrentIndustry.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbPreferredIndustry.Text))
            {
                MessageBox.Show("Please select or Enter Preferred Industry", "Validation Message", MessageBoxButtons.OK);
                cmbPreferredIndustry.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbNoticePeriod.Text))
            {
                MessageBox.Show("Please select or Enter Notice", "Validation Message", MessageBoxButtons.OK);
                cmbNoticePeriod.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCurrentCTC.Text))
            {
                MessageBox.Show("Please Enter Current CTC", "Validation Message", MessageBoxButtons.OK);
                txtCurrentCTC.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtExpectedCTC.Text))
            {
                MessageBox.Show("Please Enter Expected CTC", "Validation Message", MessageBoxButtons.OK);
                txtExpectedCTC.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAttachCV.Text))
            {
                MessageBox.Show("Please select CV", "Validation Message", MessageBoxButtons.OK);
                linkLabel1.Focus();
                return false;
            }

            return true;
        }

        private void SaveAttachCV(CandidateDetail candidate)
        {
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
            var frm = new AddQualification();
            frm.ShowDialog();

            lbAddQualification.Items.Add(frm.Qualification);
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

    }
}
