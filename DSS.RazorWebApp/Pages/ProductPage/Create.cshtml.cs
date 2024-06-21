using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using DSS.Data.Models;
using DSS.Business.Business;


namespace DSS.RazorWebApp.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly ProductBusiness _business;

        public CreateModel()
        {
            _business ??= new ProductBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _business.Create(Product);

            return RedirectToPage("./Index");
        }
    }
}
