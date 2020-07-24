using ECommerceAPI.Products.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Products.Db.Interfaces
{
    public interface IProdustsProvider
    {
        Task<(bool IsSucces, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSucces, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
