using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DSS.Data.Models;
using DSS.Business.Business;

namespace DSS.RazorWebApp.Pages.ExtraDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly ExtraDiamondBusiness _business;

        public IndexModel()
        {
            _business ??= new ExtraDiamondBusiness();
        }

        public IList<ExtraDiamond> ExtraDiamond { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if(result != null && result.Status > 0 && result.Data!= null)
            {
                ExtraDiamond = result.Data as List<ExtraDiamond>;
            }
        }
    }
}
