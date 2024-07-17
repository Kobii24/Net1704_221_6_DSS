using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.Temp
{
    public class CreateModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public CreateModel()
        {
            _business ??= new DiamondShellBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DiamondShell DiamondShell { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DiamondShell.Status = true;
            await _business.Create(DiamondShell);

            return RedirectToPage("./Index");
        }
    }
}
