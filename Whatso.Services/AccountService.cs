using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whatso.Model;
using Whatso.Repository.Interface;
using Whatso.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Whatso.Common;
using System.IO;
using System.Data;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Whatso.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;            
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _accountRepository.GetCountries();
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _accountRepository.GetCities();
        }
        public async Task<Accounts> GetAccountDetaiById(int Id)
        {
            return await _accountRepository.GetAccountDetaiById(Id);
        }
        public async Task<int> UpdateAccountDetails(Accounts model)
        {
            return await _accountRepository.UpdateAccountDetails(model);
        }
       
    }
}
