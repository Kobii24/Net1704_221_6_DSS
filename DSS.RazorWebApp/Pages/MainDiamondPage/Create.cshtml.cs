using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DSS.Data.Models;
using DSS.Business.Category;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class CreateModel : PageModel
    {
        private readonly MainDiamondBusiness _mainDiamondBusiness;
        public CreateModel()
        {
            _mainDiamondBusiness = new MainDiamondBusiness();
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public MainDiamond mainDiamond { get; set; } = default;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _mainDiamondBusiness.Create(mainDiamond);

            return RedirectToPage("./Index");
        }
    }
}
