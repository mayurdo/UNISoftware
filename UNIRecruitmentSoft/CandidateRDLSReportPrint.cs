using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace UNIRecruitmentSoft
{
    public partial class CandidateRDLSReportPrint : Form
    {
        private readonly IQueryable<CandidateDetail> _candidatesFroPrint;

        public CandidateRDLSReportPrint(IQueryable<CandidateDetail> candidateDetails)
        {
            InitializeComponent();
            _candidatesFroPrint = candidateDetails;
        }

        private void CandidateRDLSReportPrint_Load(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("UNIRecruitmentSoft_CandidateDetail", _candidatesFroPrint.ToList()));
            this.reportViewer1.RefreshReport();
        }
    }
}
