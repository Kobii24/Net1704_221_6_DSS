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
    public class IndexModel : PageModel
    {
        private readonly Order_Business _business;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }
        public IndexModel()
        {
            _business ??= new Order_Business();
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                Order = (List<Order>)result.Data;
            }

            if (search == null)
            {
                Order = Order.ToList();
            }
            else
            {
                if (searchBy == "PaymentMethod")
                {
                    Order = Order.Where(x => x.PaymentMethod.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else if (searchBy == "PaymentStatus")
                {
                    Order = Order.Where(x => x.PaymentStatus.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                Order = Order.ToList();
                            else
                                Order = Order.Where(x => x.CustomerId == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Order = Order.ToList<Order>();
                        }
                    }
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(Order.ToList().Count / (double)PageSize);
            Order = Order.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
