using System.Configuration;

namespace ChatApplication.Infrastructure.Contracts
{
    public class ConfigSettings : IApplicationSettings
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string GetConnection(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
