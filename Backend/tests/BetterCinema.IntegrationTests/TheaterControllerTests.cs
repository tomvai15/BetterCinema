using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BetterCinema.Api.Contracts.Theaters;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BetterCinema.IntegrationTests
{
    public class TheaterControllerTests(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
    {
        [Theory]
        [InlineData("/api/theaters")]
        [InlineData("/api/theaters/1")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }


        [Theory]
        [InlineData("/api/theater")]
        [InlineData("/api/theaters/-1")]
        public async Task Get_EndpointsReturnNotFound(string url)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_EndpointsReturnSuccess()
        {
            // Arrange
            var url = "api/theaters";
            var client = factory.CreateClient();
            var theaterRequest = new CreateTheaterRequest
            {
                Name = "Name",
                Address = "Address",
                Description = "Description",
                ImageUrl = "data",
            };

            // Act
            var response = await client.PostAsJsonAsync(url, theaterRequest);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task Post_MissingProperty_ReturnsBadRequest()
        {
            // Arrange
            var url = "api/theaters";
            var client = factory.CreateClient();
            var theaterRequest = new CreateTheaterRequest
            {
                Name = "Name",
                Address = "Address",
                Description = "Description",
            };

            // Act
            var response = await client.PostAsJsonAsync(url, theaterRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest); // Status Code 200-299
        }
    }
}