using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatso.API.DTOs
{
    public class ResponseModel
    {
        public int ResponseStatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
