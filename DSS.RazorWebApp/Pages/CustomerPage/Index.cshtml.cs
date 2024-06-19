﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;
using System.Drawing.Printing;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.NewFolder
{
    public class IndexModel : PageModel
    {
        private readonly CustomerBusiness _business;

        public IndexModel()
        {
            _business ??= new CustomerBusiness();
        }

        public IList<Customer> Customer { get;set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }
        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Customer = (List<Customer>)result.Data;
            }
            if (search != null)
            {
                if (searchBy == "Name")
                {
                    Customer = Customer.Where(item => item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if (searchBy == "Address")
                {
                    Customer = Customer.Where(item => item.Address.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if (searchBy == "Phone")
                {
                    Customer = Customer.Where(item => item.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(Customer.ToList().Count / (double)PageSize);
            Customer = Customer.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }

}


