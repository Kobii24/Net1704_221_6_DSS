using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

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
        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Customer = (List<Customer>)result.Data;
            }
        }
        //public void OnGet(string SearchString)
        //{
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        //Customer = Customer.Where(c => c.Name.Contains(SearchString)).ToList();
        //        Customer = Customer.Where(item => item.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
        //    }
        //}
    }

}


