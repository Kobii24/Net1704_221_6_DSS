using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class DetailsModel : PageModel
    {
        private readonly MainDiamondBusiness _mainDiamondBusiness;

        public DetailsModel()
        {
            _mainDiamondBusiness ??= new MainDiamondBusiness();
        }

        public MainDiamond mainDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maindiamond = await _mainDiamondBusiness.GetById(id);
            if (mainDiamond == null)
            {
                return NotFound();
            }
            else
            {
                mainDiamond = (MainDiamond)maindiamond.Data;
            }
            return Page();
        }
    }
}
