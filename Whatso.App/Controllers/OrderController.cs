using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Whatso.App.Helper;
using Whatso.App.Models;
using Whatso.Common;
using Whatso.Model;

namespace Whatso.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAPIHelper _apiHelper;

        public OrderController(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }
               
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            Accounts account = new Accounts();
            account.Id = 95370;
            var aPIResponseModel = await _apiHelper.Post(APIEndPoints.getMyOrders, account);
            JArray jarr1 = (JArray)JToken.FromObject(aPIResponseModel.Data);
            var orders = jarr1.ToObject<List<MyorderDetailsModel>>().FirstOrDefault();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhiteLabel([FromForm] IFormFile CompanyLOGOFile, [FromForm] IFormFile CompanyICONFile, WhiteLabel objWhitelabel)
        {
            try
            {
                CommonHelper common = new CommonHelper();

                string whiteLabelPath = common.DisplaySpecialCharacters(objWhitelabel.CompanyName);
                string exeName = common.DisplaySpecialCharacters(objWhitelabel.EXEName);
                if (!String.IsNullOrEmpty(whiteLabelPath) && !String.IsNullOrEmpty(exeName))
                {
                    common.SetDisplayToast(this, "Please do not use SpecialCharacters", "warning", "");
                    return RedirectToAction(nameof(Orders));
                }
                else
                {
                    _apiHelper.CreateWhiteLabel(APIEndPoints.createwhitelabel, CompanyLOGOFile, CompanyICONFile, objWhitelabel);
                    var aPIResponseModel = await _apiHelper.Post(APIEndPoints.createwhitelabel, objWhitelabel);
                    
                    if (objWhitelabel.IsWhiteLabelGenerated == 1)
                    {
                        TempData["WhitelabelDownloadURL"] = objWhitelabel.WhitelabelDownloadURL;
                        TempData["DownloadMessageTitle"] = objWhitelabel.DownloadMessageTitle;
                        TempData["DownloadMessage"] = objWhitelabel.DownloadMessage;
                    }
                    else
                    {
                        common.SetDisplayToast(this, "Something Went Wrong", "error", "");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction(nameof(Orders));
        }
        public async Task<IActionResult> GenerateLicense()
        {
            CommonHelper common = new CommonHelper();

            GenerateLicenseModel objLicense = new GenerateLicenseModel();
            try
            {
                common.GetDisplayToast(this);
                objLicense.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd");
                objLicense.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd");
                var aPIResponseModel = await _apiHelper.Post(APIEndPoints.generatelicense, 95370);
                JArray jarr1 = (JArray)JToken.FromObject(aPIResponseModel.Data);
                objLicense = jarr1.ToObject<List<GenerateLicenseModel>>().FirstOrDefault();
                

            }
            catch (Exception ex)
            {
            }
            return View(objLicense);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateLicense(GenerateLicenseModel objLicenseModel)
        {
            CommonHelper common = new CommonHelper();
            GenerateLicenseModel objLicense = new GenerateLicenseModel();
            try
            {
               
                objLicenseModel.AccountId = "95370";
                var aPIResponseModel = await _apiHelper.Post(APIEndPoints.savelicense, objLicenseModel);
                JArray jarr1 = (JArray)JToken.FromObject(aPIResponseModel.Data);
                objLicense = jarr1.ToObject<List<GenerateLicenseModel>>().FirstOrDefault();
                // int AddLicense = objClsManage.SaveWhitelabellicense(objLicenseModel);
                if (objLicense != null)
                {
                    common.SetDisplayToast(this, "License Details Save Succesfully", "success", "");
                }
                else
                {
                    common.SetDisplayToast(this, "Something went wrong", "warning", "");
                    objLicense.StartDate = objLicenseModel.StartDate;
                    objLicense.EndDate = objLicenseModel.EndDate;
                    objLicense.Email = objLicenseModel.Email;

                }
                common.GetDisplayToast(this);
                objLicense.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd");
                objLicense.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd");
                return View(objLicense);
                // objLicense.LicenseList = objClsManage.GetLicenseList();
            }
            catch (Exception ex)
            {
            }
            return View(objLicense);
        }
        public async Task<IActionResult> DeleteLicense(string LicenseId)
        {
            CommonHelper common = new CommonHelper();
            try
            {
                if (!string.IsNullOrEmpty(LicenseId))
                {
                    var aPIResponseModel = await _apiHelper.Post(APIEndPoints.deeletelicense, LicenseId);
                    common.SetDisplayToast(this, "License Delete Succesfully", "success", "");
                   
                }
                else
                {
                    common.SetDisplayToast(this, "Something went wrong", "warning", "");
                }
                return RedirectToAction(nameof(GenerateLicense));
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}
