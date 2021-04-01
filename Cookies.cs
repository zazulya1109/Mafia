using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class Cookies
    {
        public static string Get(string name, HttpRequest request)
        {
            if (request.Cookies[name] == null)
            {
                throw new Exception("Я такого не говорил");
            }
            else
            {
                return request.Cookies[name];
            }
        }

        public static void Append(string name, object value, HttpResponse response) => response.Cookies.Append(name, value.ToString());

    }
}
