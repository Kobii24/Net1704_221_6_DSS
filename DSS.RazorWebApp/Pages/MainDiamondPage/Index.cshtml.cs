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
using DSS.Business.Category;

namespace DSS.RazorWebApp.Pages.MainDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly MainDiamondBusiness _mainDiamondBusiness;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }


        public IndexModel()
        {
            _mainDiamondBusiness ??= new MainDiamondBusiness();
        }

        public IList<MainDiamond> mainDiamonds { get; set; } = default!;

        public async Task OnGetAsync(string searchBy, string search, int? pageNumber)
        {
            var result = await _mainDiamondBusiness.GetAll();

            if (result != null && result.Status > 0 && result.Data != null/*!string.IsNullOrEmpty(search)*/)
            {
                mainDiamonds = (List<MainDiamond>)result.Data;
            }

            if (search == null)
            {
                mainDiamonds = mainDiamonds.ToList();
            }
            else
            {
                if (searchBy == "Name")
                {
                    mainDiamonds = mainDiamonds.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                else if (searchBy == "Origin")
                {
                    mainDiamonds = mainDiamonds.Where(x => x.Origin.ToLower().Contains(search.ToLower())).ToList();
                }
                else if (searchBy == "Clarity")
                {
                    mainDiamonds = mainDiamonds.Where(x => x.Clarity.ToLower().Contains(search.ToLower())).ToList();
                }
                else if (searchBy == "Color")
                {
                    mainDiamonds = mainDiamonds.Where(x => x.Color.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    foreach (char c in search)
                    {
                        try
                        {
                            if (!char.IsDigit(c))
                                mainDiamonds = mainDiamonds.ToList();
                            else
                                mainDiamonds = mainDiamonds.Where(x => x.Quantity == Convert.ToInt32(search)).ToList();
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                            mainDiamonds = mainDiamonds.ToList();
                        }
                    }
                }
            }
            PageNumber = pageNumber ?? 1;
            TotalPages = (int)System.Math.Ceiling(mainDiamonds.ToList().Count / (double)PageSize);
            mainDiamonds = mainDiamonds.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}