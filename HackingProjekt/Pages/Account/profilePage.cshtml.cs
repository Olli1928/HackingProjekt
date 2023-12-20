using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HackingProjekt.Pages.Account
{
    [Authorize]
    public class profilePageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public profilePageModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        [BindProperty]
        public bool IsEighteenPlus { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostToggleAdultClaimDelete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
              
              
                
                    await _userManager.RemoveClaimAsync(user, new Claim("15+", "true"));
                

                // Sign in again to refresh the user's claims
                await _signInManager.RefreshSignInAsync(user);


            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleAdultClaimAdd()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Toggle the +18 claim based on the checkbox state
                
                
                await _userManager.AddClaimAsync(user, new Claim("15+", "true"));
                
                

                // Sign in again to refresh the user's claims
                await _signInManager.RefreshSignInAsync(user);


            }

            return RedirectToPage();
        }
    }
}
