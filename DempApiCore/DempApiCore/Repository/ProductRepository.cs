using AutoMapper;
using DempApiCore.Data;
using DempApiCore.Data.Entitties;
using DempApiCore.Model;
using DempApiCore.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;

        public ProductRepository(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductsModel>> GetProductsAsync()
        {
            //var records = await _context.Products.Select(x => new ProductsModel()
            //{

            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description,
            //    Title = x.Title
            //}).ToListAsync();

            //return records;

            var records = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductsModel>>(records);
        }

        public async Task<ProductsModel> GetProductsByIdAsync(int productId)
        {
            //var records = await _context.Products.Where(x => x.Id == productId).Select(x => new ProductsModel
            //{

            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description,
            //    Title = x.Title
            //}).FirstOrDefaultAsync();

            //return records;


            //using auto mapper;

            var product = await _context.Products.FindAsync(productId);

            return _mapper.Map<ProductsModel>(product);

        }

        public async Task<int> AddProducts(ProductsModel product)
        {
            var pd = new Products()
            {
                Name = product.Name,
                Description = product.Description,
                Title = product.Title
            };

            _context.Products.Add(pd);
            await _context.SaveChangesAsync();

            return pd.Id;
        }

        public async Task UpdateProductAsync(ProductsModel product, int id)
        {
            //In this process database hits two times

            //var getProductbyId = await _context.Products.FindAsync(id);

            //if(getProductbyId != null)
            //{
            //    getProductbyId.Name = product.Name;
            //    getProductbyId.Title = product.Title;
            //    getProductbyId.Description = product.Description;
            //}

            //await _context.SaveChangesAsync();



            //Another Process databse hits one time(More Preferable)

            var pd = new Products()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Title = product.Title
            };

            _context.Update(pd);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductPatchAsync(JsonPatchDocument product, int id)
        {
            var productById = await _context.Products.FindAsync(id);

            if (productById != null)
            {
                product.ApplyTo(productById);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
        }

        public async Task ProductDeleteAsync(int productId)
        {
            //if Don't have primary key
            // var pd = _context.Products.Where(x => x.Title == "").FirstOrDefault();

            //if Have then

            var pd = new Products()
            {
                Id = productId
            };

            _context.Products.Remove(pd);
            await _context.SaveChangesAsync();
        }
    }
}
