using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CarModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }

        public List<CarModel> FillCars()
        {
            List<CarModel> cars = new List<CarModel>
            {
                new CarModel {id=0,Name="Ford",Model="Focus" },
                new CarModel {id=1,Name="Kia",Model="Cee'd" },
                new CarModel {id=2,Name="Volvo",Model="S80" }
            };
            return cars;
        }
    }
}