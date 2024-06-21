using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.DiamondShellPage
{
    public class IndexModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public IndexModel()
        {
            _business ??= new DiamondShellBusiness();
        }

        public IList<DiamondShell> DiamondShell { get;set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                DiamondShell = (List<DiamondShell>)result.Data;
            }
            if (search != null)
            {
                if (searchBy == "Name")
                {
                    DiamondShell = DiamondShell.Where(item => item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if (searchBy == "Origin")
                {
                    DiamondShell = DiamondShell.Where(item => item.Origin.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if (searchBy == "Price")
                {
                    DiamondShell = DiamondShell.Where(item =>
                    {
                        return item.Price == Convert.ToDouble(search); ;
                    }).ToList();
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(DiamondShell.ToList().Count / (double)PageSize);
            DiamondShell = DiamondShell.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
