using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0,HowTo="Boil an egg",Line="Boil water",Platform="Kettle & Pan"},
                new Command{Id=1,HowTo="Cu Bread",Line="Get a knife",Platform="Knife & board"},
                new Command{Id=2,HowTo="Make cup of tea",Line="Place teabag in cup",Platform="Kettle & cuo"},
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0,HowTo="Boil an egg",Line="Boil water",Platform="Kettle & Pan"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}