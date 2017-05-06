using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Configuration;
using WebApplication2.Controllers;
using WebApplication2.Models;
using Xunit;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;

namespace TestSuite.Acceptance
{
    public class UsersControllerTests : IDisposable
    {
        private UsersController controller;
        private UnityContainer container;
        private HttpClient client;
        private string usersUrl;

        public UsersControllerTests()
        {
            controller = new UsersController();
            container = new UnityContainer();
            client = SetupFakeServer();
            usersUrl = "http://localhost:5000/api/Users/";
        }

        [Fact]
        public void GetUsersReturnsAllUsers()
        {
            string json = GetJsonResponse();

            var actualUsers = JsonConvert.DeserializeObject<List<UserModel>>(json);

            var expectedUsers = FakeUsersWithCars();

            for (int i = 0; i < actualUsers.Count; i++)
            {
                Assert.Equal(expectedUsers[i].id, actualUsers[i].id);
                Assert.Equal(expectedUsers[i].Name, actualUsers[i].Name);
                Assert.Equal(expectedUsers[i].Cars.id, actualUsers[i].Cars.id);
                Assert.Equal(expectedUsers[i].Cars.Model, actualUsers[i].Cars.Model);
                Assert.Equal(expectedUsers[i].Cars.Name, actualUsers[i].Cars.Name);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByIdReturnsJsonUserWithThatId(int userId)
        {
            usersUrl += userId;

            var json = GetJsonResponse();

            var user = JsonConvert.DeserializeObject<UserModel>(json);

            Assert.Equal(userId, user.id);
        }

        [Theory]
        [InlineData("Teet", "asc", 1)]
        [InlineData("Simmo", "asc", 1)]
        [InlineData("Teet", "desc", -1)]
        [InlineData("Simmo", "desc", -1)]
        public void GetUsersWithNameAndSortAscendingByIdReturnsSortedJsonUsers(string userName, string orderBy, int compareResult)
        {
            usersUrl += $"?find={userName}&sort=id:{orderBy}";

            var json = GetJsonResponse();

            var user = JsonConvert.DeserializeObject<List<UserModel>>(json);

            Assert.True(user.Count > 0);
            Assert.True(user.TrueForAll(a => a.Name == userName));
            
            for (int i = 0; i < user.Count - 1; i++)
            {
                Assert.True(user[i].id.CompareTo(user[i + 1].id) != compareResult); 
            }
        }        

        [Theory]
        [InlineData("Teet", "asc", 1)]
        [InlineData("Simmo", "asc", 1)]
        [InlineData("Teet", "desc", -1)]
        [InlineData("Simmo", "desc", -1)]
        public void GetUsersWithNameAndSortAscendingByNameReturnsSortedJsonUsers(string userName, string orderBy, int compareResult)
        {
            usersUrl += $"?find={userName}&sort=name:{orderBy}";

            var json = GetJsonResponse();

            var user = JsonConvert.DeserializeObject<List<UserModel>>(json);

            Assert.True(user.Count > 0);
            Assert.True(user.TrueForAll(a => a.Name == userName));

            for (int i = 0; i < user.Count - 1; i++)
            {
                Assert.True(user[i].Name.CompareTo(user[i + 1].Name) != compareResult);
            }
        }

        private string GetJsonResponse()
        {
            var response = client.GetAsync(usersUrl);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            return json;
        }

        private HttpClient SetupFakeServer()
        {
            var config = new HttpConfiguration();

            ElloreWebAPIConfig.Register(config);

            config.DependencyResolver = new UnityResolver(container);

            HttpServer server = new HttpServer(config);

            return new HttpClient(server);
        }

        private List<UserModel> FakeUsersWithCars()
        {
            var cars = new List<CarModel>
            {
                new CarModel {id=0,Name="Ford",Model="Focus" },
                new CarModel {id=1,Name="Kia",Model="Cee'd" },
                new CarModel {id=2,Name="Volvo",Model="S80" }
            };

            var usersWithCars = new List<UserModel>
            {
                new UserModel {id=0,Name="Teet",Cars=cars[0]},
                new UserModel {id=1,Name="Teet",Cars=cars[2]},
                new UserModel {id=2,Name="Simmo",Cars=cars[1]}
            };

            return usersWithCars;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
