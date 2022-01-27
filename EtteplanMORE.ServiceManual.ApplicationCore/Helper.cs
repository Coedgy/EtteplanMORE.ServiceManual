namespace EtteplanMORE.ServiceManual.ApplicationCore
{
    internal static class Helper
    {
        public static string ConnectionString(string connectionName = "Default")
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}