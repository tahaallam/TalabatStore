using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var ProductBrands = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.Json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrands);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbcontext.AddAsync(Brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.ProductTypes.Any())
            {
                var ProductTypes = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypes);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.AddAsync(Type);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbcontext.AddAsync(product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
