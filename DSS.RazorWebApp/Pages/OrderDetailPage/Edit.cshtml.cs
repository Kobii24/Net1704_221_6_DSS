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

namespace DSS.RazerWebApp.Pages.OrderDetailPage
{
    public class EditModel : PageModel
    {
        private readonly OrderDetail_Business _business;

        public EditModel()
        {
            _business = new OrderDetail_Business();
        }

        [BindProperty]
        public OrderDetail? OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _business.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            OrderDetail = (OrderDetail)orderDetail.Data;
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

            _business.Update(OrderDetail);

            try
            {
                await _business.Save(OrderDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists((int)OrderDetail.OrderId))
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

        private bool OrderExists(int id)
        {
            return (bool)_business.GetById(id).Result.Data;
        }
    }
}
