﻿using System;
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
        public IndexModel()
        {
            _business ??= new Order_Business();
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if(result != null && result.Status >0 && result.Data !=null)
            {
                Order = result.Data as List<Order>;
            }
        }
    }
}
