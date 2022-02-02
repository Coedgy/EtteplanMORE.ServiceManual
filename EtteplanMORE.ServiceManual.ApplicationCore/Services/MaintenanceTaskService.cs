using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            return await DapperQuery("SELECT * FROM MaintenanceTasks ORDER BY importance desc, issueDate desc");
        }

        public async Task<MaintenanceTask> Get(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await DapperQueryParameters("SELECT * FROM MaintenanceTasks WHERE id = @Id", parameters);
            
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<MaintenanceTask>> Search(MaintenanceTaskSearch variables)
        {
            // If ID set, just return a list with the one task
            if (variables.Id != 0)
            {
                var task = await Get(variables.Id);

                if (task == null)
                {
                    return null;
                }
                
                var list = new List<MaintenanceTask> {task};
                return list;
            }

            // Build string in parts to avoid redundant query statements. Also add ImportanceMin first so you can append "AND variable = @Variable"
            var queryString = new StringBuilder("SELECT * FROM MaintenanceTasks WHERE importance >= @ImportanceMin");
            var parameters = new DynamicParameters();

            if (variables.DeviceId != 0)
            {
                parameters.Add("@DeviceId", variables.DeviceId);
                queryString.Append(" AND deviceId = @DeviceId");
            }

            if (variables.IssueDateFrom != DateTime.MinValue)
            {
                parameters.Add("@IssueDateFrom", variables.IssueDateFrom);
                queryString.Append(" AND issueDate > @IssueDateFrom");
            }
            
            if (variables.IssueDateTo != DateTime.MinValue)
            {
                parameters.Add("@IssueDateTo", variables.IssueDateTo);
                queryString.Append(" AND issueDate < @IssueDateTo");
            }
            
            if (variables.DescriptionIncludes != null)
            {
                parameters.Add("@DescriptionIncludes", $"%{ variables.DescriptionIncludes }%"); //Add wildcards
                queryString.Append(" AND description LIKE @DescriptionIncludes");
            }

            if (variables.ImportanceMax != 0)
            {
                parameters.Add("@ImportanceMax", variables.ImportanceMax);
                queryString.Append(" AND importance <= @ImportanceMax");
            }

            if (variables.Closed != null)
            {
                parameters.Add("@Closed", variables.Closed);
                queryString.Append(" AND closed = @Closed");
            }

            parameters.Add("@ImportanceMin", variables.ImportanceMin);
            queryString.Append(" ORDER BY importance desc, issueDate desc");
            
            var result = await DapperQueryParameters(queryString.ToString(), parameters);

            return result;
        }

        public async Task<IEnumerable<MaintenanceTask>> GetByDevice(int deviceId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeviceId", deviceId);
            var result = await DapperQueryParameters("SELECT * FROM MaintenanceTasks WHERE deviceId = @DeviceId ORDER BY importance desc, issueDate desc", parameters);

            return result;
        }

        public async Task<IAsyncResult> PostTask(MaintenanceTask task)
        {
            if (string.IsNullOrEmpty(task.Description))
            {
                task.Description = "No description";
            }

            if (task.IssueDate == DateTime.MinValue)
            {
                task.IssueDate = DateTime.Now;
            }

            if (task.Importance == 0)
            {
                task.Importance = ImportanceLevel.Mild;
            }

            var parameters = new DynamicParameters();
            parameters.Add("@DeviceId", task.DeviceId);
            parameters.Add("@IssueDate", task.IssueDate);
            parameters.Add("@Description", task.Description);
            parameters.Add("@Importance", task.Importance);
            parameters.Add("@Closed", task.Closed);
            
            var queryTask = DapperQueryParameters(
                "INSERT INTO MaintenanceTasks (deviceId, issueDate, description, importance, closed) VALUES (@DeviceId, @IssueDate, @Description, @Importance, @Closed)",
                parameters);

            return await Task.FromResult(queryTask);
        }

        public async Task<IAsyncResult> UpdateTask(MaintenanceTask updatedTask)
        {
            // Build string in parts to avoid redundant update statements
            var queryString = new StringBuilder("UPDATE MaintenanceTasks SET closed = @Closed");
            var parameters = new DynamicParameters();

            if (updatedTask.DeviceId != 0)
            {
                parameters.Add("@DeviceId", updatedTask.DeviceId);
                queryString.Append(", deviceId = @DeviceId");
            }

            if (updatedTask.IssueDate != DateTime.MinValue)
            {
                parameters.Add("@IssueDate", updatedTask.IssueDate);
                queryString.Append(", issueDate = @IssueDate");
            }

            // Default the description if its empty, but not null (PUT request had description='')
            if (updatedTask.Description != null)
            {
                parameters.Add("@Description", updatedTask.Description);
                queryString.Append(", description = @Description");
            }
            else if (updatedTask.Description == "")
            {
                queryString.Append(", description = 'No description'");
            }

            if (updatedTask.Importance != 0)
            {
                parameters.Add("@Importance", updatedTask.Importance);
                queryString.Append(", importance = @Importance");
            }
            
            parameters.Add("@Closed", updatedTask.Closed);
            parameters.Add("@Id", updatedTask.Id);
            queryString.Append(" WHERE id = @Id");

            var queryTask = DapperQueryParameters(queryString.ToString(), parameters);

            return await Task.FromResult(queryTask);
        }

        public async Task<IAsyncResult> DeleteTask(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var queryTask = DapperQueryParameters("DELETE FROM MaintenanceTasks WHERE id = @Id", parameters);

            return await Task.FromResult(queryTask);
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