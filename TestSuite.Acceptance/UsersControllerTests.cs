using System.Collections.Generic;
using System.Web.Http.Results;
using WebApplication2.Controllers;
using WebApplication2.Models;
using Xunit;

namespace TestSuite.Acceptance
{
    public class UsersControllerTests
    {
        [Fact]
        public void Get_ReturnsAllUsers()
        {
            UsersController controller = new UsersController();

            var result = (OkNegotiatedContentResult<List<UserModel>>)controller.Get();

            var actualUsers = result.Content;
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
    }
}
