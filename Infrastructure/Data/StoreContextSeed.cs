using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (!context.ProductBrands.Any())
        {
          var brandsData = File.ReadAllText("../Infrastructure/Data/Seed/brands.json");

          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

          foreach (var brand in brands)
          {
            context.ProductBrands.Add(brand);
          }

          await context.SaveChangesAsync();
        }

        if (!context.ProductTypes.Any())
        {
          var typesData = File.ReadAllText("../Infrastructure/Data/Seed/types.json");

          var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

          foreach (var type in types)
          {
            context.ProductTypes.Add(type);
          }

          await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
          var productsData = File.ReadAllText("../Infrastructure/Data/Seed/products.json");

          var products = JsonSerializer.Deserialize<List<Product>>(productsData);

          foreach (var product in products)
          {
            context.Products.Add(product);
          }

          await context.SaveChangesAsync();
        }
      }
      catch (Exception exception)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();

        logger.LogError(exception.Message);
      }
    }
  }
}