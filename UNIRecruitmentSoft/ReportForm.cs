using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public class ReportForm<T> where T : class, IUniTable
    {
        protected readonly GridPagination<T> _pagination;
        protected readonly UniDBDataContext _uniDb;
        private readonly IQueryable<T> _queryable;

        public DataGridView dataGridView1 { get; set; }
        public BindingSource bindingSource1 { get; set; }
        public BindingNavigator bindingNavigator1 { get; set; }
        public Label lblReportStatus;
        public TextBox bindingNavigatorPositionItem;

        protected string PrimaryKeyName { get; set; }

        public UniDBDataContext UniDb { get { return _uniDb; } }

        public ReportForm(BindingNavigator bindingNavigator, DataGridView dataGridView, TextBox txtPageNo, Label labelReportStatus)
        {
            _pagination = new GridPagination<T>();
            _uniDb = new UniDBDataContext();
            //_queryable = queryable;

            bindingSource1 = new BindingSource();
            bindingNavigator1 = bindingNavigator;
            dataGridView1 = dataGridView;
            bindingNavigatorPositionItem = txtPageNo;
            lblReportStatus = labelReportStatus;

            PrimaryKeyName = "SrNo";
        }


        public void AutoComplete(TextBox txtBox, Func<T, string> selectColumn)
        {
            var items = _pagination.ReportData.Select(selectColumn).Distinct().ToArray();
            var itemCollection = new AutoCompleteStringCollection();
            itemCollection.AddRange(items);
            txtBox.AutoCompleteCustomSource = itemCollection;
            txtBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void FillComboBox(ComboBox comboBox, Func<T, string> selectColumn)
        {
            var items = _pagination.ReportData.Select(selectColumn).Distinct().ToArray();

            comboBox.DataSource = items;
            comboBox.Text = string.Empty;
        }

        public void BindGridViewWithFilter(IQueryable<T> queryable)
        {
            _pagination.ReportData = queryable;

            if (bindingNavigatorPositionItem.Text != _pagination.TotalItem.ToString(CultureInfo.InvariantCulture))
            {
                bindingSource1.DataSource = Enumerable.Range(1, _pagination.TotalPage);
                bindingNavigator1.BindingSource = bindingSource1;
            }

            BindGridView();
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = _pagination.PageData;

            dataGridView1.Columns["DeletedReason"].Visible = false;
            var isDeletedColumn = dataGridView1.Columns["IsDeleted"];
            if (isDeletedColumn != null)
                isDeletedColumn.Visible = false;

            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                var column = dataGridView1.Columns[i];
                if (column.ValueType == typeof(DateTime))
                    column.DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            lblReportStatus.Text = _pagination.GetReportStatus();
        }

        internal List<long> GetSelectedSrNo()
        {
            var srNos = new List<long>();

            if (dataGridView1.SelectedRows.Count < 1)
                return srNos;

            for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[PrimaryKeyName].Value));
            }

            return srNos;
        }

        #region Page Change

        public void MoveToPriviousPage()
        {
            _pagination.GoToPriviousPage();
            BindGridView();
        }

        public void MoveToNextPage()
        {
            _pagination.GoToNextPage();
            BindGridView();
        }

        public void MoveToLastPage()
        {
            _pagination.GoToLastPage();
            BindGridView();
        }

        public void MoveToFirstPage()
        {
            _pagination.GoToFirstPage();
            BindGridView();
        }

        public void ChangePage()
        {
            int pageNo;
            if (!int.TryParse(bindingNavigatorPositionItem.Text, out pageNo))
                return;

            _pagination.GoToPage(pageNo);
            BindGridView();
        }

        #endregion

        #region Add Edit & Delete Record

        public void RefreshGrid()
        {
            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        public void AddNewRecord<TAddNewForm>(object[] objects = null) where TAddNewForm : Form
        {
            var frm = (Form)Activator.CreateInstance(typeof(TAddNewForm), objects);
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        public void EditRecord<TAddNewForm>(object[] objects = null) where TAddNewForm : Form
        {
            if (objects == null)
            {
                var srNos = GetSelectedSrNo();
                if (!srNos.Any())
                    return;

                var srNo = srNos[0];
                objects = new object[] { srNo };
            }

            // ask for password
            var frmLogin = new Loginfrm(HomePage.UserDetail);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var frm = (Form)Activator.CreateInstance(typeof(TAddNewForm), objects);// new AddNewCompany(srNo);
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        public void DeleteRecord(Func<T, bool> condition = null)
        {
            if (condition == null)
            {
                var srNos = GetSelectedSrNo();
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

                condition = (x => srNos.Contains(x.SrNo));
            }

            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;

            var deleteReasionfrm = new DeleteReasionfrm();
            deleteReasionfrm.ShowDialog();

            if (string.IsNullOrEmpty(DeleteReasionfrm.DeleteReasion))
                return;

            var records = _pagination.PageData.Where(condition).ToList();

            foreach (var record in records)
            {
                record.IsDeleted = true;
                record.ExecutiveName = HomePage.UserDetail.UserId;
                record.ModifiedDateTime = DateTime.Now;
                record.DeletedReason = DeleteReasionfrm.DeleteReasion;
            }
            _uniDb.SubmitChanges();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        #endregion

        public void ShowRecyclePage(Expression<Func<T, bool>> expression = null)
        {
            var frm = new RecycleDeletedRecord<T>(expression);
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }


        #region Send Mail & SMS

        public void SendMail()
        {
            var hasEmailId = typeof(IEmail).IsAssignableFrom(typeof(T));
            if (!hasEmailId)
                return;

            var srNos = GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var companys = _pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyEmailIds = companys.Where(x => string.IsNullOrEmpty(((IEmail)x).Email))
                                        .Select(x => x.SrNo.ToString(CultureInfo.InvariantCulture))
                                        .ToArray();
            if (emptyEmailIds.Any())
            {
                MessageBox.Show(string.Format("SrNos {0} don't have email id", string.Join(",", emptyEmailIds)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var emailIds = companys.Select(x => ((IEmail)x));
            CommanFuction.OpenOutlook(emailIds);
        }

        public void SendSms()
        {
            var hasEmailId = typeof(IMobile).IsAssignableFrom(typeof(T));
            if (!hasEmailId)
                return;

            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells[PrimaryKeyName].Value));
            }

            var reports = _pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyMobileNos = reports.Where(x => string.IsNullOrEmpty(((IMobile)x).MobileNo))
                                        .Select(x => x.SrNo.ToString(CultureInfo.InvariantCulture))
                                        .ToArray();
            if (emptyMobileNos.Any())
            {
                MessageBox.Show(string.Format("SrNos {0} don't have Mobile No", string.Join(",", emptyMobileNos)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var contactDetail = reports.Select(x => ((IMobile)x));
            var frm = new SendSMSfrm(contactDetail);
            frm.ShowDialog();
        }

        #endregion

        #region Export to Excel

        public void ExportToExcel(string reportName, List<string> columnList)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int i = 0;
            int j = 0;

            var properties = typeof(T).GetProperties().Where(x => columnList.Contains(x.Name)).ToList();

            int mid = properties.Count() / 2;
            xlWorkSheet.Cells[1, mid] = reportName;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[1, mid], xlWorkSheet.Cells[1, mid]).Font.Size = 20;

            for (j = 0; j < properties.Count(); j++)
            {
                xlWorkSheet.Cells[2, j + 1] = properties[j].Name;
            }

            var dataListArray = _pagination.ReportData.ToArray();
            for (i = 0; i < dataListArray.Count(); i++)
            {
                var data = dataListArray[i];

                for (j = 0; j < properties.Count(); j++)
                {
                    var columnName = properties[j].Name;
                    string cellValue = Convert.ToString(typeof(T).GetProperty(columnName).GetValue(data, null));
                    xlWorkSheet.Cells[i + 3, j + 1] = cellValue;
                }
            }
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, j]).Font.Bold = true;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[i + 2, j]).Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string full_name = saveFileDialog1.InitialDirectory + saveFileDialog1.FileName;
                xlWorkBook.SaveAs(full_name + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file " + full_name + ".xls");
            }
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
