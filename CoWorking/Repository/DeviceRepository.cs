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

        public IQueryable<Device> GetAllDevice() => db.Devices.OrderBy(d => d.DeviceId);

        public Device GetDeviceByID(string DeviceId)
        {
            int intDeviceId;
            DeviceId = DeviceId.Replace(" ","");
            string[] ids = DeviceId.Split(",");
            Device check = new();
            for (int i = 0; i < ids.Length;)
            {
                intDeviceId = Int32.Parse(ids[i].ToString());

                check = db.Devices.Find(intDeviceId);
                if (check == null)
                {
                    return null;
                }
                else
                {
                    i++;
                }
            }
            return check;
        }


    }
}
