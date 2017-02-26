using System.Configuration;
using ChatApplication.Data.Contracts;

namespace ChatApplication.Infrastructer.Contracts
{
    public class ConfigSettings : IApplicationSettings
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
