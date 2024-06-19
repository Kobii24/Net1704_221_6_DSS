using DSS.Business.Base;
using DSS.Business.Business;
using DSS.Data;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace DSS.RazorWebApp.Pages
{
    public class ExtraDiamondModel : PageModel
    {
        private readonly IExtraDiamondBusiness _extraDiamondBusiness = new ExtraDiamondBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public ExtraDiamond ExtraDiamond { get; set; } = default;
        public List<ExtraDiamond> ExtraDiamonds { get; set; } = new List<ExtraDiamond>();

        public void OnGet()
        {
            ExtraDiamonds = this.GetExtraDiamonds();
        }

        public void OnPost()
        {
            this.SaveCurrency();
        }

        public void OnDelete()
        {
        }


        private List<ExtraDiamond> GetExtraDiamonds()
        {
            var extraDiamondResult = _extraDiamondBusiness.GetAll();

            if (extraDiamondResult.Status > 0 && extraDiamondResult.Result.Data != null)
            {
                var currencies = (List<ExtraDiamond>)extraDiamondResult.Result.Data;
                return currencies;
            }
            return new List<ExtraDiamond>();
        }

        private void SaveCurrency()
        {
            var ExtraDiamondResult = _extraDiamondBusiness.Save(this.ExtraDiamond);

            if (ExtraDiamondResult != null)
            {
                this.Message = ExtraDiamondResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

    }
}
