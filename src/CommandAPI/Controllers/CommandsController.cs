using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return new JsonResult(new {Name = "Dima", Message = "Hello world!"});
        }
        
        [HttpGet("{name}")]
        public ActionResult GreetingsByName(string name)
        {
            return new JsonResult(new {Name = name, Message = $"Hello {name}!"});
        }
    }
}