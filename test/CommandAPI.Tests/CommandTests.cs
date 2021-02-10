using System;
using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        private Command _testCommand;
        
        public CommandTests()
        {
            //Arrange
            _testCommand = new Command
            {
                HowTo = "Do some awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        
        [Fact]
        public void CanChangeHowTo()
        {
            //Act
            _testCommand.HowTo = "Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests", _testCommand.HowTo);
        }
        
        [Fact]
        public void CanChangePlatform()
        {
            //Act
            _testCommand.Platform = ".Net Core";

            //Assert
            Assert.Equal(".Net Core", _testCommand.Platform);
        }
        
        [Fact]
        public void CanChangeCommandLine()
        {
            //Act
            _testCommand.CommandLine = "dotnet run";

            //Assert
            Assert.Equal("dotnet run", _testCommand.CommandLine);
        }

        public void Dispose()
        {
            _testCommand = null;
        }
    }
}