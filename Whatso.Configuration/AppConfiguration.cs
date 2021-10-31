using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Whatso.Configuration.Models;

namespace Whatso.Configuartion
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(IConfiguration configuration)
        {
            ConnectionString = new ConnectionStrings()
            {
                DefaultDbConnection = configuration.GetConnectionString("WhatsoDB")
            };
            var apiSettings = configuration.GetSection("WhatsoAPISettings");
            WhatsoAPISettings = new WhatsoAPISettings();

            if (apiSettings != null)
            {
                WhatsoAPISettings.APIUrl = apiSettings["APIUrl"];
            }
        }
        public ConnectionStrings ConnectionString { get; set; }
        public WhatsoAPISettings WhatsoAPISettings { get; set; }
        public class ConnectionStrings
        {
            public string DefaultDbConnection { get; set; }
        }
    }
}
