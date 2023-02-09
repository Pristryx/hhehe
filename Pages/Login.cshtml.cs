using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ASAssignment212344H.Models;
using ASAssignment212344H.Model;

namespace ASAssignment212344H.Pages
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        //private readonly IdentityOptions _identityOptions;
        private readonly IHttpContextAccessor contxt;

        [BindProperty]
        public Login LModel { get; set; }
        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, /*IOptions<IdentityOptions> identityOptions,*/ IHttpContextAccessor contxt)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            //_identityOptions = identityOptions.Value;
            //_identityOptions.Lockout.AllowedForNewUsers = true;
            //_identityOptions.Lockout.MaxFailedAccessAttempts = 3;
            //_identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            this.contxt = contxt;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(LModel.Email);
                    if (user != null)
                    {
                        if (await userManager.IsLockedOutAsync(user))
                        {
                            ModelState.AddModelError("", "Your account is locked out due to multiple failed login attempts.");
                            return Page();
                        }
                    }

                    var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                    LModel.RememberMe, true);
                    if (identityResult.Succeeded)
                    {
                        contxt.HttpContext.Session.SetString("Email", LModel.Email);
                        

                    return RedirectToPage("Index");
                    }

                    ModelState.AddModelError("", "Username or Password incorrect");
                    
                }

                return Page();
            }
    }
}
