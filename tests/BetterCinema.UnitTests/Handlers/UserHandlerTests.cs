using AutoFixture;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Models;
using BetterCinema.UnitTests.Fakes;
using BetterCinema.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using static BetterCinema.UnitTests.Fakes.Dummy;

namespace BetterCinema.UnitTests.Handlers
{
    public class UserHandlerTests: IClassFixture<CinemaDbFixture>
    {
        private readonly CinemaDbFixture cinemaDbfixture;

        public  UserHandlerTests(CinemaDbFixture cinemaDbfixture)
        {
            this.cinemaDbfixture = cinemaDbfixture;
        }

        [Fact]
        public async void TryGetUserByEmail_UserExists_ReturnUser()
        {
            // Arrange
            User actualUser = null;
            User expectedUser = SampleData.GetUsers().First();

            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                actualUser = await usersHandler.GetUserByEmail(expectedUser.Email);
            }

            // Assert
            actualUser.Email.Should().Be(expectedUser.Email);
        }

        [Fact]
        public async void TryGetUserByEmail_UserNotFound_ReturnNull()
        {
            // Arrange
            User actualUser = null;
            string nonExistingEmail = Any<string>();

            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                actualUser = await usersHandler.GetUserByEmail(nonExistingEmail);
            }

            // Assert
            actualUser.Should().BeNull();
        }

        [Fact]
        public async void TryGetUserById_UserExists_ReturnUser()
        {
            // Arrange
            User actualUser = null;
            User expectedUser = SampleData.GetUsers().First();

            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                actualUser = await usersHandler.GetUserById(expectedUser.UserId);
            }

            // Assert
            actualUser.UserId.Should().Be(expectedUser.UserId);
        }

        [Fact]
        public async void TryGetUserById_UserNotFound_ReturnNull()
        {
            // Arrange
            User actualUser = null;
            int nonExistingId = -1;

            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                actualUser = await usersHandler.GetUserById(nonExistingId);
            }

            // Assert
            actualUser.Should().BeNull();
        }

        [Fact]
        public async void AddNewUser_EmailIsNotTaken_ReturnUser()
        {
            // Arrange
            User newUser = Build<User>().Without(x=>x.Theaters).Create();
            newUser.UserId = 1001;
            User addedUser = null;
            int nonExistingId = -1;


            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                addedUser = await usersHandler.AddNewUser(newUser);
            }

            // Assert
            addedUser.Should().NotBeNull();
            addedUser.Should().BeEquivalentTo(newUser);
        }

        [Fact]
        public async void AddNewUser_EmailIsTaken_ReturnNull()
        {
            // Arrange
            User existingUser = SampleData.GetUsers().First();
            User addedUser = null;
            int nonExistingId = -1;


            // Act
            using (var context = new CinemaDbContext(cinemaDbfixture.Options))
            {
                UsersHandler usersHandler = new UsersHandler(context);

                addedUser = await usersHandler.AddNewUser(existingUser);
            }

            // Assert
            addedUser.Should().BeNull();
        }
    }
}
