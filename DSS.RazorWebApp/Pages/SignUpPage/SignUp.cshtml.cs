using DSS.Business.Business;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSS.RazorWebApp.Pages.SignUpPage
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public Customer customer { get; set; }
        private readonly CustomerBusiness _business;
        public SignUpModel()
        {
            _business ??= new CustomerBusiness();
        }
        public void OnGet()
        {
            customer = new Customer();
        }
        public IActionResult OnPost()
        {
            //customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            _business.Create(customer);
            _business.SaveAll();
            return RedirectToPage("/SignIn");
        }
    }
}
