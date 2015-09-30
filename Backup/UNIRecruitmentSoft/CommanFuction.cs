using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;

namespace UNIRecruitmentSoft
{
    public class Condition
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }

    public class CommanFuction
    {

        public static Expression GetLinqCondition<T>(List<Condition> conditionList)
        {
            var param = Expression.Parameter(typeof(T), "t");

            var body = Expression.Equal(Expression.Constant(1), Expression.Constant(1));
            foreach (var condition in conditionList)
            {
                body = Expression.And(body, Expression.Call(Expression.PropertyOrField(param, condition.Field), "Contains", null, Expression.Constant(condition.Value)));
            }
            //var body = Expression.Call(Expression.PropertyOrField(param, "Name"), "Contains", null, Expression.Constant("Mayur"));

            var lamda = Expression.Lambda<Func<T, bool>>(body, param);

            return lamda;
        }

        public static List<long> GetLongFromString(string value)
        {
            List<long> result = new List<long>();

            string[] arr = value.Split(',');

            foreach (string a in arr)
            {
                string[] adesh = a.Split('-');
                try
                {
                    long MinValue = Convert.ToInt64(adesh[0]);
                    long MaxValue = Convert.ToInt64(adesh[adesh.Length - 1]);
                    while (MaxValue >= MinValue)
                    {
                        result.Add(MinValue);
                        MinValue++;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return result;
        }

        public static void AcceptOnlyNumber(KeyPressEventArgs e)
        {
            int key = Convert.ToInt32(e.KeyChar);
            if (!((key >= 48 && key <= 57) || (key > 1 && key <= 26) || key == 8))
            {
                e.Handled = true;
            }
        }

        public static void AcceptOnlySearchingNumber(KeyPressEventArgs e)
        {
            int key = Convert.ToInt32(e.KeyChar);
            if (!((key >= 48 && key <= 57) || (key > 1 && key <= 26) || key == 8 || key == 44 || key == 45))
            {
                e.Handled = true;
            }
        }

        public static void OpenOutlook(CandidateDetail candidate)
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = candidate.Email;
                mailItem.Body = string.Format("Dear {0},", candidate.Name);
                mailItem.Importance = Outlook.OlImportance.olImportanceLow;
                mailItem.Display(false);
            }
            catch (Exception eX)
            {
                MessageBox.Show("cDocument: Error occurred trying to Create an Outlook Email"
                                    + Environment.NewLine + eX.Message, "Error Message", MessageBoxButtons.OK);
            }
        }

        public static void Export_To_Excel2<T>(IEnumerable<T> dataList, string ReportName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int i = 0;
            int j = 0;

            var properties = typeof(T).GetProperties();

            int mid = properties.Count() / 2;
            xlWorkSheet.Cells[1, mid] = ReportName;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[1, mid], xlWorkSheet.Cells[1, mid]).Font.Size = 20;

            for (j = 0; j < properties.Count(); j++)
            {
                xlWorkSheet.Cells[2, j + 1] = properties[j].Name;
            }

            for (i = 0; i < dataList.Count(); i++)
            {
                var data = dataList.ToArray()[i];

                for (j = 0; j < properties.Count(); j++)
                {
                    var columnName = properties[j].Name;
                    string CellValue = Convert.ToString(typeof(T).GetProperty(columnName).GetValue(data, null));
                    xlWorkSheet.Cells[i + 1, j + 1] = CellValue;
                }
            }
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, j]).Font.Bold = true;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[i + 1, j]).Borders.Weight = Excel.XlBorderWeight.xlThin;


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string full_name = saveFileDialog1.InitialDirectory + saveFileDialog1.FileName;
                xlWorkBook.SaveAs(full_name + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file " + full_name + ".xls");
            }
        }

        public static void Export_To_Excel(ref DataGridView dataGridView1, string ReportName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int i = 0;
            int j = 0;

            int mid = dataGridView1.ColumnCount / 2;
            xlWorkSheet.Cells[1, mid] = ReportName;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[1, mid], xlWorkSheet.Cells[1, mid]).Font.Size = 20;

            for (i = 0; i < dataGridView1.Rows.Count + 1; i++)
            {
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (i == 0)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Columns[j].HeaderText;
                    }
                    else
                    {
                        DataGridViewCell cell = dataGridView1.Rows[i - 1].Cells[j];
                        string CellValue = "";
                        CellValue = (cell.Value == null) ? "" : cell.Value.ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = CellValue;
                    }
                }

            }
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, j]).Font.Bold = true;
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[i + 1, j]).Borders.Weight = Excel.XlBorderWeight.xlThin;


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string full_name = saveFileDialog1.InitialDirectory + saveFileDialog1.FileName;
                xlWorkBook.SaveAs(full_name + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
    }
}
