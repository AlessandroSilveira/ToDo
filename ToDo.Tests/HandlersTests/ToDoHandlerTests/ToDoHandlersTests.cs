using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Entities;
using ToDo.Domain.Handlers.ToDoHandles;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Tests.HandlersTests.ToDoHandlerTests
{
    [TestFixture]
    public class ToDoHandlersTests
    {
        private Mock<ITodoRepository> _toDoRepositoryMock;
        private Mock<IMediator> _mediatorMock;
        private List<TodoItem> listToDoItem;

        [SetUp]
        public void SetUp()
        {
            _toDoRepositoryMock = new Mock<ITodoRepository>();
            _mediatorMock = new Mock<IMediator>();
            
            listToDoItem = new List<TodoItem>
            {
                new TodoItem("teste",true, DateTime.Now, "Teste"),
            };
        }

        [Test]
        public async Task CreateTdDoCommandHandlerTestShouldReturnGenericCommandResultObject()
        {
            var todo = new TodoItem("Teste", false, DateTime.Now, "Teste");
            var command = new CreateTodoCommand();
            var handler = new CreateTodoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Add(todo)).ReturnsAsync(todo);

            //Act
            var result = await ((IRequestHandler<CreateTodoCommand, GenericCommandResult>) handler).Handle(command, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa salva"); 
        }
        
        [Test]
        public async Task GetAllDoneToDoCommandHandlerTestShouldReturnGetAllDoneToDoCommandObject()
        {
            var command = new GetAllDoneToDoCommand
            {
                User = "Teste"
            };

            var handler = new GetAllDoneToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a => a.User == command.User && a.Done == true )).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
           
        }
        
        [Test]
        public async Task GetAllToDoCommandHandlerTestShouldReturnGetAllToDoCommandObject()
        {
            var command = new GetAllToDoCommand
            {
                User = "Teste"
            };

           var handler = new GetAllToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User)).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
           
        }
        [Test]
        public async Task GetAllUndoneToDoCommandHandlerTestShouldReturnGetAllUndoneToDoCommandObject()
        {
            var command = new GetAllUndoneToDoCommand
            {
                User = "Teste"
            };

            var handler = new GetAllUndoneToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User && a.Done == false)).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
           
        }
        
        [Test]
        public async Task GetDoneForTodayToDoCommandHandlerTestShouldReturnGetDoneForTodayToDoCommandObject()
        {
            var command = new GetDoneForTodayToDoCommand
            {
                User = "Teste"
            };

            var handler = new GetDoneForTodayToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User && a.Done == true && a.Date == DateTime.Now.Date)).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
           
        }
        
        [Test]
        public async Task GetDoneForTomorrowToDoCommandHandlerTestShouldReturnGetDoneForTomorrowToDoCommandObject()
        {
            var command = new GetDoneForTomorrowToDoCommand
            {
                User = "Teste"
            };

           
            var handler = new GetDoneForTomorrowToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User && a.Done == true && a.Date == DateTime.Now.AddDays(1))).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
           
        }
        
        [Test]
        public async Task GetUndoneForTodayToDoCommandHandlerTestShouldReturnGetUndoneForTodayToDoCommandObject()
        {
            var command = new GetUndoneForTodayToDoCommand
            {
                User = "Teste"
            };

            var handler = new GetUndoneForTodayToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User && a.Done == false && a.Date == DateTime.Now.Date)).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
        }
        
        [Test]
        public async Task GetUndoneForTomorrowToDoCommandHandlerTestShouldReturnGetUndoneForTomorrowToDoCommandObject()
        {
            var command = new GetUndoneForTomorrowToDoCommand
            {
                User = "Teste"
            };

            var handler = new GetUndoneForTomorrowToDoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a => a.Search(a=>a.User == command.User && a.Done == false && a.Date == DateTime.Now.AddDays(1))).ReturnsAsync(listToDoItem);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Should().NotBeEmpty();
        }
        
        
        [Test]
        public async Task MarkToDoAsDoneCommandHandlerTestShouldReturnGenericCommandResultObject()
        {
            var command = new MarkTodoAsDoneCommand
            {
                Id = Guid.NewGuid(),
                User = "Teste"
            };

            var todo = new TodoItem("Teste", true, DateTime.Now, "Teste");
            
            var handler = new MarkTodoAsDoneCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a=>a.GetById(command.Id)).ReturnsAsync(todo);
            _toDoRepositoryMock.Setup(a => a.Update(todo)).ReturnsAsync(todo);
    
            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa salva");
        }
        
        [Test]
        public async Task MarkToDoAsUndoneCommandHandlerTestShouldReturnGenericCommandResultObject()
        {
            var command = new MarkTodoAsUndoneCommand
            {
                Id = Guid.NewGuid(),
                User = "Teste"
            };

            var todo = new TodoItem("Teste", true, DateTime.Now, "Teste");
            
            var handler = new MarkTodoAsUndoneCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a=>a.GetById(command.Id)).ReturnsAsync(todo);
            _toDoRepositoryMock.Setup(a => a.Update(todo)).ReturnsAsync(todo);
    
            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa salva");
        }
        
        [Test]
        public async Task UpdateTodoCommandHandlerTestShouldReturnGenericCommandResultObject()
        {
            var command = new UpdateTodoCommand(Guid.NewGuid(), "Teste", "Teste");

            var todo = new TodoItem("Teste", true, DateTime.Now, "Teste");
            
            var handler = new UpdateTodoCommandHandler(_toDoRepositoryMock.Object);
            _toDoRepositoryMock.Setup(a=>a.GetById(command.Id)).ReturnsAsync(todo);
            _toDoRepositoryMock.Setup(a => a.Update(todo)).ReturnsAsync(todo);
    
            //Act
            var result = await handler.Handle(command, new CancellationToken());

            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa salva");
        }
    }
}