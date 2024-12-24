using BetterCinema.Api.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using BetterCinema.Api.Providers;
using FluentAssertions;
using static BetterCinema.UnitTests.Fakes.Dummy;

namespace BetterCinema.UnitTests.Providers
{
    public class ClaimsProviderTests
    {
        private  readonly Guid guid = Guid.NewGuid();
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
            var expectedUserId = Any<int>();

            AddClaimToHttpContext(CustomClaim.UserId, expectedUserId.ToString());

            // Act 
            var wasFound = claimsProvider.TryGetUserId(out var actualUserId);
            
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
            var wasFound = claimsProvider.TryGetUserId(out var _);

            // Assert
            wasFound.Should().BeFalse();
        }

        [Fact]
        public void TryGetRole_ClaimExists_ReturnsTrueAndRole()
        {
            // Arrange
            var expectedRole = Any<string>();

            AddClaimToHttpContext(ClaimTypes.Role, expectedRole);

            // Act 
            var wasFound = claimsProvider.TryGetUserRole(out var actualRole);

            // Assert
            Assert.True(wasFound);
            Assert.Equal(actualRole,expectedRole);
        }

        [Fact]
        public void TryGetRole_ClaimDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var expectedRole = Any<string>();

            var httpContext = new DefaultHttpContext();
            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            var wasFound = claimsProvider.TryGetUserRole(out var actualRole);

            // Assert
            wasFound.Should().BeFalse();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("SomeClaim")]
        public void TryGetClaim_ClaimExists_ReturnsTrueAndClaim(string claimType)
        {
            // Arrange
            var expectedClaimValue = Any<string>();

            AddClaimToHttpContext(claimType, expectedClaimValue);

            // Act 
            var wasFound = claimsProvider.TryGetClaim(claimType, out var actualClaim);

            // Assert
            wasFound.Should().BeTrue();
            actualClaim.Should().Be(expectedClaimValue);
        }

        [Fact]
        public void TryGetClaim_ClaimDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var claimType = Any<string>(); 
            var expectedRole = Any<string>();

            var httpContext = new DefaultHttpContext();
            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            var wasFound = claimsProvider.TryGetClaim( claimType, out var actualRole);

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
