using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;
using System.Drawing.Printing;

namespace DSS.RazorWebApp.Pages.ExtraDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly ExtraDiamondBusiness _business;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; } 


        public IndexModel()
        {
            _business ??= new ExtraDiamondBusiness();
        }

        public IList<ExtraDiamond> ExtraDiamond { get;set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                ExtraDiamond = (List<ExtraDiamond>)result.Data;
            }

            if (search == null)
            {
                ExtraDiamond = ExtraDiamond.ToList();
            }
            else
            {
                if (searchBy == "Name")
                {
                    ExtraDiamond = ExtraDiamond.Where(x => x.Name.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else if (searchBy == "Title")
                {
                    ExtraDiamond = ExtraDiamond.Where(x => x.Title.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else
                {
                    foreach(char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                ExtraDiamond = ExtraDiamond.ToList();
                            else
                                ExtraDiamond = ExtraDiamond.Where(x => x.Quantity == Convert.ToInt32(search) /*|| search == null*/).ToList();
                        }catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            ExtraDiamond = ExtraDiamond.ToList();
                        }
                    }             
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(ExtraDiamond.ToList().Count / (double)PageSize);
            ExtraDiamond = ExtraDiamond.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
