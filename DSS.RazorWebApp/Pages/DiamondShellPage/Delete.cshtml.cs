using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.DiamondShellPage
{
    public class DeleteModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public DeleteModel()
        {
            _business ??= new DiamondShellBusiness();
        }


        [BindProperty]
        public DiamondShell DiamondShell { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var diamondshell = await _business.GetById(id);

            if (diamondshell == null)
            {
                return NotFound();
            }
            else
            {
                DiamondShell = (DiamondShell)diamondshell.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var diamondShell = await _business.GetById(id);
            if (diamondShell.Data != null)
            {
                await _business.DeleteById(id);
                await _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
