
namespace WebServer.ByTheCakeApp.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using ViewModels;
    using System;
    using System.Linq;
    using ViewModels.Products;
    using WebServer.ByTheCakeApp.Services;
    using WebServer.Server.Http.Response;

    public class ProductsController : Controller
    {
        private readonly IProductService products;

        public ProductsController()
        {
            this.products = new ProductService();
        }
        //5
        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";
            this.ViewData["showError"] = "none";

            return this.FileViewResponse(@"Products/add");
        }
        //5
        public IHttpResponse Add(AddProductViewModel model)
        {
            if (model.Name.Length < 3
                || model.Name.Length > 30
                || model.ImageUrl.Length < 3
                || model.ImageUrl.Length > 2000)
            {                
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Product information is not valid";

                return this.FileViewResponse(@"Products/add");
            }

            this.products.Create(model.Name, model.Price, model.ImageUrl);

            this.ViewData["showError"] = "none";
            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = model.Price.ToString();
            this.ViewData["imageUrl"] = model.ImageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"Products/add");
        }
        //----------------------------------------------------------------------
        //6
        public IHttpResponse Search(IHttpRequest req)
        {
            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;

            var searchTerm = urlParameters.ContainsKey("searchTerm")
                ? urlParameters["searchTerm"]
                : null;

            this.ViewData["searchTerm"] = searchTerm; //only to stay the filter in searchbox - used together with "valueЧ from the form in search.html 

            var result = this.products.All(searchTerm);//return or all Cakes or only filtred Cakes

            if (!result.Any())
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"
                      <div><a href=""/cakeDetails/{c.Id}"">{c.Name}</a>  -  
                      ${c.Price:F2}  -  
                      <img src=""{c.ImageUrl}"" width=""40"" height=""40"" />  -  
                      <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                var allProductsAsString = string.Join(Environment.NewLine, allProducts);

                this.ViewData["results"] = allProductsAsString;
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"Products/search");
        }

        //7
        public IHttpResponse Details(int id)
        {
            var product = this.products.Find(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["imageUrl"] = product.ImageUrl;

            return this.FileViewResponse(@"Products/details");
        }
    }
}
