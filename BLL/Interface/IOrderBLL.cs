using System.Collections.Generic;
using DTO;
using DTO.Models;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IOrderBLL
    {
        void AddOrder(Order order);
        Order CreateOrder(OrderSession orderSession);
        Order CreateOrder(OrderDTO orderDTO);
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);
        IEnumerable<Order> GetOrder(string ReferenceNumber);
        IEnumerable<Order> GetAllOrdersConnections();
        string CanCreateOrder(OrderDTO dto);
    }
}