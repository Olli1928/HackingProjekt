using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HackingProjekt.Pages.Account
{
    public class LogoutModel : PageModel
    {

        private readonly SignInManager<IdentityUser> signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync() 
        {
            await signInManager.SignOutAsync();
            return Redirect("~/Index");
        }

        public IActionResult OnPostDontLogoutAsync()
        {
            return Redirect("~/Index");
        }
    }
}
