using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace UNIRecruitmentSoft
{
    public partial class ImportFromExcel : Form
    {
        private UniDBDataContext db = new UniDBDataContext();

        public ImportFromExcel()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var excelType = cmbExcelName.Text;
            switch (excelType)
            {
                case "Candidate":
                    CommanFuction.Excel_Format<CandidateDetail>(GetCandidateColumnList());
                    return;
                case "Company":
                    CommanFuction.Excel_Format<CustomerDetail>(GetCompanyColumnList());
                    break;
                default:
                    throw new ExecutionNotFoundException(string.Format("Import excel for {0} not found", excelType));
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbExcelName.Text))
            {
                MessageBox.Show("Please Select Excel Name Drop Down", "Validation", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(txtExcelPath.Text);
            if (!fileInfo.Exists)
            {
                MessageBox.Show("File not fount", "File not fount", MessageBoxButtons.OK);
                return;
            }

            var excelType = cmbExcelName.Text;
            bool dataSaved = false;
            long maxSrNo = 0;
            switch (excelType)
            {
                case "Candidate":
                    maxSrNo = db.CandidateDetails.Any() ? db.CandidateDetails.Select(c => c.SrNo).Max() : 0;
                    var candidateList = CommanFuction.Import_From_Excel<CandidateDetail>(GetCandidateColumnList(), maxSrNo, fileInfo.FullName);
                    db.CandidateDetails.InsertAllOnSubmit(candidateList);
                    //foreach (var candidateDetail in candidateList)
                    //{
                    //    db.CandidateDetails.InsertOnSubmit(candidateDetail);
                    //    db.SubmitChanges();
                    //}

                    dataSaved = (candidateList.Count > 0);
                    break;
                case "Company":

                    maxSrNo = db.CustomerDetails.Any() ?
                         db.CustomerDetails
                         .Where(c => c.CompanyType == UniEnums.CompanyType.Company.ToString()).Max(c => c.SrNo)
                         : 0;
                    var companyList = CommanFuction.Import_From_Excel<CustomerDetail>(GetCompanyColumnList(), maxSrNo, fileInfo.FullName);
                    companyList.ForEach(x => x.CompanyType = UniEnums.CompanyType.Company.ToString());

                    db.CustomerDetails.InsertAllOnSubmit(companyList);
                    dataSaved = (companyList.Count > 0);
                    break;
                default:
                    throw new ExecutionNotFoundException(string.Format("Import excel for {0} not found", excelType));
            }

            db.SubmitChanges();

            var message = dataSaved ? "Excel data saved" : "Excel data not saved";
            MessageBox.Show(message, "Message", MessageBoxButtons.OK);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtExcelPath.Text = openFileDialog1.FileName;
            }
        }



        #region Column Lists

        private static List<string> GetCandidateColumnList()
        {
            var columnList = new List<string>
            {
                "SrNo",
                "Link",
                "ProcessedDate",
                "Name",
                "Gender",
                "DateOfBirth",
                "Age",
                "MarritalStatus",
                "CurrentLocation",
                "PreferedLocation",
                "MobileNo",
                "LandLineNo",
                "Email",
                "Qualification",
                "AdditionalQualification",
                "Experience",
                "ExpSlap",
                "JobTitle",
                "CurrentIndustry",
                "PreferredIndustry",
                "NoticePeriod",
                "CurrentCTC",
                "ExpectedCTC",
                "NoOfCalls",
                "Remarks",
                "Registered",
                "Placed"
            };
            return columnList;
        }

        private static List<string> GetCompanyColumnList()
        {
            var columnList = new List<string>
            {
                "SrNo",
                "CompanyName",
                "OfficeAddress",
                "FactoryAddress",
                "ContactPerson",
                "PhoneNo",
                "MobileNo",
                "Email",
                "Website",
                "BusinessType",
                "Sector",
                "Expert",
                "EstablishedYear",
                "NoOfEmployees",
                "AnnualTurnover",
                "Source",
                "Note"
            };

            return columnList;
        }


        #endregion

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
