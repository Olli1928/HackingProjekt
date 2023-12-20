using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HackingProjekt.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;


        [BindProperty]
        public Login Model { get; set; }
        public string ReturnUrl { get; set; }


        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(Model.Email);

                // If user is not found by email, try finding by username
                if (user == null)
                {
                    user = await userManager.FindByNameAsync(Model.Email);
                }

                if (user != null)
                {
                    var identityResult = await signInManager.PasswordSignInAsync(user, Model.Password, false, true);

                    if (identityResult.Succeeded)
                    {
                        // Authentication succeeded
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToPage("/Index");
                    }

                    if (identityResult.IsLockedOut)
                    {
                        // Handle lockout
                        return RedirectToPage("./Lockout");
                    }
                }

                // Authentication failed
                ModelState.AddModelError("", "Email/Brugernavn eller Password er forkert");
            }

            return Page();
        }

    }
    
}
