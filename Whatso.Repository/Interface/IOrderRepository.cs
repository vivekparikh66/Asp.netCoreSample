using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;

namespace Whatso.Repository.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<MyOrders> GetMyOrders(int customerId);
        Task<int> InsertWhiteLabelDetail(WhiteLabel data,string rgb);
        IEnumerable<GenerateLicenseModel> GetLicenses(int customerId);
        IEnumerable<GenerateLicenseModel> SaveLicenses(GenerateLicenseModel model);
        int DeleteLicenses(int licenseid);
    }
}
