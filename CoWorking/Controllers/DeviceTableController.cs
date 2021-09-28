using CoWorking.Models;
using CoWorking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class DeviceTableController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceTableController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [Authorize]
        public IActionResult IndexDevice()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            else 
            {
                var model = _deviceRepository.GetAllDevice();
                return View(model);
            }
            
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddDevice()
        {

            return View("AddDevice", new Device());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddDevice(Device newDevice)
        {
            
            _deviceRepository.AddDevice(newDevice);
            return RedirectToAction("Index", "DeviceTable");
        }

    }
}
