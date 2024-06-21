using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.NewFolder
{
    public class DeleteModel : PageModel
    {
        private readonly CustomerBusiness _business;

        public DeleteModel()
        {
            _business ??= new CustomerBusiness(); 
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _business.GetById(id);

            if (customer.Data == null)
            {
                return NotFound();
            }
            else
            {
                Customer = (Customer)customer.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var customer = await _business.GetById(id);
            if (customer.Data != null)
            {
                await _business.DeleteById(id);
                await _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
