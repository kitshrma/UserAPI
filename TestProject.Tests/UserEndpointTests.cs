using Application.Common.Interfaces;
using Application.Common.Wrappers;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserById;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.WebAPI;
using Xunit;

namespace TestProject.Tests
{
    public class UserEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public UserEndpointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                    
                });
            });
        }

        [Fact]
        public async Task CreateUser_Returns_OK()
        {
            //Arrange
            var client = _factory.CreateClient();
            var resource = "api/v1/Users";
            var command = new CreateUserCommand
            {
                Name = "NewUser",
                EmailAddress = "newuser@gmail.com",
                MonthlySalary = 3000,
                MonthlyExpenses = 1500
            };
            var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(resource, data);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            var responseObj = JsonConvert.DeserializeObject<Response<int>>(content);
            Assert.NotNull(responseObj);
            Assert.Equal("User Created successfully", responseObj.Message);

        }

        [Fact]
        public async Task CreateUser_Returns_BadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var resource = "api/v1/Users";
            var command = new CreateUserCommand
            {
                Name = "NewUser",
                EmailAddress = "newuser@gmail.com",
                MonthlySalary = 0,
                MonthlyExpenses = 0
            };
            var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(resource, data);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(content);
            

        }

        [Fact]
        public async  Task GetUser_Returns_Ok()
        {
            //Arrange
            var client = _factory.CreateClient();
            var postResource = "api/v1/Users";
            var command = new CreateUserCommand
            {
                Name = "NewUser2",
                EmailAddress = "newuser2@gmail.com",
                MonthlySalary = 3000,
                MonthlyExpenses = 1500
            };

            var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var postResponse = await client.PostAsync(postResource, data);
            var postContent = await postResponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<Response<int>>(postContent);
            var getResource = $"api/v1/Users/{responseObj.Data}";

            //Act
            var response = await client.GetAsync(getResource);
            var content = await response.Content.ReadAsStringAsync();
            
            //Assert
            var userResponse = JsonConvert.DeserializeObject<Response<UserDTO>>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);
            Assert.Equal(command.Name, userResponse.Data.Name);
            Assert.Equal(command.EmailAddress, userResponse.Data.EmailAddress);
            Assert.Equal(command.MonthlySalary, userResponse.Data.MonthlySalary);
            Assert.Equal(command.MonthlyExpenses, userResponse.Data.MonthlyExpenses);
        }

        [Fact]
        public async Task GetUser_Returns_NotFound()
        {
            //Arrange
            var client = _factory.CreateClient();
            var resource = "api/v1/Users/100";

            //Act
            var response = await client.GetAsync(resource);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(content);
        }
    }
}
