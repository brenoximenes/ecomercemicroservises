using AutoMapper;
using ECommerceAPI.Products.Db;
using ECommerceAPI.Products.Db.Interfaces;
using ECommerceAPI.Products.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Products.Providers
{
    public class ProductsProvider : IProdustsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();

        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Product() { Id = 2, Name = "Mouse", Price = 18, Inventory = 300 });
                dbContext.Products.Add(new Product() { Id = 3, Name = "Mother Board", Price = 150, Inventory = 50 });
                dbContext.Products.Add(new Product() { Id = 4, Name = "Monitor", Price = 90, Inventory = 300 });
                dbContext.Products.Add(new Product() { Id = 5, Name = "Pendrive", Price = 10, Inventory = 600 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucces, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Db.Models.ProductModel>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);

              
            }
        }

        public async Task<(bool IsSucces, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);


            }
        }

    }
}
