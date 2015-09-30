using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace UNIEntity
{
    public class CandidateDetail : Entity
    {
        public string Link { get; set; }

        public DateTime ProcessedDate { get; set; }

        public string Name { get; set; }

        public long CandidateCode { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string CurrentLocation { get; set; }

        public string PreferedLocation { get; set; }

        public string MobileNo { get; set; }

        public string LandLineNo { get; set; }

        public string Email { get; set; }

        public string Qualification { get; set; }

        public string AdditionalQualification { get; set; }

        public int Experience { get; set; }

        public string ExpSlap { get; set; }

        public string JobTitle { get; set; }

        public string CurrentIndustry { get; set; }

        public string PreferredIndustry { get; set; }

        public string NoticePeriod { get; set; }

        public string CurrentCTC { get; set; }

        public string ExpectedCTC { get; set; }

        public string Remarks { get; set; }

        public string CVPath { get; set; }

        public string MarritalStatus { get; set; }
    }
}
