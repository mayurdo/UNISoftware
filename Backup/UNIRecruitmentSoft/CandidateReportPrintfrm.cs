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
    public partial class CandidateReportPrintfrm : Form
    {
        private readonly IQueryable<CandidateDetail> _candidatesFroPrint;
        public CandidateReportPrintfrm(IQueryable<CandidateDetail> candidates)
        {
            InitializeComponent();
            _candidatesFroPrint = candidates;
        }

        private void CandidateReportPrintfrm_Load(object sender, EventArgs e)
        {
            CandidateReportPrint cry = new CandidateReportPrint();
            cry.SetDataSource(_candidatesFroPrint);
            crystalReportViewer1.ReportSource = cry;
        }
    }
}
