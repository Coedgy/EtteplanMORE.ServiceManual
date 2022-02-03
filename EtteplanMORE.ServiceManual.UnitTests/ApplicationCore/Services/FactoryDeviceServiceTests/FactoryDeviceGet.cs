using System.Linq;
using System.Reflection;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.FactoryDeviceServiceTests
{
    public class FactoryDeviceGet
    {
        [Fact]
        public async void AllDevices()
        {
            IFactoryDeviceService factoryDeviceService = new FactoryDeviceService();

            var fds = (await factoryDeviceService.GetAll()).ToList();

            Assert.NotNull(fds);
            Assert.NotEmpty(fds);
            Assert.Equal(50, fds.Count);
        }

        [Fact]
        public async void ExistingDeviceWithId()
        {
            IFactoryDeviceService factoryDeviceService = new FactoryDeviceService();
            int fdId = 1;

            var fd = await factoryDeviceService.Get(fdId);

            Assert.NotNull(fd);
            Assert.Equal(fdId, fd.Id);
        }

        [Fact]
        public async void NonExistingDeviceWithId()
        {
            IFactoryDeviceService factoryDeviceService = new FactoryDeviceService();
            int fdId = 85;

            var fd = await factoryDeviceService.Get(fdId);

            Assert.Null(fd);
        }
    }
}