using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;
using Whatso.Repository.Interface;
using Whatso.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Whatso.Common;
using System.IO;
using System.Data;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Whatso.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;            
        }
      
        public async Task<MyorderDetailsModel> GetMyOrders(int customerId)
        {
            MyorderDetailsModel model = new MyorderDetailsModel();
            model.ListOfOrder = _orderRepository.GetMyOrders(customerId);
            GetOrderFurtherDetails(model);
            return model;
        }
      
        public void GetOrderFurtherDetails(MyorderDetailsModel objMyOrderDetailsModel)
        {
            CommonHelper _common = new CommonHelper();
            objMyOrderDetailsModel.isWhitelabeVisible = false;
            objMyOrderDetailsModel.IsNoteVisible = false;
            foreach (var order in objMyOrderDetailsModel.ListOfOrder)
            {
                if (order.DownloadText == "WhitelabelNew")
                {
                    objMyOrderDetailsModel.isWhitelabeVisible = true;
                    objMyOrderDetailsModel.IsNoteVisible = true;

                    objMyOrderDetailsModel.Note = "<b>Note:</b> You can  <a target='_blank' href='https://www.whatso.net/blog/how-to-generate-whitelabel/'>check instructions</a> on how to create WhiteLabel here.";

                    objMyOrderDetailsModel.WhitelabelNote = "<b>WHITE-LABEL</b><br>We have received your order. Please Fillup below whitelabel form to generate your version of software.<br><br><b>License Management</b><br>Click below button to generate license for your customer's. You can setup custom duration for each license.<br><br><b> WHITE - LABEL WITH WEBSITE</b><br> We have received your order. Please Fillup below whitelabel form to generate your version of software.Also email us on hi @whatso.net your domain name for us to setup your website.<br>";

                }
                if (order.DownloadText == "Whitelabel")
                {
                    objMyOrderDetailsModel.IsNoteVisible = true;
                    objMyOrderDetailsModel.isWhitelabeVisible = true;
                    objMyOrderDetailsModel.Note = "<b>Note:</b> You can  <a target='_blank' href='https://www.whatso.net/blog/how-to-generate-whitelabel/'>check instructions</a> on how to create WhiteLabel here.";

                    objMyOrderDetailsModel.WhitelabelNote = "<b>Note:</b> There is a Data file in the download folder. You can edit that file in notepad and adjust the settings as per your company details. This is for advanced users only.<br><br><b>WHITE-LABEL</b><br>We have received your order. Please Fillup below whitelabel form to generate your version of software.<br><br><b>License Management</b><br>Click below button to generate license for your customer's. You can setup custom duration for each license.<br>";
                }
                string orderid = _common.Encrypt(order.Id.ToString());
                string TransactionId = _common.Encrypt(order.TransactionId);
                order.Receipt = "/Manage/Invoice?OrderId=" + orderid + "&TransactionId=" + TransactionId + "";
            }
        }

        public async Task<int> CreateWhiteLabel(WhiteLabel objWhiteLabel)
        {
            int i = 0;
            try
            {
                string rgbColor = "141, 213, 73";
                
                i = await _orderRepository.InsertWhiteLabelDetail(objWhiteLabel, rgbColor);
                if (i > 0)
                {
                   // dbc.InsertLogs(LOGS.LogLevel.Information, "New Whitelabel Generated", "(Accountid :: " + objWhiteLabel.AccountId + ")");
                    objWhiteLabel.IsWhiteLabelGenerated = 1;
                    //sweetMessage("your software is Ready Please download From Below link", "your software is Ready Please download From Below link", "success", "");
                    string downloadurl = "/" + Constants.downloadFolderName + "/whitelabel/" + objWhiteLabel.CompanyName + ".zip?" + DateTime.Now.Ticks.ToString() + "";
                    objWhiteLabel.DownloadMessageTitle = "Your WhiteLabel is generated";
                    objWhiteLabel.DownloadMessage = "Please Click on download button to download the newly created software.";
                    objWhiteLabel.WhitelabelDownloadURL = downloadurl;

                    
                }
            }
            catch (Exception ex)
            {
               // return 0;
               // dbc.InsertLogs(LOGS.LogLevel.Error, "ClsManage_CreateWhiteLabel", ex.Message + "\r\n" + ex.StackTrace);
            }
            return i;
        }

        public async Task<GenerateLicenseModel> GetLicenses(int customerId)
        {
            GenerateLicenseModel model = new GenerateLicenseModel();
            model.LicenseList = _orderRepository.GetLicenses(customerId);
            return model;
        }
        public async Task<GenerateLicenseModel> SaveLicenses(GenerateLicenseModel model)
        {
            _orderRepository.SaveLicenses(model);
            model.LicenseList = _orderRepository.GetLicenses(int.Parse(model.AccountId));
            return model;
        }
        public async Task<int> DeleteLicenses(int licenseid)
        {
            return  _orderRepository.DeleteLicenses(licenseid);
            
        }
    }
}
