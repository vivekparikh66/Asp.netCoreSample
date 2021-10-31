using System;
using System.Collections.Generic;
using System.Text;
using Whatso.Configuration.Models;

namespace Whatso.Configuartion
{
    public interface IAppConfiguration
    {
        AppConfiguration.ConnectionStrings ConnectionString { get; }
        WhatsoAPISettings WhatsoAPISettings { get; }
    }
}
