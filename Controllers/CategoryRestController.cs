using System.Collections.Generic;
using System.Linq;
using catalogapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalogapp.Controllers{
    [Route("/api/categories")]
    public class CategoryRestController: Controller{
        public CatalogDbRepository catalogRespository { get; set; }

        public CategoryRestController(CatalogDbRepository repository)
        {
            this.catalogRespository = repository;
        }
        
        [HttpGet]
        public IEnumerable<Category> ListCategories(){
            return catalogRespository.Categories;
        }

        [HttpGet("{Id}")]
        public Category GetCategory(int Id){
            return catalogRespository.Categories.FirstOrDefault(c=>c.CategoryID==Id);
        }
        

        [HttpPost]
        public Category save([FromBody] Category category){
            catalogRespository.Categories.Add(category);
            catalogRespository.SaveChanges();
            return category;
        }
        [HttpPut("{Id}")]
        public Category update([FromBody] Category category, int Id){
            category.CategoryID=Id;
            catalogRespository.Categories.Update(category);
            catalogRespository.SaveChanges();
            return category;
        }

        [HttpDelete("{Id}")]
        public void delete(int Id){
            Category category = catalogRespository.Categories.FirstOrDefault(c=>c.CategoryID==Id);
            catalogRespository.Categories.Remove(category);
            catalogRespository.SaveChanges();
        }

        [HttpGet("{Id}/products")]
        public IEnumerable<Product> getProductsByCategory(int Id){
            Category category = catalogRespository.Categories.Include(c=>c.Products).FirstOrDefault(c=>c.CategoryID==Id);
            return category.Products;
        }
    }
}