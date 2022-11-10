using BetterCinema.Api.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using static BetterCinema.UnitTests.Fakes.Dummy;
using BetterCinema.Api.Providers;
using FluentAssertions;

namespace BetterCinema.UnitTests.Providers
{
    public class ClaimsProviderTests
    {
        private readonly Mock<IHttpContextAccessor> contextAccessor = new Mock<IHttpContextAccessor>();
        private readonly ClaimsProvider claimsProvider;

        public ClaimsProviderTests()
        {
            claimsProvider = new ClaimsProvider(contextAccessor.Object);
        }

        [Fact]
        public void TryGetUserId_ClaimExists_ReturnsTrueAndUserId()
        {
            // Arrange
            int expectedUserId = Any<int>();

            AddClaimToHttpContext(CustomClaim.UserId, expectedUserId.ToString());

            // Act 
            bool wasFound = claimsProvider.TryGetUserId(out int actualUserId);

            // Assert
            wasFound.Should().BeTrue();
            actualUserId.Should().Be(expectedUserId);
        }

        [Fact]
        public void TryGetUserId_ClaimDoesNotExists_ReturnsFalse()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            bool wasFound = claimsProvider.TryGetUserId(out int _);

            // Assert
            wasFound.Should().BeFalse();
        }

        [Fact]
        public void TryGetRole_ClaimExists_ReturnsTrueAndRole()
        {
            // Arrange
            int expectedUserId = Any<int>();

            AddClaimToHttpContext(CustomClaim.UserId, expectedUserId.ToString());

            // Act 
            bool wasFound = claimsProvider.TryGetUserId(out int actualUserId);

            // Assert
            wasFound.Should().BeTrue();
            actualUserId.Should().Be(expectedUserId);
        }

        [Fact]
        public void TryGetRole_ClaimExists_ReturnsTrueAndRole()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            bool wasFound = claimsProvider.TryGetUserId(out int _);

            // Assert
            wasFound.Should().BeFalse();
        }

        private void AddClaimToHttpContext(string type, string value)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(type, value),
            }));

            var httpContext = new DefaultHttpContext();
            httpContext.User = user;
            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);
        }
    }
}
