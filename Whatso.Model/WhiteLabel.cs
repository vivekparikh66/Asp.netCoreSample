using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Whatso.Model
{
    public class WhiteLabel
    {
        public int AccountId { get; set; }
        public string CompanyName { get; set; }
        public string WebsiteURL { get; set; }
        public string CompanyLOGOFile { get; set; }
        public string CompanyLOGO { get; set; }
        public string CompanyICON { get; set; }
        public string CompanyICONFile { get; set; }
        public string EXEName { get; set; }
        public int IsWhiteLabelGenerated { get; set; }
        public string DownloadMessage { get; set; }
        public string DownloadMessageTitle { get; set; }
        public string WhitelabelDownloadURL { get; set; }
    }
}
