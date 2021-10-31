using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatso.API.DTOs
{
    public interface ISerializableObject
    {
        string GetPrimaryPropertyName();
        Type GetPrimaryPropertyType();
    }
}
