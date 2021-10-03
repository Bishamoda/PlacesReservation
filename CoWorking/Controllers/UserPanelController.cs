using CoWorking.Models;
using CoWorking.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IWorkerService _workerService;
        private readonly IWorkSpacesService _workSpacesService;
        private readonly IDeviceService _deviceService;

        public UserPanelController(IOrderService orderService, IWorkerService workerService,
            IWorkSpacesService workSpacesService, IDeviceService deviceService)
        {
            _orderService = orderService;
            _workerService = workerService;
            _workSpacesService = workSpacesService;
            _deviceService = deviceService;
        }

        [Authorize]
        public IActionResult UserPanel()
        {
            TempData["UserId"] = Request.Cookies["UserId"];
            ViewData["Login"] = User.Identity.Name;

            var model = _orderService.GetOrderByDate();
            return View(model);

        }

        [Authorize]
        [HttpGet]
        public IActionResult AddOrder(string ErrorMes)
        {

            ViewData["alarm"] = ErrorMes;
            return View("AddOrder", new Order());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOrder(Order newOrder, string WorkerID, DateTime StartDate, DateTime EndDate, int WorkSpaceID, string DevicesId)
        {
            var dateNow = DateTime.Now;
            var intWorkerID = Int32.Parse(WorkerID);
            var workersCheck = _workerService.GetWorkerByIDCheck(intWorkerID);
            var workSpaceCheck = _workSpacesService.GetPlacesByIDCheck(WorkSpaceID);
            var orderDublicate = _orderService.GetOrdersCheck(StartDate, EndDate, WorkSpaceID);
            


            if ((StartDate > dateNow) && (EndDate > dateNow) && (workSpaceCheck != null) && (workersCheck != null))
            {
                if (orderDublicate == null)
                {
                    if (DevicesId == null)
                    {
                        _orderService.AddOrder(newOrder);
                    }
                    else
                    {
                        var deviceCheck = _deviceService.GetDeviceByID(DevicesId);

                        if (deviceCheck != null)
                        {
                            _orderService.AddOrder(newOrder);
                        }
                        else
                        {
                            string ErrorMes = "There are no such devices, check the list of devices in the main menu!";
                            return RedirectToAction("AddOrder", "UserPanel", new { ErrorMes });
                        }


                    }
                    

                }
                else
                {
                    string ErrorMes = "As of this date, this place has already been reserved!";
                    return RedirectToAction("AddOrder", "UserPanel", new { ErrorMes });
                }

            }
            else
            {
                string ErrorMes = "You entered incorrect data! Check the employee number, start and end dates of work, place number!";
                return RedirectToAction("AddOrder", "UserPanel", new { ErrorMes });
            }

            return RedirectToAction("UserPanel", "UserPanel");
        }
    }
}
