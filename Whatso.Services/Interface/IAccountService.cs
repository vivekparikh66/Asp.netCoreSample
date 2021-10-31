using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;

namespace Whatso.Services.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<City>> GetCities();
        Task<Accounts> GetAccountDetaiById(int Id);
        Task<int> UpdateAccountDetails(Accounts model);

    }
}
