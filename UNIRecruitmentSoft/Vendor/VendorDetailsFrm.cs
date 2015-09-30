using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace UNIRecruitmentSoft.Vendor
{
    public partial class VendorDetailsFrm : Form
    {
        private readonly ReportForm<VendorDetail> _reportForm;
        private UniEnums.VendorType _vendorType;

        public VendorDetailsFrm(UniEnums.VendorType vendorType)
        {
            InitializeComponent();

            _vendorType = vendorType;
            _reportForm = new ReportForm<VendorDetail>(bindingNavigator1, dataGridView1,
                                                      bindingNavigatorPositionItem.TextBox, lblReportStatus);
        }

        private IQueryable<VendorDetail> GetFiterQuery()
        {
            if (_vendorType != UniEnums.VendorType.VisitingCard)
                _vendorType = (UniEnums.VendorType)Enum.Parse(typeof(UniEnums.VendorType), cbType.Text);

            return _reportForm.UniDb.VendorDetails
                              .Where(x => x.VendorType == _vendorType.ToString()
                                          && x.CompanyName.Contains(txtCompanyName.Text)
                                          && x.ContactPerson.Contains(txtContactPerson.Text)
                                          && x.MobileNo.Contains(txtMobileNo.Text)
                                          && x.BusinessType.Contains(cmbBusinessType.Text)
                                          && x.Sector.Contains(cmbSector.Text)
                                          && x.Expert.Contains(cmbExpert.Text)
                                          && x.OfficeAddress.Contains(txtLocation.Text)
                                          && (!cbNetworking.Checked || x.Networking == cbNetworking.Checked)
                                          && !x.IsDeleted
                                          && (!cbSearchByDate.Checked
                                              || (x.ProcessedDate.Date >= dtpDateFrom.Value.Date
                                                  && x.ProcessedDate.Date <= dtpDateTo.Value.Date)));

        }

        private void VendorDetailsFrm_Load(object sender, EventArgs e)
        {
            if (_vendorType != UniEnums.VendorType.VisitingCard)
            {
                cbType.Text = _vendorType.ToString();
            }

            _reportForm.BindGridViewWithFilter(GetFiterQuery());
            lblHeader.Text = (_vendorType == UniEnums.VendorType.VisitingCard) ? "Visiting Card Report" : "VTC Report";// string.Format("{0} Report", _vendorType);
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
            dataGridView1.Columns["IsDeleted"].Visible = false;
            dataGridView1.Columns["DeletedReason"].Visible = false;

            if (_vendorType == UniEnums.VendorType.VisitingCard)
            {
                dataGridView1.Columns["VendorType"].Visible = false;
                dataGridView1.Columns["Designation"].Visible = true;

                cbType.Visible = false;
                lblType.Visible = false;
            }
            else
            {
                dataGridView1.Columns["VendorType"].Visible = true;
                dataGridView1.Columns["Designation"].Visible = false;

                cbType.Visible = true;
                lblType.Visible = true;
            }

            BindControls();
        }

        public void BindControls()
        {
            _reportForm.AutoComplete(txtCompanyName, x => x.CompanyName);
            _reportForm.AutoComplete(txtContactPerson, x => x.ContactPerson);
            _reportForm.AutoComplete(txtMobileNo, x => x.MobileNo);

            _reportForm.FillComboBox(cmbBusinessType, x => x.BusinessType);
            _reportForm.FillComboBox(cmbExpert, x => x.Expert);
            _reportForm.FillComboBox(cmbSector, x => x.Sector);
        }



        #region change Page No

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToFirstPage();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToPriviousPage();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToNextPage();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToLastPage();
        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {
            _reportForm.ChangePage();
        }
        #endregion

        #region Send Mail &  SMS

        private void toolStripButtonSendMail_Click(object sender, EventArgs e)
        {
            _reportForm.SendMail();
        }

        private void toolStripButtonSendMessage_Click(object sender, EventArgs e)
        {
            _reportForm.SendSms();
        }

        #endregion

        #region Print & Export to Excel

        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            _reportForm.ExportToExcel(_vendorType + " Details", GetExportColumnName());
        }

        private List<string> GetExportColumnName()
        {
            return new List<string>() { "SrNo","CompanyName","OfficeAddress","FactoryAddress",
                                        "ContactPerson","PhoneNo","MobileNo","Email","Website","Designation","BusinessType",
                                        "Sector","Expert","EstablishedYear","NoOfEmployees","AnnualTurnover","Source","Note","ProcessedDate"};
        }

        #endregion

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            _reportForm.BindGridViewWithFilter(GetFiterQuery());
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }



        #region Add Edit Delete Record

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (_vendorType == UniEnums.VendorType.VisitingCard)
            {
                _reportForm.AddNewRecord<AddNewVendorFrm>(new object[] { _vendorType });
                return;
            }

            _reportForm.AddNewRecord<AddNewVendorFrm>(new object[] { UniEnums.VendorType.Vendor });
            BindControls();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            var srNos = _reportForm.GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var srNo = srNos[0];

            _reportForm.EditRecord<AddNewVendorFrm>(new object[] { srNo, _vendorType });
            BindControls();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var srNos = _reportForm.GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var result = MessageBox.Show(string.Format("Do you want to delete SrNos : {0}",
                                                       string.Join(",",
                                                                   srNos.Select(
                                                                       x => x.ToString(CultureInfo.InvariantCulture))
                                                                        .ToArray())),
                                         "Delete Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            _reportForm.DeleteRecord(x => srNos.Contains(x.SrNo) && x.VendorType == _vendorType.ToString());
            BindControls();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _reportForm.ShowRecyclePage(x => x.VendorType == _vendorType.ToString());
        }

        #endregion

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ask for password
            var frmLogin = new Loginfrm(HomePage.UserDetail);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var selectedSrNos = _reportForm.GetSelectedSrNo();
            var uniDb = new UniDBDataContext();
            var selectedVendors = uniDb.VendorDetails.Where(x => selectedSrNos.Contains(x.SrNo));
            uniDb.VendorDetails.DeleteAllOnSubmit(selectedVendors);

            if (toolStripComboBox1.Text != UniEnums.CompanyType.Customer.ToString())
            {
                var query = uniDb.VendorDetails.Where(x => x.VendorType == toolStripComboBox1.Text);
                var srNo = query.Any() ? query.Max(c => c.SrNo) : 0;

                var newVendors = new List<VendorDetail>();
                foreach (var vendor in selectedVendors)
                {
                    srNo++;
                    newVendors.Add(new VendorDetail()
                    {
                        SrNo = srNo,
                        AnnualTurnover = vendor.AnnualTurnover,
                        BusinessType = vendor.BusinessType,
                        CompanyName = vendor.CompanyName,
                        ContactPerson = vendor.ContactPerson,
                        Email = vendor.Email,
                        EstablishedYear = vendor.EstablishedYear,
                        ExecutiveName = vendor.ExecutiveName,
                        Expert = vendor.Expert,
                        FactoryAddress = vendor.FactoryAddress,
                        IsDeleted = vendor.IsDeleted,
                        MobileNo = vendor.MobileNo,
                        Networking = vendor.Networking,
                        NoOfEmployees = vendor.NoOfEmployees,
                        Note = vendor.Note,
                        OfficeAddress = vendor.OfficeAddress,
                        PhoneNo = vendor.PhoneNo,
                        ProcessedDate = vendor.ProcessedDate,
                        Profile1 = vendor.Profile1,
                        Profile2 = vendor.Profile2,
                        Sector = vendor.Sector,
                        Source = vendor.Source,
                        Website = vendor.Website,
                        VendorType = toolStripComboBox1.Text,
                    });
                    uniDb.VendorDetails.InsertAllOnSubmit(newVendors);
                }
            }
            else
            {
                var srNo = uniDb.CustomerDetails.Any(c => c.CompanyType == UniEnums.CompanyType.Customer.ToString())
                            ? uniDb.CustomerDetails.Where(c => c.CompanyType == UniEnums.CompanyType.Customer.ToString())
                                                   .Max(c => c.SrNo)
                            : 0;

                var newCustomers = new List<CustomerDetail>();
                foreach (var vendor in selectedVendors)
                {
                    srNo++;
                    newCustomers.Add(new CustomerDetail()
                    {
                        SrNo = srNo,
                        CompanyType = UniEnums.CompanyType.Customer.ToString(),
                        AnnualTurnover = vendor.AnnualTurnover,
                        BusinessType = vendor.BusinessType,
                        CompanyName = vendor.CompanyName,
                        ContactPerson = vendor.ContactPerson,
                        Email = vendor.Email,
                        EstablishedYear = vendor.EstablishedYear,
                        ExecutiveName = vendor.ExecutiveName,
                        Expert = vendor.Expert,
                        FactoryAddress = vendor.FactoryAddress,
                        IsDeleted = vendor.IsDeleted,
                        MobileNo = vendor.MobileNo,
                        Networking = vendor.Networking,
                        NoOfEmployees = vendor.NoOfEmployees,
                        Note = vendor.Note,
                        OfficeAddress = vendor.OfficeAddress,
                        PhoneNo = vendor.PhoneNo,
                        ProcessedDate = vendor.ProcessedDate,
                        Profile1 = vendor.Profile1,
                        Profile2 = vendor.Profile2,
                        Sector = vendor.Sector,
                        Source = vendor.Source,
                        Website = vendor.Website
                    });
                }
                uniDb.CustomerDetails.InsertAllOnSubmit(newCustomers);
            }

            uniDb.SubmitChanges();
            _reportForm.RefreshGrid();
        }

        private void toolStripButtonShowProfile1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var profile1 = _reportForm.UniDb.VendorDetails.Single(x => x.VendorType == _vendorType.ToString() && x.SrNo == srNo).Profile1;

            if (string.IsNullOrEmpty(profile1))
            {
                MessageBox.Show("Profile 1 not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(CommanFuction.GetServerPath(profile1));
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Profile 1 not found", "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void toolStripButtonShowProfile2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var profile2 = _reportForm.UniDb.VendorDetails.Single(x => x.VendorType == _vendorType.ToString() && x.SrNo == srNo).Profile2;

            if (string.IsNullOrEmpty(profile2))
            {
                MessageBox.Show("Profile 2 not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(CommanFuction.GetServerPath(profile2));
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Profile 2 not found", "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RowSelected();
        }

        private void RowSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var profile1 = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Profile1"].Value);
            var profile2 = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Profile2"].Value);

            toolStripButtonShowProfile1.Enabled = !string.IsNullOrEmpty(profile1);
            toolStripButtonShowProfile2.Enabled = !string.IsNullOrEmpty(profile2);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCompanyName_TextChanged(sender, e);

            _reportForm.FillComboBox(cmbBusinessType, x => x.BusinessType);
            _reportForm.FillComboBox(cmbExpert, x => x.Expert);
            _reportForm.FillComboBox(cmbSector, x => x.Sector);
        }
    }
}
