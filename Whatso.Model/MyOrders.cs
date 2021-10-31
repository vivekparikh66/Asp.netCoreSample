using System;
using System.Collections.Generic;
using System.Text;

namespace Whatso.Model
{
    public class MyOrders
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public string TransactionId { get; set; }
        public string PurchasedDate { get; set; }
        public string Amount { get; set; }
        public DateTime LicenseEndDate { get; set; }
        public string PackageNumber { get; set; }
        public string PackagePurchased { get; set; }
        public string ProductName { get; set; }
        public int productid { get; set; }
        public string FileName { get; set; }        
        public string SoftwareLink { get; set; }
        public string DownloadText { get; set; }
        public string Receipt { get; set; }
    }
}
