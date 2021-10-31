using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.API.DTOs;
using Whatso.API.JSON.Serializers;
using Whatso.Common;

namespace Whatso.API.Controllers
{
    public class BaseApiController : Controller
    {
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public BaseApiController(IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }
        protected ActionResult Success(object data, string message = "")
        {
            var usersRootObjectDto = new RootObjectDto();

            //assign data directly if is already in form of list or IEnumerable
            if (data != null && data.GetType().IsGenericType && data.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                usersRootObjectDto.Data = new List<object>((IEnumerable<object>)data);
            }
            else
            {
                usersRootObjectDto.Data.Add(data);
            }

            usersRootObjectDto.Message =
                data == null && string.IsNullOrEmpty(message) ? "No result found" : message;

            //var json = _jsonFieldsSerializer.Serialize(usersRootObjectDto.Data, string.Empty);
            var json = JsonConvert.SerializeObject(usersRootObjectDto.Data);
            ResponseModel model = new ResponseModel();
            model.Data = usersRootObjectDto.Data;
            model.ResponseStatusCode = APIResponseStatusCode.Sucess;
            model.Message = usersRootObjectDto.Message;
            //return new RawJsonActionResult(json);
            return Json(model);
        }

        protected ActionResult Exception(Exception ex, string errorMessage = "")
        {
            var json = JsonConvert.SerializeObject(ex);
            ResponseModel model = new ResponseModel();
            model.Data = json;
            model.ResponseStatusCode = APIResponseStatusCode.ExceptionResponse;
            model.Message = errorMessage;
            return Json(model);
        }

        protected ActionResult Error(object data, string message = "")
        {
            var usersRootObjectDto = new RootObjectDto();
            if (data != null && data.GetType().IsGenericType && data.GetType().GetGenericTypeDefinition() == typeof(List<>))
                usersRootObjectDto.Data = new List<object>((IEnumerable<object>)data);
            else
                usersRootObjectDto.Data.Add(data);
            usersRootObjectDto.Message =
                data == null && string.IsNullOrEmpty(message) ? "No result found" : message;
            var json = JsonConvert.SerializeObject(usersRootObjectDto.Data);
            ResponseModel model = new ResponseModel();
            model.Data = usersRootObjectDto.Data;
            model.ResponseStatusCode = APIResponseStatusCode.Error;
            model.Message = usersRootObjectDto.Message;
            return Json(model);
        }

        protected ActionResult UnAuthorizedAccess(object data, string message = "")
        {
            ResponseModel model = new ResponseModel();
            model.Data = "";
            model.ResponseStatusCode = APIResponseStatusCode.Unauthorized;
            model.Message = message;
            return Json(model);
        }
    }
}
