using ECommerceAPI.Orders.Interfaces;
using ECommerceAPI.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customersService.GetCustomersAsync(customerId);
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                            productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product Information is Not Available";

                    }

                }

                var result = new
                {
                    Customer = customersResult.IsSuccess ? customersResult.Customer : new { Name = "Customer Info not available"},

                    Orders = ordersResult.Orders
                };
                return (true, result);

            }
            return (false, null);
        }
    }
}
