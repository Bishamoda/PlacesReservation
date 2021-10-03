using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public interface IOrderService
    {
        public IQueryable<Order> GetAllOrders();
        public Order UpDate(Order orders);
        public Order DeleteOrder(int id);
        public Order GetSingleOrderByID(int id);
        public Order AddOrder(Order orders);
        public IQueryable<Order> GetOrderByDate();
        public Order GetOrdersCheck(DateTime StartDate, DateTime EndDate, int WorkSpaceID);
    }
}
