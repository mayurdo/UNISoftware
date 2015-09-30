using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class RecycleDeletedRecord<T> : Form where T : class ,IUniTable
    {
        private readonly UniDBDataContext _uniDb;
        private readonly string _primaryKeyName;
        private readonly Expression<Func<T, bool>> _expression;

        public RecycleDeletedRecord(Expression<Func<T, bool>> expression = null)
        {
            InitializeComponent();

            _uniDb = new UniDBDataContext();
            _primaryKeyName = "SrNo";
            _expression = expression;
        }

        private void RecycleDeletedRecord_Load(object sender, EventArgs e)
        {
            BindGridView();

            lblHeader.Width = this.Width;
            dataGridView1.Width = this.Width - 40;
            buttonRestore.Location = new Point(12, 50);
            btnDeleteForever.Location = new Point(150, 50);
            buttonReset.Location = new Point(buttonReset.Location.X, lblHeader.Height + 10);
            var column = dataGridView1.Columns["IsDeleted"];

            if (column != null)
                column.Visible = false;
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = _uniDb.GetTable<T>().Where(x => x.IsDeleted).ToList();
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[_primaryKeyName].Value));
            }

            var result = MessageBox.Show(string.Format("Do you want to Restore SrNos : {0}",
                                                       string.Join(",",
                                                                   srNos.Select(
                                                                       x => x.ToString(CultureInfo.InvariantCulture))
                                                                        .ToArray())),
                                         "Restore Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            if (HomePage.UserDetail.UserId != "admin")
            {
                MessageBox.Show(@"Only admin can restore data", @"Authentication Error", MessageBoxButtons.OK);
                return;
            }

            // ask for password
            var frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();
            if (!frm.IsLogedIn)
                return;

            // restore candidate
            var candidates = (_expression == null)
                ? _uniDb.GetTable<T>().Where(x => srNos.Contains(x.SrNo)).ToList()
                : _uniDb.GetTable<T>().Where(x => srNos.Contains(x.SrNo)).Where(_expression.Compile()).ToList();

            foreach (var candidate in candidates)
            {
                candidate.IsDeleted = false;
                candidate.ExecutiveName = HomePage.UserDetail.ExecutiveName;
            }
            _uniDb.SubmitChanges();

            _uniDb.Refresh(RefreshMode.KeepChanges);
            BindGridView();
        }

        private void btnDeleteForever_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[_primaryKeyName].Value));
            }

            var result = MessageBox.Show(string.Format("Do you Delete Forever SrNos : {0}",
                                                       string.Join(",",
                                                                   srNos.Select(
                                                                       x => x.ToString(CultureInfo.InvariantCulture))
                                                                        .ToArray())),
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
            var candidates = (_expression == null)
                ? _uniDb.GetTable<T>().Where(x => srNos.Contains(x.SrNo)).ToList()
                : _uniDb.GetTable<T>().Where(x => srNos.Contains(x.SrNo)).Where(_expression.Compile()).ToList();

            _uniDb.GetTable<T>().DeleteAllOnSubmit(candidates);
            _uniDb.SubmitChanges();

            _uniDb.Refresh(RefreshMode.KeepChanges);
            BindGridView();

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
