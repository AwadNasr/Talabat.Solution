using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class TalabatDataSeed
    {
        public async static Task SeedAsync(TalabatDbContext context)
        {
            if (context.ProductBrands.Count()==0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {
                    brands = brands.Select(b => new ProductBrand
                    {
                        Name = b.Name,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false
                    }).ToList();
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                } 
            }


            if (context.ProductCategories.Count()==0)
            {
                var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);
                if (categories?.Count() > 0)
                {
                    categories = categories.Select(b => new ProductCategory
                    {
                        Name = b.Name,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false
                    }).ToList();
                    foreach (var category in categories)
                    {
                        context.Set<ProductCategory>().Add(category);
                    }
                    await context.SaveChangesAsync();
                } 
            }


            if (context.Products.Count()==0)
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                products = products?.Select(p => new Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    PictureUrl = p.PictureUrl,
                    Price = p.Price,
                    BrandId = p.BrandId,
                    CategoryId = p.CategoryId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }).ToList();
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                    await context.SaveChangesAsync();
                } 
            }
        }
    }
}
