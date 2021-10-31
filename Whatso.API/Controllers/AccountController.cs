using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.API.APIModels;
using Whatso.API.JSON.Serializers;
using Whatso.Model;
using Whatso.Services.Interface;

namespace Whatso.API.Controllers
{
    [Route("api/account")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IJsonFieldsSerializer jsonFieldsSerializer, IAccountService accountService) : base(jsonFieldsSerializer)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("company-detail")]
        public async Task<IActionResult> GetAccountDetails([FromBody] Accounts accounts)
                {
            try
            {
                CompanyDetailsModel model = new CompanyDetailsModel();
                model.Countries = await _accountService.GetCountries();
                model.Cities = await _accountService.GetCities();

                var currencies = new List<Currency>();
                currencies.Add(new Currency() { CurrencyName = "INR", Id = 1 });
                currencies.Add(new Currency() { CurrencyName = "USD", Id = 2 });
                model.Currencies = currencies;

                model.CompanyDetail = await _accountService.GetAccountDetaiById(accounts.Id);
                if (model == null)
                {
                    return Error(null, "No company found for loggedin user");
                }
                return Success(model, "Company detail fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching company detail!");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("update-company-details")]
        public async Task<IActionResult> UpdateCompanyDetails([FromBody] Accounts accounts)
        {
            try
            {
                int result = await _accountService.UpdateAccountDetails(accounts);
                if (result <= 0)
                {
                    return Error(null, "Error while updating company details");
                }
                CompanyDetailsModel model = new CompanyDetailsModel();
                model.Countries = await _accountService.GetCountries();
                model.Cities = await _accountService.GetCities();

                var currencies = new List<Currency>();
                currencies.Add(new Currency() { CurrencyName = "INR", Id = 1 });
                currencies.Add(new Currency() { CurrencyName = "USD", Id = 2 });
                model.Currencies = currencies;

                model.CompanyDetail = await _accountService.GetAccountDetaiById(accounts.Id);
                return Success(model, "Company detail updated succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while updating company detail!");
            }
        }      
            
    }
}
