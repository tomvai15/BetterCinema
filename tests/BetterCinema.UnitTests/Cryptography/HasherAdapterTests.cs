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
            string textToHash = Any<string>();

            // Act
            string hashedText = hasherAdapter.HashText(textToHash);

            // Assert
            bool doesHashMatch = BCryptNet.Verify(textToHash, hashedText);
            doesHashMatch.Should().BeTrue();
        }

        [Fact]
        public void VerifyText_TextAndHashMatch_ReturnTrue()
        {
            // Arrange
            string textToHash = Any<string>();
            string hashedText = BCryptNet.HashPassword(textToHash);

            // Act
            bool doesHashMatch = hasherAdapter.VerifyHashedText(textToHash, hashedText);

            // Assert
            doesHashMatch.Should().BeTrue();
        }

        [Fact]
        public void VerifyText_TextAndHashDoNotMatch_ReturnFalse()
        {
            // Arrange
            string textToHash = Any<string>();
            string hashedText = Any<string>();

            // Act
            bool doesHashMatch = hasherAdapter.VerifyHashedText(textToHash, hashedText);

            // Assert
            doesHashMatch.Should().BeFalse();
        }
    }
}