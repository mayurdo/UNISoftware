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
    public partial class CandidateRecycle : Form
    {
        UniDBDataContext uniDb;

        public CandidateRecycle()
        {
            InitializeComponent();
            uniDb = new UniDBDataContext();
        }

        private void CandidateRecycle_Load(object sender, EventArgs e)
        {
            BindGridView();
            lblHeader.Width = this.Width;
            dataGridView1.Width = this.Width - 40;
            buttonRestore.Location = new Point(12, buttonRestore.Location.Y);
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = uniDb.CandidateDetails.Where(x => x.IsDeleted == true).ToList();
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            var result = MessageBox.Show(string.Format("Do you want to Restore SrNos : {0}",
                                            string.Join(",", srNos.Select(x => x.ToString()).ToArray())),
                                                                        "Restore Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.ExecutiveName);
            frm.ShowDialog();
            if (!frm.IsLogedIn)
                return;

            // restore candidate
            var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            foreach (var candidate in candidates)
            {
                candidate.IsDeleted = false;
                candidate.ExecutiveName = HomePage.ExecutiveName;
            }
            uniDb.SubmitChanges();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges);
            BindGridView();
        }
    }
}
