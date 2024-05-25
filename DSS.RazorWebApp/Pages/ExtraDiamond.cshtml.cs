using DSS.Business.Base;
using DSS.Business.Category;
using DSS.Data;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace DSS.RazorWebApp.Pages
{
    public class ExtraDiamondModel : PageModel
    {
        private readonly ExtraDiamondBusiness _extra;
        public List<ExtraDiamond> extraDiamonds;
        public ExtraDiamondModel()
        {
            _extra ??= new ExtraDiamondBusiness();
        }
        public void OnGet()
        {
          IBusinessResult a = _extra.GetAll().Result;
            extraDiamonds = (List<ExtraDiamond>)a.Data;
           
        }
    }
}
