using AuthorInstitution_XXXXXXXX.ViewModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace AuthorInstitution_XXXXXXXX.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public LoginVM LoginVM { get; set; }

        private readonly ServiceBase<MemberAccount> _serviceBase;

        public IndexModel()
        {
            _serviceBase = new ServiceBase<MemberAccount>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _serviceBase.GetAll().FirstOrDefault(c => c.MemberId == LoginVM.Username && c.MemberPassword == LoginVM.Password && c.MemberRole == 1);
            if (customer == null)
            {
                ModelState.AddModelError("LoginVM.Password", "You do not have permission to do this function!");
                return Page();
            }

            return RedirectToPage("/Author/Index");
        }
    }
}