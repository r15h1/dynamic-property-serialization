using System;
using System.Collections.Generic;

namespace DynamicSerialization.Models
{
    public static class Repository
    {
        public static List<Product> GetProducts() => new List<Product> {
            new Product { 
                Id = 1, 
                Name = "iPhone 8", 
                Description = "Mobile phone made by Apple and running on iOS", 
                Price=1000 
            },

            new Product { 
                Id = 2, 
                Name = "Galaxy 10", 
                Description = "Manufactured by Samsung and running Android OS", 
                Price=999 
            },

            new Product { 
                Id = 3, 
                Name = "Pixel", 
                Description = "Google's phone, running Android", 
                Price=888 
            },

            new Product { 
                Id = 4, 
                Name = "Librem", 
                Description = "Built on PureOS Linux distro, Designed by Purism", 
                Price=777 
            },
        };
    }
}