using System;
using System.Collections.Generic;
using System.Text;

namespace Whatso.Common
{
    public static class APIResponseStatusCode
    {
        public const int Sucess = 200;
        public const int Error = 201;
        public const int ExceptionResponse = 400;
        public const int Unauthorized = 401;
        public const int APINotFound = 403;
    }
}
