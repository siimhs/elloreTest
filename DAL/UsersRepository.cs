using Domain;
using System.Collections.Generic;

namespace DAL
{
    public class UsersRepository
    {
        private List<CarModel> cars;

        public UsersRepository()
        {
            cars = PopulateCarsList();
        }

        private List<CarModel> PopulateCarsList()
        {
            CarModel carModel = new CarModel();
            return carModel.FillCars();
        }

        public IEnumerable<UserModel> Get()
        {
            return new List<UserModel>
            {
                new UserModel {id=0,Name="Teet",Cars=cars[0]},
                new UserModel {id=1,Name="Teet",Cars=cars[2]},
                new UserModel {id=2,Name="Simmo",Cars=cars[1]}
            };
        }
    }
}
