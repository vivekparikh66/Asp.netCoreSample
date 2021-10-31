using System;
using System.Collections.Generic;
using System.Text;

namespace Whatso.Model
{
    public class GenerateLicenseModel
    {
        public string LicenseId { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<GenerateLicenseModel> LicenseList { get; set; }
    }

}
