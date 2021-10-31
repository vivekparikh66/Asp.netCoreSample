using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.API.DTOs;

namespace Whatso.API.JSON.Serializers
{
    public interface IJsonFieldsSerializer
    {
        string Serialize(ISerializableObject objectToSerialize, string fields);
    }
}
