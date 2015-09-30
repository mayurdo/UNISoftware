using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UNIRecruitmentSoft;

namespace UNIRecruitmentSoft.CompanyModule
{
    public partial class CustomerVendorDetails : Form
    {
        private readonly ReportForm<CompanyDetail> _reportForm;

        public CustomerVendorDetails(UniEnums.VendorType custVendor)
        {
            InitializeComponent();

            _reportForm = new ReportForm<CompanyDetail>(bindingNavigator1, dataGridView1,
                                                  bindingNavigatorPositionItem.TextBox, lblReportStatus);

        }

        private IQueryable<CompanyDetail> GetFiterQuery()
        {
            return _reportForm.UniDb.CustomerDetails.OfType<CompanyDetail>()
                              .Where(x => x.CompanyName.Contains(txtCompanyName.Text)
                                          && x.ContactPerson.Contains(txtContactPerson.Text)
                                          && x.MobileNo.Contains(txtMobileNo.Text)
                                          && x.BusinessType.Contains(txtBusinessType.Text)
                                          && x.Sector.Contains(txtSector.Text)
                                          && x.Expert.Contains(txtExpert.Text));
        }

        private void CustomerVendorDetails_Load(object sender, EventArgs e)
        {
            _reportForm.BindGridViewWithFilter(GetFiterQuery());
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;

            _reportForm.AutoComplete(txtCompanyName, x => x.CompanyName);
            _reportForm.AutoComplete(txtContactPerson, x => x.ContactPerson);
            _reportForm.AutoComplete(txtMobileNo, x => x.MobileNo);
            _reportForm.AutoComplete(txtBusinessType, x => x.BusinessType);
            _reportForm.AutoComplete(txtExpert, x => x.Expert);
            _reportForm.AutoComplete(txtSector, x => x.Sector);
        }
    }
}
