using Blog.Attributes;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    [ApiKey]
    public class HomeController : ControllerBase
    {
        [Route("")]
        public IActionResult Get() => Ok("Você está aconectado");
        
        
    }
}
