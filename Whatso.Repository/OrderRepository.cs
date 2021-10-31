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
    public class OrderRepository : GenericRepository, IOrderRepository, IDisposable
    {
        public IDbConnection DbConnection { get; }

        public void Dispose()
        {
            DbConnection?.Dispose();
        }

        public OrderRepository(IAppConfiguration appConfiguration)
        {
            ConnectionString = appConfiguration.ConnectionString.DefaultDbConnection;
            DbConnection = new SqlConnection(ConnectionString);
        }

        public IEnumerable<MyOrders> GetMyOrders(int customerId)
        {
            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@customerId", customerId);
            return DbConnection.Query<MyOrders>(ConstantsForSp.usp_GetMyOrders, dbParameters, commandType: CommandType.StoredProcedure);
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

        public IEnumerable<GenerateLicenseModel> GetLicenses(int customerId)
        {
            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@accountid", customerId);
            return DbConnection.Query<GenerateLicenseModel>(ConstantsForSp.usp_GetLicense, dbParameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<GenerateLicenseModel> SaveLicenses(GenerateLicenseModel model)
        {
            CommonHelper common = new CommonHelper();

            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@accountid", model.AccountId);
            dbParameters.Add("@email", model.Email);
            dbParameters.Add("@startdate", model.StartDate); 
            dbParameters.Add("@enddate", model.EndDate);
            dbParameters.Add("@doc", common.getDOCMtime());
            dbParameters.Add("@dom", common.getDOCMtime());
                        
            return DbConnection.Query<GenerateLicenseModel>(ConstantsForSp.usp_SaveLicense, dbParameters, commandType: CommandType.StoredProcedure);
        }

        public int DeleteLicenses(int licenseid)
        {
            CommonHelper common = new CommonHelper();

            DynamicParameters dbParameters = new DynamicParameters();
            dbParameters.Add("@dom", common.getDOCMtime());
            dbParameters.Add("@licenseid", licenseid);
            dbParameters.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            DbConnection.ExecuteScalarAsync<int>(ConstantsForSp.usp_DeleteLicense, dbParameters, commandType: CommandType.StoredProcedure);
            var id = dbParameters.Get<int>("@result");
            return id;
        }
    }
}
