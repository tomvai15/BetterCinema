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
            string expectedRole = Any<string>();

            AddClaimToHttpContext(ClaimTypes.Role, expectedRole);

            // Act 
            bool wasFound = claimsProvider.TryGetUserRole(out string actualRole);

            // Assert
            Assert.True(wasFound);
            Assert.Equal(actualRole,expectedRole);
        }

        [Fact]
        public void TryGetRole_ClaimDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string expectedRole = Any<string>();

            var httpContext = new DefaultHttpContext();
            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            bool wasFound = claimsProvider.TryGetUserRole(out string actualRole);

            // Assert
            wasFound.Should().BeFalse();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("SomeClaim")]
        public void TryGetClaim_ClaimExists_ReturnsTrueAndClaim(string claimType)
        {
            // Arrange
            string expectedClaimValue = Any<string>();

            AddClaimToHttpContext(claimType, expectedClaimValue);

            // Act 
            bool wasFound = claimsProvider.TryGetClaim(claimType, out string actualClaim);

            // Assert
            wasFound.Should().BeTrue();
            actualClaim.Should().Be(expectedClaimValue);
        }

        [Fact]
        public void TryGetClaim_ClaimDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string claimType = Any<string>(); 
            string expectedRole = Any<string>();

            var httpContext = new DefaultHttpContext();
            contextAccessor.SetupGet(x => x.HttpContext).Returns(httpContext);

            // Act 
            bool wasFound = claimsProvider.TryGetClaim( claimType, out string actualRole);

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
