using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IOrderDAL
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);
        IEnumerable<Order> GetAllOrdersConnections();
        Order AddOrderWithCustomer(Order o);

        Order AddOrder(Order o);
    }
}