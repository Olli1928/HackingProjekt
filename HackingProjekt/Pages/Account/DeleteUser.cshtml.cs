using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HackingProjekt.Pages.Account
{

    [ValidateAntiForgeryToken]
    [Authorize]
        public class DeleteModel : PageModel
        {
            private readonly UserManager<IdentityUser> _userManager;

            public DeleteModel(UserManager<IdentityUser> context)
            {
                _userManager = context;
            }

            [BindProperty]
            public IdentityUser User { get; set; } = default!;

            public async Task<IActionResult> OnGetAsync(string? id)
            {
                if (id == null || _userManager.Users == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    User = user;
                }
                return Page();
            }

        
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    
                    return RedirectToPage("./Admin");
                }
                else
                {
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    
                    return Page();
                }
            }
            else
            {
                
                return NotFound();
            }
        }
    }
    }

