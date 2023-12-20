using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HackingProjekt.Pages.Account
{
    public class editUsernameModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public editUsernameModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string NewUsername { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    // Check if the new username is available
                    var existingUser = await _userManager.FindByNameAsync(NewUsername);
                    if (existingUser == null)
                    {
                        // Update the username
                        user.UserName = NewUsername;
                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            // Redirect to a success page or home page
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to update username.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("NewUsername", "Username is already taken.");
                    }
                }
                else
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
            }

            // If we're here, there are validation errors
            return Page();
        }
    }
}
