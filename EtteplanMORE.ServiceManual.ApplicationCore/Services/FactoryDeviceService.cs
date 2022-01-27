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
    public class FactoryDeviceService : IFactoryDeviceService
    {
        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            return await DapperQuery("SELECT * FROM FactoryDevices");
        }

        public async Task<FactoryDevice> Get(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await DapperQueryParameters("SELECT * FROM FactoryDevices WHERE id = @Id", parameters);
            
            return result.FirstOrDefault();
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