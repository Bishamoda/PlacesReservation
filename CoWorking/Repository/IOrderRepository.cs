using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetAllOrders();
        public Order UpDate(Order orders);
        public Order DeleteOrder(int id);
        public Order GetOrderByID(int id);
        public Order AddOrder(Order orders);

    }

}
