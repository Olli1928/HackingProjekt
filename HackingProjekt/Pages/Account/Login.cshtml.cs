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
            {   // Finder bruger med Email
                var user = await userManager.FindByEmailAsync(Model.Email);

                // Finder bruger med BrugerNavn
                if (user == null)
                {
                    user = await userManager.FindByNameAsync(Model.Email);
                }

                if (user != null)
                {
                    var identityResult = await signInManager.PasswordSignInAsync(user, Model.Password, false, true);

                    if (identityResult.Succeeded)
                    {
                        // Authentication lykkes 
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToPage("/Index");
                    }
                    //Håndterer og sender bruger til lockout siden 
                    if (identityResult.IsLockedOut)
                    {
                        
                        return RedirectToPage("./Lockout");
                    }
                }

                // Authentication fejlet 
                ModelState.AddModelError("", "Email/Brugernavn eller Password er forkert");
            }

            return Page();
        }

    }
    
}
