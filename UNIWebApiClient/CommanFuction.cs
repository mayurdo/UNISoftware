using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UNIEntity;

namespace UNIWebApiClient
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

    public class MethodHelper
    {
        public static ResultResponse<T> GetServiceResponse<T>(string servicePath, IRequest request) where T : Entity
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:2818/")
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(request);
            HttpContent requestContent = new StringContent(json);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = client.PostAsync(servicePath, requestContent).Result;

            var response = new ResultResponse<T>();
            if (httpResponse.IsSuccessStatusCode)
            {
                response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result).ToObject<ResultResponse<T>>();

                if (!response.IsSuccess)
                {
                    MessageBox.Show(response.Exception.Message, @"Error Message");
                }
            }
            else
            {
                MessageBox.Show(httpResponse.StatusCode.ToString(), @"Error Message");
            }

            return response;
        }

        public static T GetServicePageDataResponse<T>(string servicePath)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:2818/")
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var httpResponse = client.GetAsync(servicePath).Result;

            var response = new ResultResponse<T>();
            if (httpResponse.IsSuccessStatusCode)
            {
                response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result).ToObject<ResultResponse<T>>();

                if (!response.IsSuccess)
                {
                    MessageBox.Show(response.Exception.Message, @"Error Message");
                }
            }
            else
            {
                MessageBox.Show(httpResponse.StatusCode.ToString(), @"Error Message");
            }
          
            return response.Object;
        }

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
    }
}
