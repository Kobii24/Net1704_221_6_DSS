using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.ExtraDiamondPage
{
    public class EditModel : PageModel
    {
        private readonly ExtraDiamondBusiness _business;

        public EditModel()
        {
            _business ??= new ExtraDiamondBusiness();
        }

        [BindProperty]
        public ExtraDiamond ExtraDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extradiamond = await _business.GetById(id);
            if (extradiamond.Data == null)
            {
                return NotFound();
            }
            ExtraDiamond = (ExtraDiamond)extradiamond.Data;
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

            _business.Update(ExtraDiamond);

            try
            {
                await _business.Save(ExtraDiamond);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtraDiamondExists(ExtraDiamond.ExtraDiamondId))
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

        private bool ExtraDiamondExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
