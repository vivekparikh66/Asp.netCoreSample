using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Whatso.App.Models;
using Whatso.Common;
using Whatso.Configuartion;
using Whatso.Model;

namespace Whatso.App.Helper
{
    public class APIHelper : IAPIHelper
    {
        public string BaseURL { get; set; }
        public APIHelper(IAppConfiguration appconfiguration)
        {
            BaseURL = appconfiguration.WhatsoAPISettings.APIUrl;
        }
		public async Task<APIResponseModel> Post(string endpoint, object data)
		 {
			string apiurl = BaseURL + endpoint;

			using (HttpClient client = new HttpClient())
			{
                
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

				using (var Response = await client.PostAsync( apiurl, content))
				{
					var responseString = await Response.Content.ReadAsStringAsync();
                    //if (responseString != null)
                    //    return JObject.Parse(responseString)["data"].ToString();
                    //return string.Empty;
                    var detail = JsonConvert.DeserializeObject<APIResponseModel>(responseString);
                    return detail;
                }
			}
		}
        public void CreateWhiteLabel(string endpoint, IFormFile CompanyLOGOFile, IFormFile CompanyICONFile,WhiteLabel objWhiteLabel)
        {
            string apiurl = BaseURL + endpoint;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var _accessor = new HttpContextAccessor();
                    var rootPath = _accessor.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath;
                    
                    CommonHelper common = new CommonHelper();
                    string rgbColor = "141, 213, 73";
                    objWhiteLabel.AccountId = 95370; // ClsUser.GetAccountIdfromCookie();

                    //var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
                    string oldPath = Path.Combine(rootPath + @"\" + Constants.downloadFolderName + "\\Whitelabel\\whatso");
                    string newPath = Path.Combine(rootPath + @"\" + Constants.downloadFolderName + "\\Whitelabel\\" + objWhiteLabel.CompanyName.Trim() + "");
                    if (Directory.Exists(newPath.Trim()))
                    {
                        Directory.Delete(newPath, true);
                    }
                    if (File.Exists(newPath.Trim() + ".zip"))
                    {
                        File.Delete(newPath + ".zip");
                    }
                    common.DirectoryCopy(oldPath, newPath, true);
                     objWhiteLabel.CompanyLOGO = Path.GetFileName(CompanyLOGOFile.FileName);
                     objWhiteLabel.CompanyICON = Path.GetFileName(CompanyICONFile.FileName);
                    using (FileStream fs = System.IO.File.Create(Path.Combine(newPath + "\\Images", "logo.jpg")))
                    {
                            CompanyLOGOFile.CopyTo(fs);
                        fs.Flush();
                    }
                    using (FileStream fs = System.IO.File.Create(Path.Combine(newPath + "\\Images", "favicon.ico")))
                    {
                            CompanyICONFile.CopyTo(fs);
                        fs.Flush();
                    }
                    string path = @"\" + Constants.downloadFolderName + "\\Whitelabel";
                    string strPath = Path.Combine(rootPath + path);
                    
                    DataTable dtxml = new DataTable();
                    DataSet ds = new DataSet();
                    dtxml.TableName = "alldata";
                    dtxml.Columns.Add("name");
                    dtxml.Columns.Add("hcolorcode");
                    dtxml.Columns.Add("mcolorcode");
                    dtxml.Columns.Add("url");
                    dtxml.Columns.Add("key");
                    dtxml.Columns.Add("iscodecanyon");
                    DataRow dr = dtxml.NewRow();
                    dr["name"] = objWhiteLabel.CompanyName.Trim();
                    dr["hcolorcode"] = "44, 156, 134";
                    dr["mcolorcode"] = "237, 245, 238";
                    dr["url"] = objWhiteLabel.WebsiteURL.Trim();
                    dr["key"] = objWhiteLabel.AccountId;
                    dr["iscodecanyon"] = 0;
                    dtxml.Rows.Add(dr);
                    ds.DataSetName = "licence";
                    ds.Tables.Add(dtxml);
                    ds.WriteXml(System.IO.Path.Combine(newPath, "data"));
                    File.Copy(oldPath + "\\Whatso.exe", newPath + "\\" + objWhiteLabel.EXEName.Trim() + ".exe");
                    File.Delete(newPath + "\\Whatso.exe");
                    File.Copy(oldPath + "\\Whatso.exe.config", newPath + "\\" + objWhiteLabel.EXEName.Trim() + ".exe.config");
                    File.Delete(newPath + "\\Whatso.exe.config");
                    ZipFile.CreateFromDirectory(newPath, newPath + ".zip");

                }
                catch (Exception e)
                {
                    
                }
               
            }
        }
    }
}
