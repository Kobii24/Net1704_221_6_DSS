using DSS.Business.Business;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSS.RazorWebApp.Pages.ProductPage
{
    public class DetailsModel : PageModel
    {
        private readonly ProductBusiness _business;

        public DetailsModel()
        {
            _business ??= new ProductBusiness();
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _business.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = (Product)product.Data;
            }
            return Page();
        }
    }
}
