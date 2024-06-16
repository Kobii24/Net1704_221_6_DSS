using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;

namespace DSS.RazerWebApp.Pages.OrderDetailPage
{
    public class DetailsModel : PageModel
    {
        private readonly DSS.Data.Models.Net1704_221_6_DSSContext _context;

        public DetailsModel(DSS.Data.Models.Net1704_221_6_DSSContext context)
        {
            _context = context;
        }

        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}
