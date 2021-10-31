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
    [Route("api/order")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IJsonFieldsSerializer jsonFieldsSerializer, IOrderService orderService) : base(jsonFieldsSerializer)
        {
            _orderService = orderService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("myorders")]
        public async Task<IActionResult> MyOrders([FromBody] Accounts accounts)
         {
            try
            {
                var orders = await _orderService.GetMyOrders(accounts.Id);
                if (orders == null)
                {
                    return Error(null, "No orders found");
                }
                return Success(orders, "orders fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching orders!");
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("createwhitelabel")]
        public async Task<IActionResult> CreateWhiteLabel( WhiteLabel model)
        {
            try
            {

                var orders = await _orderService.CreateWhiteLabel(model);
                if (orders == null)
                {
                    return Error(null, "No orders found");
                }
                return Success(orders, "orders fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching orders!");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("generatelicense")]
        public async Task<IActionResult> GenerateLicense([FromBody] int accountid)
        {
            try
            {
                var licenses = await _orderService.GetLicenses(accountid);
                if (licenses == null)
                {
                    return Error(null, "No License list found");
                }
                return Success(licenses, "License list fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching license list!");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("savelicense")]
        public async Task<IActionResult> SaveLicense(GenerateLicenseModel model)
        {
            try
            {
                var licenses = await _orderService.SaveLicenses(model);
                if (licenses == null)
                {
                    return Error(null, "No License list found");
                }
                return Success(licenses, "License list fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching license list!");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("deletelicense")]
        public async Task<IActionResult> DeleteLicense(int license)
        {
            try
            {
                int result = await _orderService.DeleteLicenses(license);
                if (result <= 0)
                {
                    return Error(null, "Error while updating license details");
                }
                    return Success(result, "License list fetched succesfully");
            }
            catch (Exception ex)
            {
                return Exception(ex, "Error while fetching license list!");
            }
        }

        
    }

}

