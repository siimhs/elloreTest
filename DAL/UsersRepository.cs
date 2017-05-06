using Domain;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DAL
{
    public class UsersRepository
    {
        private List<CarModel> cars;
        private IEnumerable<UserModel> users;

        public UsersRepository()
        {
            cars = PopulateCarsList();
            users = PopulateUsersList();
        }

        private List<CarModel> PopulateCarsList()
        {
            CarModel carModel = new CarModel();
            return carModel.FillCars();
        }

        private IEnumerable<UserModel> PopulateUsersList()
        {
            return new List<UserModel>
            {
                new UserModel {id=0,Name="Teet",Cars=cars[0]},
                new UserModel {id=1,Name="Teet",Cars=cars[2]},
                new UserModel {id=2,Name="Simmo",Cars=cars[1]}
            };
        }

        public IEnumerable<UserModel> Get()
        {
            return users;
        }

        public UserModel Get(int id)
        {
            if (id > users.Count() - 1)
            {
                return null;
            }
            return users.Where(x => x.id == id).First();
        }

        public IEnumerable<UserModel> Get(string name)
        {
            return users.Where(x => x.Name.Equals(name));
        }
    }
}
