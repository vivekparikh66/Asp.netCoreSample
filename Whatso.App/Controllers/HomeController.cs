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
    public class HomeController : Controller
    {
        private readonly IAPIHelper _apiHelper;

        public HomeController(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
         {
            Accounts account = new Accounts();
            account.Id = 90053;
            var aPIResponseModel = await _apiHelper.Post(APIEndPoints.getCompanyDetails, account);

            JArray jarr1 = (JArray)JToken.FromObject(aPIResponseModel.Data);
            var detail = jarr1.ToObject<List<CompanyDetailsModel>>();
            var companyDetails = detail.FirstOrDefault();

            return View(companyDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CompanyDetailsModel model)
        {
            var aPIResponseModel = await _apiHelper.Post(APIEndPoints.updateCompanyDetails, model.CompanyDetail);
            JArray jarr1 = (JArray)JToken.FromObject(aPIResponseModel.Data);
            var detail = jarr1.ToObject<List<CompanyDetailsModel>>();
            var companyDetails = detail.FirstOrDefault();

            return View(companyDetails);
        }
        
    }
}
