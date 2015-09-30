using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIEntity
{
    public class UserDetail : Entity
    {
        public string MobileNo { get; set; }

        public string EmailId { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string Modules { get; set; }
    }
}
