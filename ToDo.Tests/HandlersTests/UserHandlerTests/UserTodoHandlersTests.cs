using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Handlers.UserHandlers;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Tests.HandlersTests.UserHandlerTests
{
    [TestFixture]
    public class UserTodoHandlersTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        
        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task AddUserCommandHandlerShouldReturnUserObject()
        {
            var user = new User()
            {
                Password = "Teste",
                Username = "Teste"
            };

            var userCommand = new AddUserCommand(user)
            {
                User  = user
            };
            
            var handler = new AddUserCommandHandler(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(a => a.Add(userCommand.User)).ReturnsAsync(user);
            
            var result = await handler.Handle(userCommand, new CancellationToken());

            result.Should().BeOfType<User>();

        }
        
        [Test]
        public async Task GetUserByIdCommandHandlerShouldReturnUserObject()
        {
            var user = new User()
            {
                Password = "Teste",
                Username = "Teste"
            };

            var userCommand = new GetUserByIdCommand()
            {
                UserId = Guid.NewGuid().ToString(),
            };

            var listUser = new List<User> {user};

            var handler = new GetUserByIdCommandHandler(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(a => a.Search(a=>a.Id == Guid.Parse(userCommand.UserId))).ReturnsAsync(listUser);
            
            var result = await handler.Handle(userCommand, new CancellationToken());

            result.Should().BeOfType<User>();

        }
        
        [Test]
        public async Task GetUserByNameCommandHandlerShouldReturnUserObject()
        {
            var user = new User()
            {
                Password = "Teste",
                Username = "Teste"
            };

            var userCommand = new GetUserByNameCommand("Teste");

            var listUser = new List<User> {user};

            var handler = new GetUserByNameCommandHandler(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(a => a.Search(a=>a.Username == userCommand.Name)).ReturnsAsync(listUser);
            
            var result = await handler.Handle(userCommand, new CancellationToken());

            result.Should().BeOfType<User>();

        }
        [Test]
        public async Task GetUserCommandHandlerShouldReturnUserObject()
        {
            var user = new User()
            {
                Password = "Teste",
                Username = "Teste"
            };

            var userCommand = new GetUserCommand(user);

            var listUser = new List<User> {user};

            var handler = new GetUserCommandHandler(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(a => a.Search(a=>a.Username == userCommand.User.Username && a.Password == userCommand.User.Password)).ReturnsAsync(listUser);
            
            var result = await handler.Handle(userCommand, new CancellationToken());

            result.Should().BeOfType<User>();

        }
        
    }
}