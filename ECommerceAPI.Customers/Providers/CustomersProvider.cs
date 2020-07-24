using AutoMapper;
using ECommerceAPI.Customers.Db;
using ECommerceAPI.Customers.Interfaces;
using ECommerceAPI.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Customers.Providers
{
    public class CustomersProvider : ICostumersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer { Id = 1, Name = "Maria José", Address = "Rua das Flores 4" });
                dbContext.Customers.Add(new Customer { Id = 2, Name = "José Maria", Address = "Rua das Begonias 6" });
                dbContext.Customers.Add(new Customer { Id = 3, Name = "Joâo Landim", Address = "Rua das Margaridas 9" });
                dbContext.Customers.Add(new Customer { Id = 4, Name = "Douglas Roberto", Address = "Rua das Rosas 30" });
                dbContext.Customers.Add(new Customer { Id = 5, Name = "Marcelino Pâo e Vinho", Address = "Rua das Tulipas 20" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
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
        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer>(customer);
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
