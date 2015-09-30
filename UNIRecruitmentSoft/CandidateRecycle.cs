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
            buttonRestore.Location = new Point(12, 50);
            btnDeleteForever.Location = new Point(150, 50);
            buttonReset.Location = new Point(buttonReset.Location.X, lblHeader.Height + 10);
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = uniDb.CandidateDetails.Where(x => x.IsDeleted == true).ToList();
            dataGridView1.Columns["IsDeleted"].Visible = false;
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


            if (HomePage.UserDetail.UserId != "admin")
            {
                MessageBox.Show(@"Only admin can restore data", @"Authentication Error", MessageBoxButtons.OK);
                return;
            }

            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();
            if (!frm.IsLogedIn)
                return;

            // restore candidate
            var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            foreach (var candidate in candidates)
            {
                candidate.IsDeleted = false;
                candidate.ExecutiveName = HomePage.UserDetail.UserId;
                candidate.DeletedReason = string.Empty;
            }
            uniDb.SubmitChanges();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges);
            BindGridView();
        }

        private void btnDeleteForever_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            var result = MessageBox.Show(string.Format("Do you Delete Forever SrNos : {0}",
                                            string.Join(",", srNos.Select(x => x.ToString()).ToArray())),
                                                                        "Delete Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            if (HomePage.UserDetail.UserId != "admin")
            {
                MessageBox.Show(@"Only admin can delete data", @"Authentication Error", MessageBoxButtons.OK);
                return;
            }

            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();
            if (!frm.IsLogedIn)
                return;

            // delete forever candidate
            var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            uniDb.CandidateDetails.DeleteAllOnSubmit(candidates);
            uniDb.SubmitChanges();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges);
            BindGridView();

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
