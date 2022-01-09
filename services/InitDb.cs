using catalogapp.Models;
using System;
namespace catalogapp.Services{
    public static class InitDb{
        public static void InitData(CatalogDbRepository catalogDbRepository){
            Console.WriteLine("Initializing db...");
            catalogDbRepository.Categories.Add(new Category{Name="electronics"});
            catalogDbRepository.Categories.Add(new Category{Name="clothes"});
            catalogDbRepository.Categories.Add(new Category{Name="kitchenware"});

            catalogDbRepository.Products.Add(new Product{Name="HP i5", Price=6000, CategoryID=1});
            catalogDbRepository.Products.Add(new Product{Name="Levis Jeans", Price=600, CategoryID=2});
            catalogDbRepository.Products.Add(new Product{Name="Hoodie", Price=60, CategoryID=2});
            catalogDbRepository.SaveChanges();
        }
    }
}