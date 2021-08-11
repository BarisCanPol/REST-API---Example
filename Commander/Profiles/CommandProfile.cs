using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles{
    public class CommandProfile:Profile
    {
        public CommandProfile()
        {
            //Source => Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto,Command>();
            CreateMap<Command,CommandUpdateDto>();
        }
    }
}