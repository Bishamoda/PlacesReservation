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

    public class AdminPanelController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkSpacesRepository _workSpacesRepository;
        private readonly IDeviceRepository _deviceRepository;

        public AdminPanelController(IOrderRepository orderRepository, IWorkerRepository workerRepository,
            IWorkSpacesRepository workSpacesRepository, IDeviceRepository deviceRepository)
        {

            _orderRepository = orderRepository;
            _workerRepository = workerRepository;
            _workSpacesRepository = workSpacesRepository;
            _deviceRepository = deviceRepository;
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            var model = _orderRepository.GetAllOrders();
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
        public IActionResult AddOrder(Order newOrder, int WorkerID, DateTime StartDate, DateTime EndDate, int WorkSpaceID, string DevicesId)
        {
            var dateNow = DateTime.Now;
            var workersCheck = _workerRepository.GetWorkerByIDCheck(WorkerID);
            var workSpaceCheck = _workSpacesRepository.GetPlacesByIDCheck(WorkSpaceID);
            var orderDublicate = _orderRepository.GetOrdersCheck(StartDate, EndDate, WorkSpaceID);
            var deviceCheck = _deviceRepository.GetDeviceByID(DevicesId);


            if ((StartDate > dateNow) && (EndDate > dateNow) && (workersCheck != null) && (workSpaceCheck != null))
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
                        return RedirectToAction("AddOrder", "AdminPanel", new { ErrorMes });
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

            Order model = id == default ? new Order() : _orderRepository.GetOrderByID(id);
            ViewData["alarm"] = ErrorMes;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult OrdersEdit(Order order, int WorkerID, int WorkSpaceID, string DevicesId)
        {

            var workersCheck = _workerRepository.GetWorkerByIDCheck(WorkerID);
            var workSpaceCheck = _workSpacesRepository.GetPlacesByIDCheck(WorkSpaceID);
            var deviceCheck = _deviceRepository.GetDeviceByID(DevicesId);


            if (ModelState.IsValid)
            {
                if ((workersCheck != null) && (workSpaceCheck != null))
                {
                    if (deviceCheck != null)
                    {
                        _orderRepository.UpDate(order);
                    }
                    else
                    {
                        string ErrorMes = "There are no such devices, check the list of devices in the main menu!";
                        return RedirectToAction("OrdersEdit", "AdminPanel", new { ErrorMes });
                    }

                }
                else
                {
                    string ErrorMes = "You entered incorrect data! Check the employee number, start and end dates of work, place number!";
                    return RedirectToAction("OrdersEdit", "AdminPanel", new { ErrorMes });
                }

            }

            return RedirectToAction("AdminPanel", "AdminPanel");
        }

        [Authorize]
        [HttpPost]
        public IActionResult OrdersDelete(int id)
        {
            _orderRepository.DeleteOrder(id);
            return RedirectToAction("AdminPanel", "AdminPanel");
        }

    }
}
