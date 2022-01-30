using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class MaintenanceTaskService : IMaintenanceTaskService
    {
        public async Task<IEnumerable<MaintenanceTask>> GetAll()
        {
            return await DapperQuery("SELECT * FROM MaintenanceTasks");
        }

        public async Task<MaintenanceTask> Get(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await DapperQueryParameters("SELECT * FROM MaintenanceTasks WHERE id = @Id", parameters);
            
            return result.FirstOrDefault();
        }
        
        // Query without parameters
        private async Task<IEnumerable<MaintenanceTask>> DapperQuery(string query)
        {
            using (IDbConnection connection = new MySqlConnection(Helper.ConnectionString()))
            {
                return await connection.QueryAsync<MaintenanceTask>(query);
            }
        }

        // Query with Dapper's DynamicParameters Bag
        private async Task<IEnumerable<MaintenanceTask>> DapperQueryParameters(string query, DynamicParameters parameters)
        {
            using (IDbConnection connection = new MySqlConnection(Helper.ConnectionString()))
            {
                return await connection.QueryAsync<MaintenanceTask>(query, parameters);
            }
        }
    }
}