using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class MaintenanceTaskService : IMaintenanceTaskService
    {
        public Task<IEnumerable<MaintenanceTask>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<MaintenanceTask> Get(int id)
        {
            throw new System.NotImplementedException();
        }
        
        // Query without parameters
        private async Task<IEnumerable<FactoryDevice>> DapperQuery(string query)
        {
            using (IDbConnection connection = new MySqlConnection(Helper.ConnectionString()))
            {
                return await connection.QueryAsync<FactoryDevice>(query);
            }
        }

        // Query with Dapper's DynamicParameters Bag
        private async Task<IEnumerable<FactoryDevice>> DapperQueryParameters(string query, DynamicParameters parameters)
        {
            using (IDbConnection connection = new MySqlConnection(Helper.ConnectionString()))
            {
                return await connection.QueryAsync<FactoryDevice>(query, parameters);
            }
        }
    }
}