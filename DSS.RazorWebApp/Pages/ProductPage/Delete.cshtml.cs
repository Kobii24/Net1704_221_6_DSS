using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.ProductPage
{
    public class DeleteModel : PageModel
    {
        private readonly ProductBusiness _business;

        public DeleteModel()
        {
            _business ??= new ProductBusiness();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _business.GetById(id);

            if (product.Data == null)
            {
                return NotFound();
            }
            else
            {
                Product = (Product)product.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _business.GetById(id);
            if (product.Data != null)
            {
                Product = (Product)product.Data;
                _business.DeleteById(Product.ProductId);
                _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
