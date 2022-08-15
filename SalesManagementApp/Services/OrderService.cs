using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Models;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;

        public OrderService(SalesManagementDbContext salesManagementDbContext)
        {
            this.salesManagementDbContext = salesManagementDbContext;
        }
        public async Task CreateOrder(OrderModel orderModel)
        {
            try
            {
                Order order = new Order
                {
                    OrderDateTime = DateTime.Now,
                    ClientId = orderModel.ClientId,
                    EmployeeId = 9,
                    Price = orderModel.OrderItems.Sum(o=>o.Price),
                    Qty = orderModel.OrderItems.Sum(o=>o.Qty)
                };

                var addedOrder = await this.salesManagementDbContext.Orders.AddAsync(order);
                await this.salesManagementDbContext.SaveChangesAsync();
                int orderId = addedOrder.Entity.Id;
               
                var orderItemsToAdd = ReturnOrderItemsWithOrderId(orderId, orderModel.OrderItems);
                this.salesManagementDbContext.AddRange(orderItemsToAdd);
                
                await this.salesManagementDbContext.SaveChangesAsync();

               await UpdateSalesOrderReportsTable(orderId, order);


            }
            catch (Exception)
            {

                throw;
            }
        }

     private List<OrderItem> ReturnOrderItemsWithOrderId(int orderId, List<OrderItem> orderItems)
        {
            return (from oi in orderItems
                   select new OrderItem
                   {
                       OrderId = orderId,
                       Price = oi.Price,
                       Qty = oi.Qty,    
                       ProductId = oi.ProductId,    
                   }).ToList();
        }
        private async Task UpdateSalesOrderReportsTable(int orderId, Order order)
        {
            try
            {
                List<SalesOrderReport> srItems = await (from oi in this.salesManagementDbContext.OrderItems
                                                        where oi.OrderId == orderId
                                                        select new SalesOrderReport
                                                        {
                                                            OrderId = orderId,
                                                            OrderDateTime = order.OrderDateTime,
                                                            OrderPrice = order.Price,
                                                            OrderQty = order.Qty,
                                                            OrderItemId = oi.Id,
                                                            OrderItemPrice = oi.Price,
                                                            OrderItemQty = oi.Qty,
                                                            EmployeeId = order.EmployeeId,
                                                            EmployeeFirstName = salesManagementDbContext.Employees.FirstOrDefault(e => e.Id == order.EmployeeId).FirstName,
                                                            EmployeeLastName = salesManagementDbContext.Employees.FirstOrDefault(e => e.Id == order.EmployeeId).LastName,
                                                            ProductId = oi.ProductId,
                                                            ProductName = salesManagementDbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId).Name,
                                                            ProductCategoryId = salesManagementDbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId).CategoryId,
                                                            ProductCategoryName = salesManagementDbContext.ProductCategories.FirstOrDefault(c => c.Id == salesManagementDbContext.Products.FirstOrDefault(p => p.Id == oi.ProductId).CategoryId).Name,
                                                            ClientId = order.ClientId,
                                                            ClientFirstName = salesManagementDbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId).FirstName,
                                                            ClientLastName = salesManagementDbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId).LastName,
                                                            RetailOutletId = salesManagementDbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId).RetailOutletId,
                                                            RetailOutletLocation = salesManagementDbContext.RetailOutlets.FirstOrDefault(r => r.Id == salesManagementDbContext.Clients.FirstOrDefault(c => c.Id == order.ClientId).RetailOutletId).Location
                                                        }).ToListAsync();

                this.salesManagementDbContext.AddRange(srItems);
                await this.salesManagementDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
