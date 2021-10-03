using CoWorking.Models;
using CoWorking.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class DeviceTableController : Controller
    {

        private readonly IDeviceService _deviceService;

        public DeviceTableController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
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
                if (User.Identity.Name == "admin")
                {
                    var model = _deviceService.GetAllDevice();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("IndexDeviceUser", "DeviceTableController");
                }

            }

        }

        [Authorize]
        public IActionResult IndexDeviceUser()
        {

            var model = _deviceService.GetAllDevice();
            return View(model);

        }





    }
}
