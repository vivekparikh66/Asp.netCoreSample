using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatso.API.DTOs
{
    public class RootObjectDto: ISerializableObject
    {
        public RootObjectDto()
        {
            Data = new List<object>();
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IList<object> Data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }
        public Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }
    }
}
