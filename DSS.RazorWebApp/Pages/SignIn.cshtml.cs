using DSS.Business.Business;
using DSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;

namespace DSS.RazorWebApp.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public Customer customer { get; set; }
        private CustomerBusiness _business;
        private Net1704_221_6_DSSContext _db;
        public string Msg;
        public SignInModel()
        {
            _business ??= new CustomerBusiness();
            _db ??= new Net1704_221_6_DSSContext();
        }
        public void OnGet()
        {
            customer = new Customer();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return Page();
        }
        public IActionResult OnPost()
        {
            var account = login(customer.Email, customer.Password);
            if(account == null)
            {
                Msg = "Invalid";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("username", account.Name);
                return RedirectToPage("Index");
            }
        }

        private Customer login(string userEmail, string password)
        {
            var account = _db.Customers.SingleOrDefault(a => a.Email.Equals(userEmail) && a.Password.Equals(password));
            if(account != null)
            {
                return account;
            }
            return null!;
        }
    }
}
