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
    public class UserPanelController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkSpacesRepository _workSpacesRepository;
        private readonly IDeviceRepository _deviceRepository;

        public UserPanelController(IOrderRepository orderRepository, IWorkerRepository workerRepository,
            IWorkSpacesRepository workSpacesRepository, IDeviceRepository deviceRepository)
        {
            _orderRepository = orderRepository;
            _workerRepository = workerRepository;
            _workSpacesRepository = workSpacesRepository;
            _deviceRepository = deviceRepository;
        }

        [Authorize]
        public IActionResult UserPanel()
        {
            TempData["UserId"] = Request.Cookies["UserId"];
            ViewData["Login"] = User.Identity.Name;

            var model = _orderRepository.GetOrderByDate();
            return View(model);

        }
        [Authorize]
        public IActionResult MyOrders(int id)
        {
            id = int.Parse(Request.Cookies["UserId"]);

            var model = _orderRepository.GetOrderByID(id);
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
        public IActionResult AddOrder(Order newOrder, DateTime StartDate, DateTime EndDate, int WorkSpaceID, string DevicesId)
        {
            var dateNow = DateTime.Now;
            var workSpaceCheck = _workSpacesRepository.GetPlacesByIDCheck(WorkSpaceID);
            var orderDublicate = _orderRepository.GetOrdersCheck(StartDate, EndDate, WorkSpaceID);
            var deviceCheck = _deviceRepository.GetDeviceByID(DevicesId);


            if ((StartDate > dateNow) && (EndDate > dateNow) && (workSpaceCheck != null))
            {
                if (orderDublicate == null)
                {
                    if (deviceCheck != null)
                    {
                        _orderRepository.AddOrder(newOrder);
                    }
                    else
                    {
                        string ErrorMes = "There are no such devices, check the list of devices in the main menu!";
                        return RedirectToAction("AddOrder", "UserPanel", new { ErrorMes });
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
