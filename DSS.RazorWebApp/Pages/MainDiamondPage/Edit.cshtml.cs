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

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class EditModel : PageModel
    {
        private readonly MainDiamondBusiness _business;

        public EditModel()
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
            MainDiamond = (MainDiamond)maindiamond.Data;
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

            _business.Update(MainDiamond);

            try
            {
                await _business.Save(MainDiamond);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainDiamondExists(MainDiamond.MainDiamondId))
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

        private bool MainDiamondExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
