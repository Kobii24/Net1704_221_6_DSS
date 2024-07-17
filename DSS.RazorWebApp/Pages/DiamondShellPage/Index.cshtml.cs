using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.Temp
{
    public class IndexModel : PageModel
    {
        private readonly DiamondShellBusiness _business;

        public IndexModel()
        {
            _business ??= new DiamondShellBusiness();
        }

        public IList<DiamondShell> DiamondShell { get; set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchName, string searchMaterial, string searchGender, string searchOrigin, string searchPrice, string searchComplexibility, int? pageNumber)
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                DiamondShell = (List<DiamondShell>)result.Data;
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                DiamondShell = DiamondShell.Where(item => item.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchMaterial))
            {
                DiamondShell = DiamondShell.Where(item => item.Material.Contains(searchMaterial, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchGender))
            {
                DiamondShell = DiamondShell.Where(item => item.Gender.Contains(searchGender, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchOrigin))
            {
                DiamondShell = DiamondShell.Where(item => item.Origin.Contains(searchOrigin, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchComplexibility))
            {
                DiamondShell = DiamondShell.Where(item => item.Complexibility.Contains(searchComplexibility, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(DiamondShell.Count / (double)PageSize);
            DiamondShell = DiamondShell.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
