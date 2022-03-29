using DempApiCore.Data.Entitties;
using DempApiCore.Model;
using AutoMapper; 

namespace DempApiCore.Helper
{
    public class ProductMapper : Profile 
    {
        public ProductMapper()
        {
           CreateMap<ProductsModel, Products>().ReverseMap();
        }
        
    }
}
