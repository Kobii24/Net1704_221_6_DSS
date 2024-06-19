using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.DiamondShellPage
{
    public class EditModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public EditModel()
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
            DiamondShell = (DiamondShell)diamondshell.Data;
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

            await _business.Update(DiamondShell);

            try
            {
                await _business.Save(DiamondShell);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiamondShellExists(DiamondShell.DiamondShellId))
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

        private bool DiamondShellExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
