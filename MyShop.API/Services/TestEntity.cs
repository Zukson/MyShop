using MyShop.API.Data;
using MyShop.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class TestEntity : ITestEntity
    {
        private readonly DataContext _dataContext;
        private readonly IProductTagsHelper productTagsHelper;
        public TestEntity(DataContext dataContext, IProductTagsHelper productTagsHelper)
        {
            _dataContext = dataContext;
            this.productTagsHelper = productTagsHelper;
        }



        public void Test() 
        {
            var products = _dataContext.Products.ToList();
            var product = productTagsHelper.AddTagsToProduct(products[0]);
            var test = "";

            
        }
    }
}
