using System;
using System.Reflection;

namespace EtteplanMORE.ServiceManual.ApplicationCore
{
    public static class Helper
    {
        public static string ConnectionString(string connectionName = "Default")
        {
            var cfg = System.Configuration.ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            return cfg.ConnectionStrings.ConnectionStrings[0].ConnectionString;
        }
    }
}