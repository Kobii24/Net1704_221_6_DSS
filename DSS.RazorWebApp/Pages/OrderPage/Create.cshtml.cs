using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazerWebApp.Pages.OrderPage
{
    public class CreateModel : PageModel
    {
        private readonly Order_Business _business;

        public CreateModel()
        {
            _business = new Order_Business();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _business.Create(Order);

            return RedirectToPage("./Index");
        }
    }
}
