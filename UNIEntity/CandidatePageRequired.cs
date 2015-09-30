using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIEntity
{
    public class CandidatePageRequired
    {
        public List<string> Links { get; set; }

        public List<string> CurrentLocations { get; set; }

        public List<string> PreferedLocations { get; set; }

        public List<string> Qualifications { get; set; }

        public List<string> AdditionalQualifications { get; set; }

        public List<string> JobTitles { get; set; }

        public List<string> CurrentIndustries { get; set; }

        public List<string> PreferredIndustries { get; set; }

        public List<string> NoticePeriods { get; set; }
    }
}
