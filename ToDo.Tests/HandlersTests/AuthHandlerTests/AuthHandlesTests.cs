using System;
using System.Threading;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Handlers.AuthHandlers;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Tests.HandlersTests.AuthHandlerTests
{
    [TestFixture]
    public class AuthHandlesTests
    {
        private Mock<IRefreshTokenCacheRepository> _refreshTokenCacheRepositoryMock;
        private Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        

        [SetUp]
        public void Setup()
        {
            _refreshTokenCacheRepositoryMock = new Mock<IRefreshTokenCacheRepository>();
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
        }

        [Test]
        public void AddRefreshTokenCommandHandlerShouldReturnRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                UserName = "Teste",
                ExpireAt = DateTime.Now.AddDays(1),
                TokenString = Guid.NewGuid().ToString()
            };
            var command = new AddRefreshTokenCommand(refreshToken);
            var handler = new AddRefreshTokenCommandHandler(_refreshTokenCacheRepositoryMock.Object);
            
            _refreshTokenCacheRepositoryMock.Setup(a => a.Add(refreshToken));

            var result = handler.Handle(command, new CancellationToken());

            result.Should().Equals(refreshToken.TokenString);
        }
        
        
        [Test]
        public void GetRefreshTokenCommandHandlerShouldReturnRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                UserName = "Teste",
                ExpireAt = DateTime.Now.AddDays(1),
                TokenString = Guid.NewGuid().ToString()
            };
            var command = new GetRefreshTokenCommand(refreshToken.TokenString);
            var handler = new GetRefreshTokenCommandHandler(_refreshTokenCacheRepositoryMock.Object);
            
            _refreshTokenCacheRepositoryMock.Setup(a => a.GetById(refreshToken.TokenString));

            var result = handler.Handle(command, new CancellationToken());

            result.Should().Equals(refreshToken.TokenString);
        }
        
        // [Test]
        // public void UpdateRefreshTokenCommandHandlerShouldReturnRefreshToken()
        // {
        //     var refreshToken = new RefreshToken()
        //     {
        //         UserName = "Teste",
        //         ExpireAt = DateTime.Now.AddDays(1),
        //         TokenString = Guid.NewGuid().ToString()
        //     };
        //     var command = new UpdateRefreskTokenCommand(refreshToken);
        //     var handler = new UpdateRefreshTokenCommandHandler(_refreshTokenRepositoryMock.Object);
        //    
        //     var result = handler.Handle(command, new CancellationToken());
        //
        //     result.Should().Equals(refreshToken.TokenString);
        // }
    }
}