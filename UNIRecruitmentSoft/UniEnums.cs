using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNIRecruitmentSoft
{
    public class UniEnums
    {
        public enum Modules
        {
            Candidate,
            Company,
            Client,
            VTC,
            UserMaster,
            SmsHistory,
            VisitingCard,
            ExportExcel
        }

        public enum Designation
        {
            Admin,
            Operater,
            Marketing
        }


        public enum VendorType
        {
            None,
            Vendor,
            Trainer,
            Consultant,
            VisitingCard
        }

        public enum CompanyType
        {
            Company,
            Customer
        }
    }
}
