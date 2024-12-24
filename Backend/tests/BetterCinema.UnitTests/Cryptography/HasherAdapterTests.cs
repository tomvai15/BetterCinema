using BetterCinema.Api.Cryptography;
using FluentAssertions;
using static BetterCinema.UnitTests.Fakes.Dummy;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BetterCinema.UnitTests.Cryptography
{
    public class HasherAdapterTests
    {
        private readonly HasherAdapter hasherAdapter = new HasherAdapter();

        [Fact]
        public void HashText_ReturnsHashedText()
        {
            // Arrange
            var textToHash = Any<string>();

            // Act
            var hashedText = hasherAdapter.HashText(textToHash);

            // Assert
            var doesHashMatch = BCryptNet.Verify(textToHash, hashedText);
            doesHashMatch.Should().BeTrue();
        }

        [Fact]
        public void VerifyText_TextAndHashMatch_ReturnTrue()
        {
            // Arrange
            var textToHash = Any<string>();
            var hashedText = BCryptNet.HashPassword(textToHash);

            // Act
            var doesHashMatch = hasherAdapter.VerifyHashedText(textToHash, hashedText);

            // Assert
            doesHashMatch.Should().BeTrue();
        }

        [Fact]
        public void VerifyText_TextAndHashDoNotMatch_ReturnFalse()
        {
            // Arrange
            var textToHash = Any<string>();
            var hashedText = Any<string>();

            // Act
            var doesHashMatch = hasherAdapter.VerifyHashedText(textToHash, hashedText);

            // Assert
            doesHashMatch.Should().BeFalse();
        }
    }
}