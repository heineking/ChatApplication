using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.Repositories;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace ChatApplication.API.Extensions
{
    public static class BootstraperExtensions
    {
        public static void ConfigureJsonNet()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
    }
}