using CoWorking.Repository;
using CoWorking.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using NUnit.Framework;

namespace TestProject1
{

    [TestClass]
    public class UnitTest1
    {
        private readonly DeviceService _deviceService;
        private readonly Mock<IDeviceRepository> _deviceRepositoryMock = new Mock<IDeviceRepository>();
        public UnitTest1()
        {
            _deviceService = new DeviceService(_deviceRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllDevices()   
        {

            var devices = _deviceService.GetAllDevice();

            NUnit.Framework.Assert.IsEmpty(devices);
        }


    }
}
