using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace UNIFreelancerDataBaseEntry
{
    public interface IGridPagination<T>
    {
        IEnumerable<T> PageData { get; }

        int PageNo { get; }

        int ItemParPage { get; set; }

        int TotalItem { get; }
    }

    public class GridPagination<T> : IGridPagination<T>
    {

        public GridPagination()
        {
            _pageNo = 1;
            ItemParPage = 20;
        }

        int _pageNo;
        public int PageNo { get { return _pageNo; } }

        public int ItemParPage { get; set; }

        public int ItemInCurrentPage { get { return PageData.Count(); } }

        public int TotalItem { get; set; }

        public int TotalPage
        {
            get
            {
                var extraPage = (TotalItem % ItemParPage) > 0 ? 1 : 0;
                return ((int)(TotalItem / ItemParPage) + extraPage);
            }
        }
        

        public IEnumerable<T> PageData { get; set; }

        public string ReportFor { get; set; }

        public void GoToNextPage()
        {
            if (_pageNo < TotalPage)
                _pageNo++;
        }

        public void GoToPriviousPage()
        {
            if (_pageNo > 1)
                _pageNo--;
        }

        public void GoToPage(int pageNo)
        {
            if (pageNo > 0 && pageNo <= TotalPage)
                _pageNo = pageNo;
        }

        public void GoToFirstPage()
        {
            _pageNo = 1;
        }

        public void GoToLastPage()
        {
            _pageNo = TotalPage;
        }

        public string GetReportStatus()
        {
            return string.Format("Total {0} : {1}", ReportFor, TotalItem);
        }
    }
}
