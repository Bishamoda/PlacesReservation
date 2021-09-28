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

        public AdminPanelController(IOrderRepository orderRepository)
        {

            _orderRepository = orderRepository;

        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            var model = _orderRepository.GetAllOrders();
            return View(model);

        }

        [Authorize]
        [HttpGet]
        public IActionResult AddOrder()
        {
            return View("AddOrder", new Order());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOrder(Order newOrder)
        {
            //if (StartDate >= DateTime.Now && EndDate >= StartDate)
            //{
                


            //}

            _orderRepository.AddOrder(newOrder);
            return RedirectToAction("AdminPanel", "AdminPAnel");
        }

        [Authorize]
        [HttpGet]
        public IActionResult OrdersEdit(int id)
        {
            Order model = id == default ? new Order() : _orderRepository.GetOrderByID(id);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult OrdersEdit(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.UpDate(order);
                return RedirectToAction("AdminPanel", "AdminPanel");
            }
            return View(order);
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
