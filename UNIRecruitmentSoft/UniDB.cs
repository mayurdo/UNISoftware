using System;

namespace UNIRecruitmentSoft
{
    partial class UniDBDataContext
    {


    }


    public partial class CandidateDetail : IUniTable, IMobile, IEmail
    {

    }

    public partial class VisitDetail : IUniTable
    {
    }

    public partial class CustomerDetail : ICompanyDetail, IUniTable, IMobile, IEmail
    {

    }

    public partial class VendorDetail : ICompanyDetail, IUniTable, IMobile, IEmail
    {

    }

    //public partial class CompanyDetail : IUniTable, IMobile, IEmail
    //{

    //}

    public partial class SMSHistory : IUniTable
    {
        
    }

    #region Interfaces

    public interface IUniTable
    {
        long SrNo { get; set; }
        string ExecutiveName { get; set; }
        DateTime? ModifiedDateTime { get; set; }
        bool IsDeleted { get; set; }
        string DeletedReason { get; set; }
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


    //public abstract class CompanyDetail : IUniTable, IMobile, IEmail
    //{
    //    public virtual long SrNo { get; set; }
    //    public virtual string CompanyName { get; set; }
    //    public virtual string OfficeAddress { get; set; }
    //    public virtual string FactoryAddress { get; set; }
    //    public virtual string ContactPerson { get; set; }
    //    public virtual string MobileNo { get; set; }
    //    public virtual string PhoneNo { get; set; }
    //    public virtual string Email { get; set; }
    //    public virtual string Website { get; set; }
    //    public virtual string BusinessType { get; set; }
    //    public virtual string Sector { get; set; }
    //    public virtual string Expert { get; set; }

    //    public virtual string ExecutiveName { get; set; }
    //    public virtual bool IsDeleted { get; set; }
    //}

    #endregion
}
