using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandApiRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandApiRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _commandRepo.GetAllCommands();
            
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            Command command = _commandRepo.GetCommandById(id);
            
            if (command == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto dto)
        {
            var command = _mapper.Map<Command>(dto);
            _commandRepo.CreateCommand(command);
            _commandRepo.SaveChanges();

            var readDto = _mapper.Map<CommandReadDto>(command);
                
            return CreatedAtRoute(nameof(GetCommandById), new {Id = readDto.Id}, readDto);
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto dto)
        {
            Command commandFromRepo = _commandRepo.GetCommandById(id);

            if (commandFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, commandFromRepo);
            
            _commandRepo.UpdateCommand(commandFromRepo);
            _commandRepo.SaveChanges();

            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _commandRepo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);
            
            _commandRepo.UpdateCommand(commandModelFromRepo);

            _commandRepo.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommand(int id)
        {
            Command command = _commandRepo.GetCommandById(id);

            if (command == null)
            {
                NotFound();
            }
            
            _commandRepo.DeleteCommand(command);
            _commandRepo.SaveChanges();

            return NoContent();
        }
    }
}