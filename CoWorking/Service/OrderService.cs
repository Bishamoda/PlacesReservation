using CoWorking.Models;
using CoWorking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order AddOrder(Order orders) => _orderRepository.AddOrder(orders);

        public Order DeleteOrder(int id) => _orderRepository.DeleteOrder(id);

        public IQueryable<Order> GetAllOrders() => _orderRepository.GetAllOrders();

        public IQueryable<Order> GetOrderByDate() => _orderRepository.GetOrderByDate();

        public Order GetOrdersCheck(DateTime StartDate, DateTime EndDate, int WorkSpaceID) => 
            _orderRepository.GetOrdersCheck(StartDate, EndDate, WorkSpaceID);

        public Order GetSingleOrderByID(int id) => _orderRepository.GetSingleOrderByID(id);

        public Order UpDate(Order orders) => _orderRepository.UpDate(orders);
    }
}
