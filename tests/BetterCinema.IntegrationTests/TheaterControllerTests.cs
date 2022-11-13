using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BetterCinema.IntegrationTests
{
    public class TheaterControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;

        public TheaterControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/theaters")]
        [InlineData("/api/theaters/1")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }


        [Theory]
        [InlineData("/api/theater")]
        [InlineData("/api/theaters/-1")]
        public async Task Get_EndpointsReturnNotFound(string url)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_EndpointsReturnSuccess()
        {
            // Arrange
            string url = "api/theaters";
            HttpClient client = _factory.CreateClient();
            CreateTheaterRequest theaterRequest = new CreateTheaterRequest
            {
                Name = "Name",
                Address = "Address",
                Description = "Description",
                ImageUrl = "data",
            };

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync(url, theaterRequest);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task Post_MissingProperty_ReturnsBadRequest()
        {
            // Arrange
            string url = "api/theaters";
            HttpClient client = _factory.CreateClient();
            CreateTheaterRequest theaterRequest = new CreateTheaterRequest
            {
                Name = "Name",
                Address = "Address",
                Description = "Description",
            };

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync(url, theaterRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest); // Status Code 200-299
        }
    }
}