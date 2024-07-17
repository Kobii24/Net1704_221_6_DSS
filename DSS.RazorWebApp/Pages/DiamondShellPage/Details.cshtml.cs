using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.Temp
{
    public class DetailsModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public DetailsModel()
        {
            _business ??= new DiamondShellBusiness();
        }


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
    }
}
