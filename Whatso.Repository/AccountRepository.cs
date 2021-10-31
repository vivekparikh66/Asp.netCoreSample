using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Whatso.Common;
using Whatso.Configuartion;
using Whatso.Model;
using Whatso.Repository.Interface;

namespace Whatso.Repository
{
    public class AccountRepository : GenericRepository, IAccountRepository, IDisposable
    {
        public IDbConnection DbConnection { get; }

        public void Dispose()
        {
            DbConnection?.Dispose();
        }

        public AccountRepository(IAppConfiguration appConfiguration)
        {
            ConnectionString = appConfiguration.ConnectionString.DefaultDbConnection;
            DbConnection = new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            try
            {
                return await DbConnection.QueryAsync<Country>(ConstantsForSp.usp_GetCountries, commandType: CommandType.StoredProcedure);
            }
            catch(Exception e)
            {
                return new List<Country>();
            }
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            try
            {
                return await DbConnection.QueryAsync<City>(ConstantsForSp.usp_GetCities, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                return new List<City>();
            }
        }
        public IEnumerable<MyOrders> GetMyOrders(int customerId)
        {
            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@customerId", customerId);
            return DbConnection.Query<MyOrders>(ConstantsForSp.usp_GetMyOrders, dbParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<Accounts> GetAccountDetaiById(int Id)
        {
            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@Id", Id);
            return await DbConnection.QueryFirstOrDefaultAsync<Accounts>(ConstantsForSp.usp_GetAccountDetails, dbParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> UpdateAccountDetails(Accounts data)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", data.Id);
            dynamicParameters.Add("@firstName", data.FirstName);
            dynamicParameters.Add("@lastName", data.LastName);
            dynamicParameters.Add("@companyName", data.CompanyName);
            dynamicParameters.Add("@companyAddress", data.CompanyAddress);
            dynamicParameters.Add("@gst", data.GSTNo);
            dynamicParameters.Add("@companyCityId", data.CompanyCityId);
            dynamicParameters.Add("@companyCountryId", data.CompanyCountryId);
            dynamicParameters.Add("@companyCurrencyId", data.CompanyCurrencyId);
            dynamicParameters.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await DbConnection.ExecuteScalarAsync<int>(sql: ConstantsForSp.usp_UpdateAccount, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            var id = dynamicParameters.Get<int>("@result");
            return id;
        }

        public async Task<int> InsertWhiteLabelDetail(WhiteLabel data,string rgb)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            CommonHelper common = new CommonHelper();

            dynamicParameters.Add("@accountid", data.AccountId.ToString());
            dynamicParameters.Add("@companyname", data.CompanyName.Trim());
            dynamicParameters.Add("@websiteurl", data.WebsiteURL.Trim());
            dynamicParameters.Add("@softwarename", data.EXEName.Trim());
            dynamicParameters.Add("@softwarecolor", rgb.ToString().Trim());
            dynamicParameters.Add("@foldername", data.CompanyName.Trim() + ".zip");
            dynamicParameters.Add("@doc", common.getDOCMtime());
            dynamicParameters.Add("@dom", common.getDOCMtime());
           
            dynamicParameters.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await DbConnection.ExecuteScalarAsync<int>(sql: ConstantsForSp.usp_InsertWhiteLabel, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            var id = dynamicParameters.Get<int>("@result");
            return id;
        }
    }
}
