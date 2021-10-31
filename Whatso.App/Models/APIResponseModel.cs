using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatso.App.Models
{
    public class APIResponseModel
    {
        public int ResponseStatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
