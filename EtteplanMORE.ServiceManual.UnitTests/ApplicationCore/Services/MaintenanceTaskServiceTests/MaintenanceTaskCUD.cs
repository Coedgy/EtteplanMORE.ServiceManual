using System.Linq;
using System.Threading;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.MaintenanceTaskServiceTests
{
    [TestCaseOrderer("EtteplanMORE.ServiceManual.UnitTests.Orderers.AlphabeticalOrderer", "EtteplanMORE.ServiceManual.UnitTests")]
    public class MaintenanceTaskCUD
    {
        [Fact]
        public async void ACreateTask()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var tempTask = new MaintenanceTask
            {
                DeviceId = 20,
                Description = "Test task!",
                Importance = ImportanceLevel.Important
            };

            await maintenanceTaskService.PostTask(tempTask);

            var searchParameters = new MaintenanceTaskSearch
            {
                DeviceId = 20,
                DescriptionIncludes = "Test task!"
            };

            var tasks = (await maintenanceTaskService.Search(searchParameters)).ToList();

            // Because the PostTask is asynchronous, the query might not have gone through yet.
            // This loop gives it 3 retries, after that it's pretty clear it did not create the item
            for (int i = 0; i < 3; i++)
            {
                if (tasks.Count == 0)
                {
                    Thread.Sleep(500);
                    tasks = (await maintenanceTaskService.Search(searchParameters)).ToList();
                }
                else
                {
                    break;
                }
            }

            Assert.NotEmpty(tasks);
            Assert.Equal(ImportanceLevel.Important, tasks[0].Importance);
            Assert.False(tasks[0].Closed);
        }

        [Fact]
        public async void BUpdateTask()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var searchParameters = new MaintenanceTaskSearch
            {
                DeviceId = 20,
                DescriptionIncludes = "Test task!",
                ImportanceMin = ImportanceLevel.Important,
                ImportanceMax = ImportanceLevel.Important
            };

            var taskId = (await maintenanceTaskService.Search(searchParameters)).ToList()[0].Id;

            var updatedTask = new MaintenanceTask
            {
                Id = taskId,
                DeviceId = 10,
                Description = "Updated test task!",
                Closed = true
            };

            await maintenanceTaskService.UpdateTask(updatedTask);
            
            var task = await maintenanceTaskService.Get(taskId);
            
            // Because the UpdateTask is asynchronous, the query might not have gone through yet.
            // This loop gives it 3 retries, after that it's pretty clear it did not update the item
            for (int i = 0; i < 3; i++)
            {
                if (task.Closed == false)
                {
                    Thread.Sleep(500);
                    task = await maintenanceTaskService.Get(taskId);
                }
                else
                {
                    break;
                }
            }
            
            Assert.True(task.Closed);
            Assert.Equal(10, task.DeviceId);
            Assert.Equal("Updated test task!", task.Description);
        }

        [Fact]
        public async void CDeleteTask()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();
            
            var searchParameters = new MaintenanceTaskSearch
            {
                DeviceId = 10,
                DescriptionIncludes = "Updated test task!",
                ImportanceMin = ImportanceLevel.Important,
                ImportanceMax = ImportanceLevel.Important,
                Closed = true
            };

            var taskId = (await maintenanceTaskService.Search(searchParameters)).ToList()[0].Id;

            await maintenanceTaskService.DeleteTask(taskId);

            var task = await maintenanceTaskService.Get(taskId);
            
            // Because the DeleteTask is asynchronous, the query might not have gone through yet.
            // This loop gives it 3 retries, after that it's pretty clear it did not delete the item
            for (int i = 0; i < 3; i++)
            {
                if (task != null)
                {
                    Thread.Sleep(500);
                    task = await maintenanceTaskService.Get(taskId);
                }
                else
                {
                    break;
                }
            }
            
            Assert.Null(task);
        }
    }
}