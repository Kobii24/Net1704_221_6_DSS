using DSS.Business.Business;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DSS.RazorWebApp.Pages.ProductPage
{
    public class EditModel : PageModel
    {
        private readonly ProductBusiness _business;

        public EditModel()
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
            Product = (Product)product.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _business.Update(Product);

            try
            {
                await _business.Save(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
