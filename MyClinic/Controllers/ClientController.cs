using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyClinic.Interfaces;
using MyClinic.Models;
using System.Security.Claims;

namespace MyClinic.Controllers
{
    //[Authorize]
    // New Comment
    public class ClientController : Controller
    {
        private readonly IClient _client;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClient client, IEmailSender emailSender, ILogger<ClientController> logger)
        {
            this._client = client;
            this._emailSender = emailSender;
            this._logger = logger;

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ClientModel> clients;

            if (User.HasClaim("TypeUser","Admin"))
            {
                _logger.LogInformation("User have Admin claim");
                clients = await _client.GetAllClient();
                return View(clients);
            }
            else
            {
                int id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                _logger.LogInformation("User don´t have Admin claim: UserID: {id}", id);
                clients = new[] { await _client.GetClientByIDAsync(id)};
                return View(clients);
            }

        }

        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            ClientModel client = await _client.GetClientByIDAsync(id);
            
            if(client != null)
                return View(client);
            else {
                return StatusCode(404); /*View("Error/404")*/
            }
        }

        //[Authorize(Policy = "AdminProfile")]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ClientModel client)
        {
            // Verify if all model attributes were passed trough the request
            // This can be done with model validation
            if(!ModelState.IsValid)
            {
                return View(client);
            }

            client.Password = new PasswordHasher<object?>().HashPassword(null, client.Password);

            _client.Add(client);

            await _emailSender.SendEmailAsync("pmms81@gmail.com", "IMPORTANT", "Hello there!");

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ClientModel client = await _client.GetClientByIDAsync(id);

            if (client != null)
                return View(client);
            else
            {
                return StatusCode(404); //View("Error/404")
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ClientModel client)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit client");
                return View("Index");
            }

            try
            {
                _client.Update(client);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to edit client");
                return View("Index");
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Task<ClientModel> _c =  _client.GetClientByIDAsync(id);
                
                if (_client.Delete(_c.Result))
                {
                    //int id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    ModelState.AddModelError("ValidationMessage","Failed to delete client");
                    return View("Index");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ValidationMessage", "Failed to delete client");
                return View("Index");
            }
        }
    }
}
