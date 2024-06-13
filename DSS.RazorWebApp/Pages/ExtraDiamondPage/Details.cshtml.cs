using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.ExtraDiamondPage
{
    public class DetailsModel : PageModel
    {
        private readonly ExtraDiamondBusiness _business;

        public DetailsModel()
        {
            _business ??= new ExtraDiamondBusiness();
        }

        public ExtraDiamond ExtraDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extradiamond = await _business.GetById(id);
            if (extradiamond == null)
            {
                return NotFound();
            }
            else
            {
                ExtraDiamond = (ExtraDiamond)extradiamond.Data;
            }
            return Page();
        }
    }
}
