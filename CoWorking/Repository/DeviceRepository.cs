using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly UserDBContext db;

        public DeviceRepository(UserDBContext context)
        {
            db = context;
        }

        public Device AddDevice(Device devices)
        {
            db.Devices.Add(devices);
            db.SaveChanges();
            return devices;
        }

        public Device DeleteDevice(int id)
        {
            Device devices = db.Devices.Find(id);
            if (devices == null)
                return null;
            else
                db.Devices.Remove(devices);
            db.SaveChanges();
            return devices;
        }

        public IQueryable<Device> GetAllDevice() => db.Devices.OrderBy(d => d.DeviceId);

        public Device GetDeviceByID(int id)
        {
            return db.Devices.Single(d => d.DeviceId == id);
        }

    }
}
