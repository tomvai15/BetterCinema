using AutoFixture;
using BetterCinema.Api.Handlers;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using BetterCinema.UnitTests.Fakes;
using BetterCinema.UnitTests.Fixtures;
using FluentAssertions;
using static BetterCinema.UnitTests.Fakes.Dummy;

namespace BetterCinema.UnitTests.Handlers
{
    public class UserHandlerTests(CinemaDbFixture cinemaDbfixture) : IClassFixture<CinemaDbFixture>
    {
        [Fact]
        public async Task TryGetUserByEmail_UserExists_ReturnUser()
        {
            // Arrange
            UserEntity actualUserEntity;
            var expectedUserEntity = SampleData.GetUsers().First();

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                actualUserEntity = await usersHandler.GetUserByEmail(expectedUserEntity.Email);
            }

            // Assert
            actualUserEntity.Email.Should().Be(expectedUserEntity.Email);
        }

        [Fact]
        public async Task TryGetUserByEmail_UserNotFound_ReturnNull()
        {
            // Arrange
            UserEntity actualUserEntity;
            var nonExistingEmail = Any<string>();

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                actualUserEntity = await usersHandler.GetUserByEmail(nonExistingEmail);
            }

            // Assert
            actualUserEntity.Should().BeNull();
        }

        [Fact]
        public async Task TryGetUserById_UserExists_ReturnUser()
        {
            // Arrange
            UserEntity actualUserEntity;
            var expectedUserEntity = SampleData.GetUsers().First();

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                actualUserEntity = await usersHandler.GetUserById(expectedUserEntity.Id);
            }

            // Assert
            actualUserEntity.Id.Should().Be(expectedUserEntity.Id);
        }

        [Fact]
        public async Task TryGetUserById_UserNotFound_ReturnNull()
        {
            // Arrange
            UserEntity actualUserEntity;
            var nonExistingId = -1;

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                actualUserEntity = await usersHandler.GetUserById(nonExistingId);
            }

            // Assert
            actualUserEntity.Should().BeNull();
        }

        [Fact]
        public async Task AddNewUser_EmailIsNotTaken_ReturnUser()
        {
            // Arrange
            var newUserEntity = Build<UserEntity>().Without(x=>x.Theaters).Create();
            newUserEntity.Id = 1001;
            UserEntity addedUserEntity;

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                addedUserEntity = await usersHandler.AddNewUser(newUserEntity);
            }

            // Assert
            addedUserEntity.Should().NotBeNull();
            addedUserEntity.Should().BeEquivalentTo(newUserEntity);
        }

        [Fact]
        public async Task AddNewUser_EmailIsTaken_ReturnNull()
        {
            // Arrange
            var existingUserEntity = SampleData.GetUsers().First();
            UserEntity addedUserEntity;

            // Act
            await using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                var usersHandler = new UsersHandler(context);

                addedUserEntity = await usersHandler.AddNewUser(existingUserEntity);
            }

            // Assert
            addedUserEntity.Should().BeNull();
        }
    }
}
