using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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

    public enum KeyValueAliase
    {
        BusinessType,
        Sector
    }

    public class CommanFuction
    {
        public static void AutoComplete(TextBox txtBox, string[] items)
        {
            var itemCollection = new AutoCompleteStringCollection();
            itemCollection.AddRange(items);
            txtBox.AutoCompleteCustomSource = itemCollection;
            txtBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public static void FillComboBox(ComboBox comboBox, string[] items)
        {
            comboBox.DataSource = items;
            comboBox.Text = string.Empty;
        }

        public static string GetServerPath1(string filePath)
        {
            var serverPath = ConfigurationSettings.AppSettings["ServerPath"];
            var driveName = Path.GetPathRoot(filePath);
            if (!string.IsNullOrEmpty(driveName))
                return filePath.Replace(driveName, serverPath);

            return filePath;

        }

        public static string GetServerPath(string filePath)
        {
            var indexText = ConfigurationSettings.AppSettings["IndexText"];
            var serverPath = ConfigurationSettings.AppSettings["CvUploadLocation"];

            var indexInDbPath = filePath.IndexOf(indexText);
            var indexInServerPath = serverPath.IndexOf(indexText);

            var folderPath = filePath.Substring(filePath.IndexOf(indexText));
            var serverLink = serverPath.Substring(0, indexInServerPath);

            var finalPath = serverLink + folderPath;

            return finalPath;
        }

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

        public static string SaveFile(string sourceFileName, string targetPath)
        {
            if (string.IsNullOrEmpty(sourceFileName))
                return string.Empty;

            var targetDir = new DirectoryInfo(targetPath);
            if (!targetDir.Exists)
            {
                targetDir.Create();
            }

            var selFile = new FileInfo(sourceFileName);

            if (!selFile.Exists)
            {
                throw new FileNotFoundException(string.Format("Selected File {0} is not Found In location {0}", selFile.Name, selFile.DirectoryName));
            }

            var fileName = System.IO.Path.GetFileName(selFile.FullName);
            var destFile = System.IO.Path.Combine(targetDir.FullName, fileName);

            if (selFile.FullName == destFile)
            {
                return destFile;
            }

            System.IO.File.Copy(selFile.FullName, destFile, true);
            return destFile;
        }


        public static void OpenOutlook(IEnumerable<IEmail> emailIds)
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = string.Join(";", emailIds.Select(x => x.Email).ToArray());
                //mailItem.Body = "Dear ";
                mailItem.Importance = Outlook.OlImportance.olImportanceLow;
                mailItem.Display(false);
            }
            catch (Exception eX)
            {
                MessageBox.Show("cDocument: Error occurred trying to Create an Outlook Email"
                                    + Environment.NewLine + eX.Message, "Error Message", MessageBoxButtons.OK);
            }
        }

        public static List<T> Import_From_Excel<T>(List<string> columnList, long maxSrNo, string excelPath)
            where T : IUniTable, new()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Open(excelPath);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
            var lastRow = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

            var dataList = new List<T>();
            for (int row = 2; row <= lastRow; row++)
            {
                var obj = new T();
                //var myValues = (Array)xlWorkSheet.Range[string.Format("{0}{1}", startRange, row), string.Format("{0}{1}", endRange, row)].Cells.Value;

                var myValues = (Array)xlWorkSheet.Range[xlWorkSheet.Cells[row, 1], xlWorkSheet.Cells[row, columnList.Count]].Cells.Value;

                var propertyInfos = typeof(T).GetProperties();
                for (int col = 0; col < columnList.Count; col++)
                {
                    var propertyInfo = propertyInfos.SingleOrDefault(x => x.Name == columnList[col]);
                    if (propertyInfo == null)
                        continue;

                    object value = null;
                    object cellValue = myValues.GetValue(1, col + 1);
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        value = (cellValue == "Y");
                    }
                    else
                    {
                        try
                        {
                            Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                            if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                            {
                                value = SetDefaultValue(propertyInfo.PropertyType);
                            }
                            else if (type == typeof (DateTime))
                            {
                                try
                                {
                                    var dateString = (cellValue is DateTime)
                                        ? ((DateTime) cellValue).ToString("M/d/yyyy")
                                        : cellValue.ToString().Trim();
                                    value = DateTime.ParseExact(dateString,
                                        new[] {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy"},
                                        CultureInfo.InvariantCulture, DateTimeStyles.None);
                                }
                                catch (Exception ex)
                                {
                                    value = DateTime.Now;
                                }
                            }
                            else
                            {
                                value = Convert.ChangeType(cellValue, type);
                            }
                        }
                        catch (Exception ex)
                        {
                            var errorMessage = string.Format("Sr No '{0}' has invalid value '{1}' in column '{2}', " +
                                                             "Please correct it in excel and try again",
                                                                            obj.SrNo, cellValue, columnList[col]);
                            MessageBox.Show(errorMessage, "Error Message", MessageBoxButtons.OK);

                            xlApp.Quit();
                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlApp);
                            return new List<T>();
                        }
                    }
                    propertyInfo.SetValue(obj, value, null);
                }

                obj.ExecutiveName = HomePage.UserDetail.ExecutiveName;
                obj.IsDeleted = false;

                if (obj.SrNo == 0)
                    continue;

                obj.SrNo = maxSrNo + 1;
                maxSrNo++;

                dataList.Add(obj);
            }


            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            return dataList;
        }

        private static object SetDefaultValue(Type type)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return null;
            }

            if (type == typeof(string))
                return string.Empty;

            if (type == typeof (DateTime))
                return DateTime.Now;

            return 0;
        }

        public static void Excel_Format<T>(List<string> ColumnList)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Name = typeof(T).Name;
            int j = 0;
            for (j = 0; j < ColumnList.Count(); j++)
            {
                xlWorkSheet.Cells[1, j + 1] = ColumnList[j];
            }

            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[2, j]].Font.Bold = true;

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

            var dataListArray = dataList.ToArray();
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
            xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[i + 2, j]).Borders.Weight = Excel.XlBorderWeight.xlThin;


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

        public static void CreateDBIfNotExist()
        {
            var uniDb = new UniDBDataContext();

            if (uniDb.DatabaseExists())
                return;

            // create db
            uniDb.CreateDatabase();

            // add admin user
            UserDetail user = new UserDetail()
            {
                Id = 1,
                ExecutiveName = "admin",
                UserId = "admin",
                Password = "admin",
                Modules = " Candidate,Company,Client,VTC,UserMaster,SmsHistory,VisitingCard,ExportExcel"
            };
            uniDb.UserDetails.InsertOnSubmit(user);
            uniDb.SubmitChanges();
        }
    }
}
