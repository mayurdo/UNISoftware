using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GRNSoft.Models
{
    public class GRNDetail
    {
        public int GRNNo { get; set; }

        public DateTime GRNDate { get; set; }

        public string Supplier { get; set; }

        public string ChallanNo { get; set; }

        public DateTime ChallanDate { get; set; }

        public string PONo { get; set; }

        public DateTime PODate { get; set; }

        public string VehicalNo { get; set; }

        public string OctroiReceiptNo { get; set; }

        public string ReasonForRejection { get; set; }

        public virtual ICollection<GRNItemDetail> GRNItemDetails { get; set; }
    }

    public class GRNItemDetail
    {
        public int ItemCode { get; set; }

        public string Description { get; set; }

        public string Challan { get; set; }

        public string ChallanUnit { get; set; }

        public string Actual { get; set; }

        public string ActualUnit { get; set; }

        public string Accepted { get; set; }

        public string Remark { get; set; }

        public virtual GRNDetail GRNDetail { get; set; }

    }

    public class GRNDBContext : DbContext
    {
        public DbSet<GRNDetail> GRNDetails { get; set; }

        public DbSet<GRNItemDetail> GRNItemDetails { get; set; }
    }
}
