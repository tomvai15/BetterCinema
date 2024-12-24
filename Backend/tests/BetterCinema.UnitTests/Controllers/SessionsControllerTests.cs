using AutoFixture;
using AutoMapper;
using BetterCinema.Api.Handlers;
using Moq;
using BetterCinema.Api.Mapping;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Controllers;
using BetterCinema.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using static BetterCinema.UnitTests.Fakes.Dummy;

namespace BetterCinema.UnitTests.Controllers
{
    public class SessionsControllerTests
    {
        private readonly IConfigurationProvider configuration;
        private readonly Mock<ISessionsHandler> sessionsHandler = new Mock<ISessionsHandler>();
        private readonly IMapper mapper;
        private readonly SessionsController sessionsController;

        public SessionsControllerTests()
        {
            IConfigurationProvider configuration = new MapperConfiguration(
                cfg => cfg.AddProfile(new SessionModelsMappingProfile()));

            mapper = new Mapper(configuration);
            sessionsController = new SessionsController(sessionsHandler.Object, mapper);
        }

        [Fact]
        public async Task GetSessions_SessionsExist_ReturnsGetSessionsResponse()
        {
            // Arrange
            var theaterId = Any<int>();
            var movieId = Any<int>();

            IEnumerable<SessionEntity> sessions = Build<SessionEntity>().Without(x => x.MovieEntity).CreateMany(5);
            IEnumerable<GetSessionResponse> sessionsResponse = mapper.Map<IEnumerable<GetSessionResponse>>(sessions);

            sessionsHandler.Setup(x =>
                    x.GetSessions(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(sessions);

            // Act
            var response = await sessionsController.GetSessions(theaterId, movieId);

            // Assert
            response.Value.Should().BeOfType<GetSessionsResponse>();
            response.Value.Sessions.Should().BeEquivalentTo(sessionsResponse);

            sessionsHandler.Verify(x => x.GetSessions(theaterId, movieId));
        }

        [Fact]
        public async Task GetSessions_SessionsDoNotExist_ReturnsNotFound()
        {
            // Arrange
            var theaterId = Any<int>();
            var movieId = Any<int>();

            sessionsHandler.Setup(x =>
                    x.GetSessions(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((IEnumerable<SessionEntity>)null);

            // Act
            var response = await sessionsController.GetSessions(theaterId, movieId);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetSession_SessionExists_ReturnsGetSessionResponse()
        {
            // Arrange
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            var sessionEntity = Build<SessionEntity>().Without(x=> x.MovieEntity).Create();
            var sessionResponse = mapper.Map<GetSessionResponse>(sessionEntity);

            sessionsHandler.Setup(x =>
                    x.GetSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(sessionEntity);

            // Act
            var response = await sessionsController.GetSession(theaterId, movieId, sessionId);

            // Assert
            response.Value.Should().BeOfType<GetSessionResponse>();
            response.Value.Should().BeEquivalentTo(sessionResponse);

            sessionsHandler.Verify(x => x.GetSession(theaterId, movieId, sessionId));
        }

        [Fact]
        public async Task GetSession_SessionDoesNotExist_ReturnsNotFound()
        {
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            sessionsHandler.Setup(x =>
                    x.GetSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((SessionEntity)null);

            // Act
            var response = await sessionsController.GetSession(theaterId, movieId, sessionId);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteSession_SessionExists_ReturnsNoContent()
        {
            // Arrange
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            sessionsHandler.Setup(x =>
                    x.DeleteSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            IActionResult response = await sessionsController.DeleteSession(theaterId, movieId, sessionId);

            // Assert
            response.Should().BeOfType<NoContentResult>();

            sessionsHandler.Verify(x => x.DeleteSession(theaterId, movieId, sessionId));
        }

        [Fact]
        public async Task DeleteSession_SessionDoesNotExist_ReturnsNotFound()
        {
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            sessionsHandler.Setup(x =>
                    x.DeleteSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            IActionResult response = await sessionsController.DeleteSession(theaterId, movieId, sessionId);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PatchSession_SessionExists_ReturnsNoContent()
        {
            // Arrange
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            var updateSessionRequest = Any<UpdateSessionRequest>();

            sessionsHandler.Setup(x =>
                    x.UpdateSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UpdateSessionRequest>()))
                .ReturnsAsync(true);

            // Act
            var response = await sessionsController.PatchSession(theaterId, movieId, sessionId, updateSessionRequest);

            // Assert
            response.Should().BeOfType<NoContentResult>();

            sessionsHandler.Verify(x => x.UpdateSession(theaterId, movieId, sessionId, updateSessionRequest));
        }

        [Fact]
        public async Task PatchSession_SessionDoesNotExist_ReturnsNotFound()
        {
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            var updateSessionRequest = Any<UpdateSessionRequest>();

            sessionsHandler.Setup(x =>
                    x.UpdateSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UpdateSessionRequest>()))
                .ReturnsAsync(false);

            // Act
            var response = await sessionsController.PatchSession(theaterId, movieId, sessionId, updateSessionRequest);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostSession_SessionExists_ReturnsGetSessionResponse()
        {
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();
            var expectedActionName = "GetSession";

            var sessionEntity = Build<SessionEntity>().Without(x => x.MovieEntity).Create();
            var createSessionRequest = mapper.Map<CreateSessionRequest>(sessionEntity);
            var sessionResponse = mapper.Map<GetSessionResponse>(sessionEntity);

            sessionsHandler.Setup(x =>
                    x.CreateSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CreateSessionRequest>()))
                .ReturnsAsync(sessionEntity);

            // Act
            var response = await sessionsController.PostSession(theaterId, movieId, createSessionRequest);

            // Assert
            response.Should().BeOfType<CreatedAtActionResult>();
            var createdActionResult = response as CreatedAtActionResult;
            createdActionResult.ActionName.Should().Be(expectedActionName);
            createdActionResult.Value.Should().BeEquivalentTo(sessionResponse);

            sessionsHandler.Verify(x => x.CreateSession(theaterId, movieId, createSessionRequest));
        }

        [Fact]
        public async Task PostSession_SessionParentDoesNotExist_ReturnsNotFound()
        {
            var theaterId = Any<int>();
            var movieId = Any<int>();
            var sessionId = Any<int>();

            var createSessionRequest = Any<CreateSessionRequest>();

            sessionsHandler.Setup(x =>
                    x.CreateSession(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CreateSessionRequest>()))
                .ReturnsAsync((SessionEntity)null);

            // Act
            var response = await sessionsController.PostSession(theaterId, movieId, createSessionRequest);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }
    }
}