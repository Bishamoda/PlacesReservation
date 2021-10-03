using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public interface IDeviceService
    {
        public IQueryable<Device> GetAllDevice();

        public Device GetDeviceByID(string DeviceId);

    }
}
