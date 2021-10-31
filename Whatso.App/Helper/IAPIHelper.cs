using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Whatso.App.Models;
using Whatso.Model;

namespace Whatso.App.Helper
{
    public interface IAPIHelper
    {
        Task<APIResponseModel> Post(string endpoint, object data);
        void CreateWhiteLabel(string endpoint, IFormFile CompanyLOGOFile, IFormFile CompanyICONFile, WhiteLabel objWhiteLabel);
    }
}
