﻿using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IOrderDAL
    {
        void AddOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);
        IEnumerable<Order> GetOrder(string ReferenceNumber);
        IEnumerable<Order> GetAllOrdersConnections();
    }
}