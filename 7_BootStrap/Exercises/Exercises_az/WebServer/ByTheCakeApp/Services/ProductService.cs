using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.Services
{
    using Data;
    using Data.Models;
    using ViewModels.Products;
    using System.Collections.Generic;
    using System.Linq;
    using WebServer.ByTheCakeApp.ViewModels;

    public class ProductService : IProductService
    {
        //5
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
        //6
        public IEnumerable<SearchProductViewModel> All(string searchTerm)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var resultsQuery = db.Products.ToList()/*.AsQueryable()*/;

                if (!string.IsNullOrEmpty(searchTerm)) // only filtred products
                {
                    var resultsCakes = resultsQuery
                        .Where(pr => pr.Name.ToLower().Contains(searchTerm.ToLower()));

                    return resultsCakes
                    .Select(pr => new SearchProductViewModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Price = pr.Price,
                        ImageUrl = pr.ImageUrl

                    })
                    .ToList();
                }

                return resultsQuery // if field search is empty -> all products visualisiert
                    .Select(pr => new SearchProductViewModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Price = pr.Price,
                        ImageUrl = pr.ImageUrl
                    })
                    .ToList();
            }
        }

        //7
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

        //8
        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(pr => pr.Id == id);
            }
        }
        //8 az
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
