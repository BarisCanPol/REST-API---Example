using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //api/commands
    //hardcoded - "api/[controller]"
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //readonly = sadece constructor da set edilebilir 
        //Dependecy injected start
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //dependency injected end

        //no more necessariy
        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        //get api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            //Success and items
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //Get a api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));

            return NotFound();
        }

        //Post api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto command)
        {
            var commandModel = _mapper.Map<Command>(command);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);

            //return Ok(commandReadDto);
        }

        //Put api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandCreateDto command)
        {
            var record = _repository.GetCommandById(id);

            if(record ==null)
                return NotFound();

            //Farklı yazıldı
            _mapper.Map(command,record);

            _repository.UpdateCommand(record);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument) 
        {

            var record = _repository.GetCommandById(id);

            if(record ==null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(record);    
            patchDocument.ApplyTo(commandToPatch,ModelState);

            if(!TryValidateModel(commandToPatch)){
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch,record);

            _repository.UpdateCommand(record);

            _repository.SaveChanges();

            return NoContent();
        }

        //Delete api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){

             var record = _repository.GetCommandById(id);

            if(record ==null)
                return NotFound();

            _repository.DeleteCommand(record);

            _repository.SaveChanges();

            return NoContent();
        }

    }
}