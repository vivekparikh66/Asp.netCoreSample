using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;

namespace Whatso.Services.Interface
{
    public interface IOrderService
    {
        Task<MyorderDetailsModel> GetMyOrders(int customerId);
        Task<int> CreateWhiteLabel(WhiteLabel model);
        Task<GenerateLicenseModel> GetLicenses(int customerId);
        Task<GenerateLicenseModel> SaveLicenses(GenerateLicenseModel model);
        Task<int> DeleteLicenses(int licenseid);
    }
}
