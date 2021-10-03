using CoWorking.Models;
using CoWorking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        public IQueryable<Device> GetAllDevice() => _deviceRepository.GetAllDevice();

        public Device GetDeviceByID(string DeviceId) => _deviceRepository.GetDeviceByID(DeviceId);

    }
}
