using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;

namespace Whatso.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<City>> GetCities();
        IEnumerable<MyOrders> GetMyOrders(int customerId);
        Task<Accounts> GetAccountDetaiById(int Id);
        Task<int> UpdateAccountDetails(Accounts data);
        Task<int> InsertWhiteLabelDetail(WhiteLabel data,string rgb);
    }
}
