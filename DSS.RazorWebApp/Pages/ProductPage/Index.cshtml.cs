using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.ProductPage
{
    public class IndexModel : PageModel
    {

        private readonly ProductBusiness _business;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }


        public IndexModel()
        {
            _business ??= new ProductBusiness();
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                Product = (List<Product>)result.Data;
            }

            if (search == null)
            {
                Product = Product.ToList();
            }
            else
            {
                if (searchBy == "Name")
                {
                    Product = Product.Where(x => x.Name.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                /*else if (searchBy == "Size")
                {
                    Product = Product.Where(x => x.Size.ToLower().Contains(search.ToLower()) *//*|| search == null*//*).ToList();
                }*/
                else
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                Product = Product.ToList();
                            else
                                Product = Product.Where(x => x.Quantity == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Product = Product.ToList();
                        }
                    }
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(Product.ToList().Count / (double)PageSize);
            Product = Product.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
