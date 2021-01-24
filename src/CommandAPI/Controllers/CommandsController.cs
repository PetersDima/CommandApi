using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandApiRepo _commandRepo;

        public CommandsController(ICommandApiRepo commandRepo)
        {
            _commandRepo = commandRepo;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Command>> Index()
        {
            return Ok(_commandRepo.GetAllCommands());
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            Command command = _commandRepo.GetCommandById(id);
            
            if (command == null)
            {
                return NotFound();
            }
            
            return Ok(_commandRepo.GetCommandById(id));
        }
    }
}