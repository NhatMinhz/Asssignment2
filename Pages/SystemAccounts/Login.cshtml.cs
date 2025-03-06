using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Service.Interfaces;
using System.Security.Claims;

namespace FUNewsManagementSystem.Pages.SystemAccounts
{
    public class LoginModel : PageModel
    {
        private ISystemAccountService _service;

        public LoginModel(ISystemAccountService service)
        {
            _service = service;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var adminEmail = "Admin@gmail.com";
            var adminPassword = "123";

            // Check Admin Credentials from Configuration
            if (Email == adminEmail && Password == adminPassword)
            {
                return await SignInUser("Admin", "Admin");
            }

            // Check User Credentials from Service
            var user = (await _service.GetAllAccountsAsync())
                .FirstOrDefault(u => u.AccountEmail == Email && u.AccountPassword == Password);

            if (user != null)
            {
                var roleName = user.AccountRole switch
                {
                    1 => "Staff",
                    2 => "Lecturer",
                    _ => "User"
                };

                return await SignInUser(user.AccountName, roleName, user.AccountId);
            }

            ViewData["Error"] = "Invalid username or password";
            return Page();
        }

        private async Task<IActionResult> SignInUser(string userName, string role, short? accountId = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            if (accountId.HasValue)
            {
                claims.Add(new Claim("AccountId", accountId.Value.ToString()));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = false });

            return RedirectToPage("/Index");
        }
    }
}
