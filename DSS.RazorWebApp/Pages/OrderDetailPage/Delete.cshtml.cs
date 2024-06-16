using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazerWebApp.Pages.OrderDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly OrderDetail_Business _business;

        public DeleteModel()
        {
            _business = new OrderDetail_Business();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

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
            else
            {
                OrderDetail = (OrderDetail)orderDetail.Data;
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
                OrderDetail = (OrderDetail)order.Data;
                _business.Delete(id);
                _business.SaveAll();
            }

            return RedirectToPage("./Index");
        }
    }
}
