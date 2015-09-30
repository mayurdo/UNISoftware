using System;
using System.Collections.Generic;

namespace UNIEntity
{
    public interface IRequest
    {
    }

    public interface IReportRequest : IRequest
    {
        int PageNo { get; set; }

        int PageSize { get; set; }
    }

    public class ResultResponse<T>
    {
        public List<T> PageData { get; set; }

        public T Object { get; set; }

        public long TotalItem { get; set; }

        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }
    }

    public class CandidateDetailSearchRequest : IReportRequest
    {
        public CandidateDetailSearchRequest()
        {
            PageNo = 1;
            PageSize = 20;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Qualification { get; set; }

        public string Gender { get; set; }

        public string MarritalStatus { get; set; }

        public bool SearchByProcessedDate { get; set; }

        public DateTime FromProcessedDate { get; set; }

        public DateTime ToProcessedDate { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}