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
    public class IndexModel : PageModel
    {
        private readonly OrderDetail_Business _business;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }
        public IndexModel()
        {
            _business ??= new OrderDetail_Business();
        }

        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                OrderDetail = (List<OrderDetail>)result.Data;
            }

            if (search == null)
            {
                OrderDetail = OrderDetail.ToList();
            }
            else
            {
                if (searchBy == "OrderId")
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                OrderDetail = OrderDetail.ToList();
                            else
                                OrderDetail = OrderDetail.Where(x => x.OrderId == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            OrderDetail = OrderDetail.ToList();
                        }
                    }
                }
                else if (searchBy == "ProductId")
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                OrderDetail = OrderDetail.ToList();
                            else
                                OrderDetail = OrderDetail.Where(x => x.ProductId == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            OrderDetail = OrderDetail.ToList();
                        }
                    }
                }
                else
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                OrderDetail = OrderDetail.ToList();
                            else
                                OrderDetail = OrderDetail.Where(x => x.Quantity == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            OrderDetail = OrderDetail.ToList();
                        }
                    }
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(OrderDetail.ToList().Count / (double)PageSize);
            OrderDetail = OrderDetail.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
