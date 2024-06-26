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
    public class DeleteModel : PageModel
    {
        private readonly MainDiamondBusiness _business;

        public DeleteModel()
        {
            _business ??= new MainDiamondBusiness();
        }

        [BindProperty]
        public MainDiamond MainDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maindiamond = await _business.GetById(id);

            if (maindiamond.Data == null)
            {
                return NotFound();
            }
            else
            {
                MainDiamond = (MainDiamond)maindiamond.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maindiamond = await _business.GetById(id);
            if (maindiamond.Data != null)
            {
                MainDiamond = (MainDiamond)maindiamond.Data;
                _business.DeleteById(MainDiamond.MainDiamondId);
                _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
