using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyClinic.Interfaces;
using MyClinic.Models;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace MyClinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClient _client;
        public AccountController(IClient client)
        {
            _client = client;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ClientModel client)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ValidationMessage", "Please fill all the form fields");
                return View("Login");
            }

            try
            {
                ClientModel c = await _client.GetClient(client);

                if (c != null)
                {
                    // Creating the security context
                    /*var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, c.Name), // Username (get from the database), hardcoded for now
                        new Claim("TypeUser","Admin")
                    };*/

                    IEnumerable<RoleModel> cr = await _client.GetClientRolesByIDAsync(c.ID);

                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, c.Name)); // Username (get from the database), hardcoded for now
                    claims.Add(new Claim(ClaimTypes.Sid, Convert.ToString(c.ID)));

                    foreach (RoleModel rm in cr)
                    {
                        claims.Add(new Claim("TypeUser", rm.Role));
                    }

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToAction("Index", "Client");
                }
                else
                {
                    ModelState.AddModelError("ValidationMessage", "Wrong E-mail or Password");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ValidationMessage", "Failed to login");
                return View("Login");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index", "Client");
        }
    }
}
