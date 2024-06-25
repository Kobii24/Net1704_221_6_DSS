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
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class EditModel : PageModel
    {
        private readonly  MainDiamondBusiness _mainDiamondbusiness;

        public EditModel()
        {
            _mainDiamondbusiness ??= new MainDiamondBusiness();
        }

        [BindProperty]
        public MainDiamond mainDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maindiamond = await _mainDiamondbusiness.GetById(id);
            if (maindiamond.Data == null)
            {
                return NotFound();
            }
            mainDiamond = (MainDiamond)maindiamond.Data;
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

            _mainDiamondbusiness.Update(mainDiamond);

            try
            {
                await _mainDiamondbusiness.Save(mainDiamond);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainDiamondExist(mainDiamond.MainDiamondId))
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

        private bool MainDiamondExist(int id)
        {
            return (bool)_mainDiamondbusiness.GetById(id).Result.Data;
        }
    }
}
