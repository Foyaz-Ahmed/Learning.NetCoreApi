using DempApiCore.Model;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DempApiCore.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductsModel>> GetProductsAsync();
        Task<ProductsModel> GetProductsByIdAsync(int id);
        Task<int> AddProducts(ProductsModel product);
        Task UpdateProductAsync(ProductsModel product, int id);
        Task UpdateProductPatchAsync(JsonPatchDocument product, int id);
        Task ProductDeleteAsync(int productId);
    }
}
