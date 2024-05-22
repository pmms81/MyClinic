using Microsoft.AspNetCore.Mvc;

namespace MyClinic.Controllers
{
    
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode == 404 && statusCode.HasValue)
                return View("NotFound");
            else
                return View("GenError");
        }
    }
}
