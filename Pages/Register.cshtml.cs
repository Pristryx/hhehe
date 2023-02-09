using ASAssignment212344H.Models;
using ASAssignment212344H.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.DataProtection;

namespace ASAssignment212344H.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpContextAccessor contxt;
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; } = new();

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        IHttpContextAccessor contxt)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contxt = contxt;
        }
        public void OnGet()
        {
            //Email: kshent@gmail.com
            //Password: Kshentamazing1!
        }
        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    NRIC = protector.Protect(RModel.NRIC),
                    Resume = RModel.Resume,
                    WhoamI = RModel.WhoamI,
                    DateOfBirth = RModel.DateOfBirth,
                    Gender = RModel.Gender
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    contxt.HttpContext.Response.Cookies.Append("logged-in-browser", "true", new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30)
                    });

                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
