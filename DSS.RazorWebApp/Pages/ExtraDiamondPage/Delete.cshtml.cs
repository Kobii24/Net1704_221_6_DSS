using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.ExtraDiamondPage
{
    public class DeleteModel : PageModel
    {
        private readonly ExtraDiamondBusiness _business;

        public DeleteModel()
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
            else
            {
                ExtraDiamond = (ExtraDiamond)extradiamond.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extradiamond = await _business.GetById(id);
            if (extradiamond.Data != null)
            {
                ExtraDiamond = (ExtraDiamond)extradiamond.Data;
                _business.DeleteById(ExtraDiamond.ExtraDiamondId);
                _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
