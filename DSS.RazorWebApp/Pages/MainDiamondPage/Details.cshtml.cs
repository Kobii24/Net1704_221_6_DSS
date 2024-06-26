using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class DetailsModel : PageModel
    {
        private readonly MainDiamondBusiness _business;

        public DetailsModel()
        {
            _business ??= new MainDiamondBusiness();
        }

        public MainDiamond MainDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maindiamond = await _business.GetById(id);
            if (maindiamond == null)
            {
                return NotFound();
            }
            else
            {
                MainDiamond = (MainDiamond)maindiamond.Data;
            }
            return Page();
        }
    }
}
