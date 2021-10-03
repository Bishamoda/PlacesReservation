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

    public class AdminPanelController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly IWorkerService _workerService;
        private readonly IWorkSpacesService _workSpacesService;
        private readonly IDeviceService _deviceService;

        public AdminPanelController(IOrderService orderService, IWorkerService workerService,
            IWorkSpacesService workSpacesService, IDeviceService deviceService)
        {
            _orderService = orderService;
            _workerService = workerService;
            _workSpacesService = workSpacesService;
            _deviceService = deviceService;
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            if (User.Identity.Name == "admin")
            {
                ViewData["Login"] = User.Identity.Name;
                var model = _orderService.GetAllOrders();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

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
        public IActionResult AddOrder(Order newOrder, int WorkerID, DateTime StartDate, DateTime EndDate, int WorkSpaceID, string DevicesId)
        {
            var dateNow = DateTime.Now;
            var workersCheck = _workerService.GetWorkerByIDCheck(WorkerID);
            var workSpaceCheck = _workSpacesService.GetPlacesByIDCheck(WorkSpaceID);
            var orderDublicate = _orderService.GetOrdersCheck(StartDate, EndDate, WorkSpaceID);
            


            if ((StartDate > dateNow) && (EndDate > dateNow) && (workersCheck != null) && (workSpaceCheck != null))
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
                            return RedirectToAction("AddOrder", "AdminPanel", new { ErrorMes });
                        }
                        
                    }
                    

                }
                else
                {
                    string ErrorMes = "As of this date, this place has already been reserved!";
                    return RedirectToAction("AddOrder", "AdminPanel", new { ErrorMes });
                }

            }
            else
            {
                string ErrorMes = "You entered incorrect data! Check the employee number, start and end dates of work, place number!";
                return RedirectToAction("AddOrder", "AdminPanel", new { ErrorMes });
            }

            return RedirectToAction("AdminPanel", "AdminPanel");
        }

        [Authorize]
        [HttpGet]
        public IActionResult OrdersEdit(int id, string ErrorMes)
        {

            Order model = id == default ? new Order() : _orderService.GetSingleOrderByID(id);
            ViewData["alarm"] = ErrorMes;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult OrdersEdit(Order order, int WorkerID, int WorkSpaceID, string DevicesId)
        {

            var workersCheck = _workerService.GetWorkerByIDCheck(WorkerID);
            var workSpaceCheck = _workSpacesService.GetPlacesByIDCheck(WorkSpaceID);
            

            if (ModelState.IsValid)
            {
                if ((workersCheck != null) && (workSpaceCheck != null))
                {
                    
                    if (DevicesId == null)
                    {
                        _orderService.UpDate(order);
                    }
                    else
                    {
                        var deviceCheck = _deviceService.GetDeviceByID(DevicesId);

                        if (deviceCheck != null)
                        {
                            _orderService.UpDate(order);
                        }
                        else
                        {
                            string ErrorMes = "There are no such devices, check the list of devices in the main menu!";
                            return RedirectToAction("OrdersEdit", "AdminPanel", new { ErrorMes });
                        }
                        
                    }

                }
                else
                {
                    string ErrorMes = "You entered incorrect data! Check the employee number, place number!";
                    return RedirectToAction("OrdersEdit", "AdminPanel", new { ErrorMes });
                }

            }

            return RedirectToAction("AdminPanel", "AdminPanel");
        }

        [Authorize]
        [HttpPost]
        public IActionResult OrdersDelete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("AdminPanel", "AdminPanel");
        }

    }
}
