using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly UserDBContext db;
        public OrderRepository(UserDBContext context)
        {
            db = context;
        }

        public Order AddOrder(Order orders)
        {
            
            db.Orders.Add(orders);
            db.SaveChanges();
            return orders;
        }

        public Order DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
                return null;
            else
            db.Orders.Remove(order);
            db.SaveChanges();
            return order;
        }

        public IQueryable<Order> GetAllOrders() => db.Orders.OrderBy(w => w.OrderId);

        public Order GetSingleOrderByID(int id)
        {
            return db.Orders.Single(o => o.OrderId == id);
        }
        public Order GetOrderByID(int id)
        {
            return (Order)db.Orders.Where(o => o.WorkerId == id);
        }

        public Order UpDate(Order orders)
        {
            
            db.Entry(orders).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return orders;
        }

        public IQueryable<Order> GetOrderByDate()
        {
            return db.Orders.OrderByDescending(o => o.StartDate);
            
        }

        public Order GetOrdersCheck(DateTime StartDate, DateTime EndDate, int WorkSpaceID)
        {
            var check = db.Orders.FirstOrDefault(order => order.StartDate == StartDate && order.EndDate == EndDate && order.WorkSpaceId == WorkSpaceID);
            if (check == null)
            {
                return null;
            }
            else
            {
                return check;
            }
            
        }
    }
}
