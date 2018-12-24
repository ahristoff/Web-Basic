
namespace WebServer.ByTheCakeApp.Services
{
    using System.Collections.Generic;
    using ViewModels.Products;
    using WebServer.ByTheCakeApp.Data.Models;

    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<SearchProductViewModel> All(string searchTerm = null);

        DetailsProductViewModel Find(int id);
        //8
        bool Exists(int id);
        //8 az
        Product FindProductById(int id);       
    }
}
