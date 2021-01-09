using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Data;
using MyShop.API.Domain;
using MyShop.API.DTO;
using MyShop.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IProductTagsHelper _productTagsHelper;
        public ProductService(DataContext dataContext,IMapper mapper,IProductTagsHelper productTagsHelper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _productTagsHelper = productTagsHelper;
        }
        public  async  Task<List<Product>> GetAllProductsAsync()
        {

            var productsDTO = await _dataContext.Products.ToListAsync();
            var products = _mapper.Map<List<Product>>(productsDTO);
            foreach(var product in products)
            {
           product.Tags=_mapper.Map<List<Tag>>( await   _productTagsHelper.AddTagsToProduct(product.ProductId));
            }
           // Parallel.ForEach(products, async product => await _productTagsHelper.AddTagsToProduct(product.ProductId));

            return products;


         

        }

        public  async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var productDTO = await _dataContext.Products.FindAsync(productId);
           if(productDTO == null)
            {
                return null;
            }

            else
            {
                var output = _mapper.Map<Product>(productDTO);
               output.Tags = _mapper.Map<List<Tag>>(await _productTagsHelper.AddTagsToProduct(output.ProductId));
                return output;
            }
            
                
            
        
        }

        public async Task<bool>CreateProductAsync(Product product)
        {
             await _dataContext.Products.AddAsync(_mapper.Map<ProductDTO>(product));
          
            await InsertIntoBridgeTable(product);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;

        }

        private  async Task InsertIntoBridgeTable(Product product)
        {
            //Parallel.ForEach(product.Tags, tag => _dataContext.PTBridges.AddAsync(new ProductTagBridgeDTO
            //{
            //    ProductId=product.ProductId,
            //    TagName=tag.Name
            //}
           
            // )
            //);

            foreach(var tag in product.Tags)
            {
                await _dataContext.Database.ExecuteSqlRawAsync("Insert into PTBridges(ProductId,TagName) Values (@productId,@tagName)"
                    , new SqlParameter("productId", product.ProductId)
                    , new SqlParameter("tagName", tag.Name));


            }
           
        }
        public  async Task<bool> UpdateProductAsync(string productId,Product product)
        {
         if   (!Guid.TryParse(productId,out Guid parsedId))
              {  
             
                return false;
            }

           var productDto= await _dataContext.Products.FindAsync(parsedId);
            if(productDto is null)
            {
                return false;
            }

            else
            {
                var updatedProduct = _mapper.Map<ProductDTO>(product);
                  _dataContext.Products.Update(updatedProduct);

                return await _dataContext.SaveChangesAsync()>0;
            }
            

        }
    }
}
