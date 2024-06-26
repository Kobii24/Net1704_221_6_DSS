using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly MainDiamondBusiness _business;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }


        public IndexModel()
        {
            _business ??= new MainDiamondBusiness();
        }

        public IList<MainDiamond> MainDiamond { get; set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                MainDiamond = (List<MainDiamond>)result.Data;
            }

            if (search == null)
            {
                MainDiamond = MainDiamond.ToList();
            }
            else
            {
                if (searchBy == "Name")
                {
                    MainDiamond = MainDiamond.Where(x => x.Name.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else if (searchBy == "Origin")
                {
                    MainDiamond = MainDiamond.Where(x => x.Origin.ToLower().Contains(search.ToLower()) /*|| search == null*/).ToList();
                }
                else
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                MainDiamond = MainDiamond.ToList();
                            else
                                MainDiamond = MainDiamond.Where(x => x.Quantity == Convert.ToInt64(search) /*|| search == null*/).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            MainDiamond = MainDiamond.ToList();
                        }
                    }
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(MainDiamond.ToList().Count / (double)PageSize);
            MainDiamond = MainDiamond.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
