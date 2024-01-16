using HackingProjekt.Migrations;
using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HackingProjekt.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register Re { get; set; }
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        
        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = Re.Email,
                    Email = Re.Email
                };

               var result = await UserManager.CreateAsync(user, Re.Password);
                if( result.Succeeded)
                {
                     //giver bruger Admin rolle, (Udkomenter for at give næste bruger admin rolle.                    
                    // await UserManager.AddToRoleAsync(user, "Admin");

                    await SignInManager.SignInAsync(user, false);
                    
                    return RedirectToPage("/Account/editUsername");
                }

                foreach ( var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
        
            }
            return Page();
        }
    }
}
