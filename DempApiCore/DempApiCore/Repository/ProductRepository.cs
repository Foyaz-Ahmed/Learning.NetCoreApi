using DempApiCore.Data;
using DempApiCore.Data.Entitties;
using DempApiCore.Model;
using DempApiCore.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DempApiCore.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly DBContext _context;

        public ProductRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<ProductsModel>> GetProductsAsync()
        {
            var records = await _context.Products.Select(x => new ProductsModel()
            {

                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Title = x.Title
            }).ToListAsync();

            return records;
        }

        public async Task<ProductsModel> GetProductsByIdAsync(int productId)
        {
            var records = await _context.Products.Where(x=> x.Id == productId).Select(x => new ProductsModel
            {

                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Title = x.Title
            }).FirstOrDefaultAsync();

            return records;
        }

        public async Task<int> AddProducts(ProductsModel product)
        {
            var pd = new Products()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Title = product.Title
            };

            _context.Add(pd);
            await _context.SaveChangesAsync();

            return product.Id;
        }
    }
}
