using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{

    public interface IDeviceRepository
    {

        public IQueryable<Device> GetAllDevice();
        
        public Device GetDeviceByID(string DeviceId);
        
        

    }

}
