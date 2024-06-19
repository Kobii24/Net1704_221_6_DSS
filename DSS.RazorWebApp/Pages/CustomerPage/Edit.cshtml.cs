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

namespace DSS.RazorWebApp.Pages.NewFolder
{
    public class EditModel : PageModel
    {
        private readonly CustomerBusiness _business;

        public EditModel()
        {
            _business = new CustomerBusiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer =  await _business.GetById(id);
            if (customer.Data == null)
            {
                return NotFound();
            }
            Customer = (Customer)customer.Data;
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

            await _business.Update(Customer);

            try
            {
                await _business.Save(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
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

        private bool CustomerExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
