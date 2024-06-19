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

namespace DSS.RazorWebApp.Pages.NewFolder
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerBusiness _business;

        public DetailsModel()
        {
            _business = new CustomerBusiness();
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _business.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = (Customer)customer.Data;
            }
            return Page();
        }
    }
}
