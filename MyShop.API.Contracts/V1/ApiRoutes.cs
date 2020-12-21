using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyShop.API.Contracts.V1
{
  public static  class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";


        public const string Base = Root + "/" + Version;
        public static class Products
        {
            public const string ProductRoot = "products";
            public const string GetAllProducts = Base+"/" + ProductRoot;
            public const string GetProductById = Base +"/" + ProductRoot +"/"+ "{productId}";
            public const string PostProduct = Base + "/" + ProductRoot;
            public const string UpdateProduct = Base + "/" + ProductRoot + "/" + "{productId}";

        }
    }

   

}
