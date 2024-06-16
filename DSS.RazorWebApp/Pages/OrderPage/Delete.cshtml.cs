using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazerWebApp.Pages.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly Order_Business _business;

        public DeleteModel()
        {
            _business = new Order_Business();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _business.GetById(id);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = (Order) order.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _business.GetById(id);
            if (order != null)
            {
                Order = (Order) order.Data;
                _business.Delete(id);
                _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
