using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HackingProjekt.Pages.Account
{
    [AllowAnonymous]
    public class AcceseDeniedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
