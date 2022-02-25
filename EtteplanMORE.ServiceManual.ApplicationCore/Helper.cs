using System;
using System.Reflection;

namespace EtteplanMORE.ServiceManual.ApplicationCore
{
    public static class Helper
    {
        public static string ConnectionString(string connectionName = "Default")
        {
            if (Environment.GetEnvironmentVariable("IS_DOCKER_CONTAINER") == "true")
            {
                var connString = Environment.GetEnvironmentVariable("DB_STRING");
                return connString;
            }
            
            var cfg = System.Configuration.ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            return cfg.ConnectionStrings.ConnectionStrings[0].ConnectionString;
        }
    }
}