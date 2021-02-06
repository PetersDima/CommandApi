﻿using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class CommandsRepo: ICommandApiRepo
    {
        private CommandContext _context;

        public CommandsRepo(CommandContext context)
        {
            _context = context;
        }
        
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public void CreateCommand(Command cmd)
        {
            _context.CommandItems.Add(cmd);

            SaveChanges();
        }

        public void UpdateCommand(Command cmd)
        {
            var command = _context.CommandItems.FirstOrDefault(p => p.Id == cmd.Id);

            if (command == null)
            {
                return;
            }

            command.CommandLine = cmd.CommandLine;
            command.HowTo = cmd.HowTo;
            command.Platform = cmd.Platform;

            _context.SaveChanges();
        }

        public void DeleteCommand(Command cmd)
        {
            _context.CommandItems.Remove(cmd);

            SaveChanges();
        }
    }
}