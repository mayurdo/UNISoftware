using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIFreelancerDataBaseEntry.Shared
{
    #region Interfaces

    public interface IUniTable
    {
        long SrNo { get; set; }
        string ExecutiveName { get; set; }
        bool IsDeleted { get; set; }
    }

    public interface IMobile
    {
        string MobileNo { get; set; }
    }

    public interface IEmail
    {
        string Email { get; set; }
    }

    public interface ICompanyDetail
    {
        string CompanyName { get; set; }
        string ContactPerson { get; set; }
        string OfficeAddress { get; set; }
        string PhoneNo { get; set; }
        string MobileNo { get; set; }
    }

    #endregion
}
