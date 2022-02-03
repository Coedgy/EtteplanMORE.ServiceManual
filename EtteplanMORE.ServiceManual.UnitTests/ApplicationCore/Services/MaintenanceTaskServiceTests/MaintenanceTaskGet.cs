using System.Linq;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.MaintenanceTaskServiceTests
{
    public class MaintenanceTaskGet
    {
        [Fact]
        public async void AllTasks()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var tasks = (await maintenanceTaskService.GetAll()).ToList();

            Assert.NotNull(tasks);
            Assert.NotEmpty(tasks);
            Assert.True(tasks.Count >= 15);
        }
        
        [Fact]
        public async void GetTaskByID()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var tasks = (await maintenanceTaskService.GetAll()).ToList();

            Assert.NotNull(tasks);
            Assert.NotEmpty(tasks);
            Assert.True(tasks.Count >= 15);
        }

        [Fact]
        public async void DeviceHasTasks()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();
            int fdId = 25;

            var tasks = (await maintenanceTaskService.GetByDevice(fdId)).ToList();

            Assert.NotNull(tasks);
            Assert.True(tasks.Count == 3);
        }
        
        [Fact]
        public async void DeviceHasNoTasks()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();
            int fdId = 24;

            var tasks = (await maintenanceTaskService.GetByDevice(fdId)).ToList();
            
            Assert.Empty(tasks);
        }

        [Fact]
        public async void SearchForClosedTasks()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var searchParameters = new MaintenanceTaskSearch
            {
                Closed = true
            };

            var tasks = (await maintenanceTaskService.Search(searchParameters)).ToList();

            Assert.Equal(6, tasks.Count);
            
            foreach (var task in tasks)
            {
                Assert.True(task.Closed);
            }
        }
        
        [Fact]
        public async void SearchForCriticalTasks()
        {
            IMaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService();

            var searchParameters = new MaintenanceTaskSearch
            {
                ImportanceMin = ImportanceLevel.Critical
            };

            var tasks = (await maintenanceTaskService.Search(searchParameters)).ToList();

            Assert.Equal(5, tasks.Count);
            
            foreach (var task in tasks)
            {
                Assert.Equal(ImportanceLevel.Critical, task.Importance);
            }
        }
    }
}