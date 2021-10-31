using Frigate.API.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.API.DTOs;
using Whatso.Common;

namespace Whatso.API.JSON.Serializers
{
    public class JsonFieldsSerializer : IJsonFieldsSerializer
    {
        public string Serialize(ISerializableObject objectToSerialize, string jsonFields)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException(Constants.ObjectToSerialize);
            }

            IList<string> fieldsList = null;

            if (!string.IsNullOrEmpty(jsonFields))
            {
                string primaryPropertyName = objectToSerialize.GetPrimaryPropertyName();

                fieldsList = GetPropertiesIntoList(jsonFields);

                // Always add the root manually
                fieldsList.Add(primaryPropertyName);
            }

            string json = Serialize(objectToSerialize, fieldsList);

            return json;
        }

        private string Serialize(object objectToSerialize, IList<string> jsonFields = null)
        {
            JToken jToken = JToken.FromObject(objectToSerialize);

            if (jsonFields != null)
            {
                jToken = jToken.RemoveEmptyChildrenAndFilterByFields(jsonFields);
            }

            string jTokenResult = jToken.ToString();

            return jTokenResult;
        }

        private IList<string> GetPropertiesIntoList(string fields)
        {
            IList<string> properties = fields.ToLowerInvariant()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Distinct()
                .ToList();

            return properties;
        }
    }
}
