using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class VisitingCardFrm : Form
    {
        private ICompanyDetail _companyDetail;

        public VisitingCardFrm(ICompanyDetail companyDetail)
        {
            InitializeComponent();
            _companyDetail = companyDetail;
        }

        private void VisitingCardFrm_Load(object sender, EventArgs e)
        {
            if (_companyDetail == null)
            {
                MessageBox.Show("Please select company", "Validation Message", MessageBoxButtons.OK);
                Close();
                return;
            }

            lblCompanyName.Text = _companyDetail.CompanyName;
            lblContactPerson.Text = _companyDetail.ContactPerson;
            lblAddress.Text = _companyDetail.OfficeAddress;
            lblPhoneNo.Text = _companyDetail.PhoneNo;
            lblMobileNo.Text = _companyDetail.MobileNo;
        }
    }
}
