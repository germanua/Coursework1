using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DAL
{
    public class OrderRepository
    {
        private const string OrdersFilePath = "orders.json";
        JsonSerializerOptions options = new() { WriteIndented = true };

        public void SaveOrders(List<Order> orders)
        {
            using FileStream fileStream = new(OrdersFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0); // Fixes issue with deleting nodes
            JsonSerializer.Serialize(fileStream, orders, options);
        }

        public List<Order> LoadOrders()
        {
            try
            {
                using FileStream fileStream = new(OrdersFilePath, FileMode.Open);
                return (List<Order>)JsonSerializer.Deserialize(fileStream, typeof(List<Order>), options);
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }
    }
}