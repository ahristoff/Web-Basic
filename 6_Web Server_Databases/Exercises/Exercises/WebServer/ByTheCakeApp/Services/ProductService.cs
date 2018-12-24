
namespace WebServer.ByTheCakeApp.Services
{
    using Data;
    using Data.Models;
    using ViewModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
       
        public void Create(string name, decimal price, string imageUrl)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };

                db.Add(product);
                db.SaveChanges();
            }
        }
        
        public IEnumerable<SearchProductViewModel> All(string searchTerm)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var resultsQuery = db.Products.ToList(); //AsQueryable()

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var resultsfiltredCakes = resultsQuery
                        .Where(pr => pr.Name.ToLower().Contains(searchTerm.ToLower()))
                        .Select(pr => new SearchProductViewModel
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = pr.Price,
                            ImageUrl = pr.ImageUrl
                        })
                        .ToList();

                    return resultsfiltredCakes;
                }

                return new List<SearchProductViewModel>();
            }
        }

        public DetailsProductViewModel Find(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products
                    .Where(pr => pr.Id == id)
                    .Select(pr => new DetailsProductViewModel
                    {
                        Name = pr.Name,
                        Price = pr.Price,
                        ImageUrl = pr.ImageUrl
                    })
                    .FirstOrDefault();
            }
        }

        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(pr => pr.Id == id);
            }
        }
        
        public Product FindProductById(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products
                    .Where(pr => pr.Id == id)
                    .Select(pr => new Product
                    {
                        Id = id,
                        Name = pr.Name,
                        Price = pr.Price,
                        ImageUrl = pr.ImageUrl
                    })
                    .FirstOrDefault();
            }
        }       
    }
}
