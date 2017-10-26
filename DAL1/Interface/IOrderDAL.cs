using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IOrderDAL
    {
        Order AddOrder(Order order);
        Order AddOrderWithCustomer(Order order);
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);
        IEnumerable<Order> GetOrder(string ReferenceNumber);
        IEnumerable<Order> GetAllOrdersConnections();
    }
}