using System.Collections.Generic;
using System.Linq;
using catalogapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalogapp.Controllers{
    [Route("/api/products")]
    public class ProductsRestController: Controller{
        public CatalogDbRepository catalogRespository { get; set; }

        public ProductsRestController(CatalogDbRepository repository)
        {
            this.catalogRespository = repository;
        }
        
        [HttpGet]
        public IEnumerable<Product> ListProducts(){
            return catalogRespository.Products.Include(p=>p.Category);
        }

        [HttpGet("{Id}")]
        public Product GetProduct(int Id){
            return catalogRespository.Products.Include(p=>p.Category).FirstOrDefault(p=>p.ProductID==Id);
        }

        [HttpGet("search")]
        public IEnumerable<Product> searchByKeyword(string kw){
            return catalogRespository.Products.Include(p=>p.Category).Where(p=>p.Name.Contains(kw));
        }
        [HttpPost]
        public Product save([FromBody] Product product){
            catalogRespository.Products.Add(product);
            catalogRespository.SaveChanges();
            return product;
        }
        [HttpPut("{Id}")]
        public Product update([FromBody] Product product, int Id){
            product.ProductID=Id;
            catalogRespository.Products.Update(product);
            catalogRespository.SaveChanges();
            return product;
        }

        [HttpDelete("{Id}")]
        public void delete(int Id){
            Product product = catalogRespository.Products.FirstOrDefault(p=>p.ProductID==Id);
            catalogRespository.Products.Remove(product);
            catalogRespository.SaveChanges();
        }

        [HttpGet("paginate")]
        public IEnumerable<Product> productsPage(int page = 0, int size = 1){
            int skipValue = (page -1) * size;
            return catalogRespository.Products.Include(p=>p.Category)
                                              .Skip(skipValue)
                                              .Take(size);
        }
    }
}