using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CarsController : ApiController
    {
        List<CarModel> cars;
        public CarsController()
        {
            CarModel carModel = new CarModel();
            cars = carModel.FillCars();
        }
        public IHttpActionResult Get(string find)
        {
            var result = cars.Where(x => x.Name.Equals(find));
            return Ok(result);
        }
        public IHttpActionResult Get(string find, string sort)
        {
            var result = cars.Where(x => x.Name.Equals(find)).ToList();

            var splitSort = sort.Split(':');
            var sortBy = splitSort[0];
            var OrderBy = splitSort[1];
            bool isDesc = true;
            switch (OrderBy)
            {
                case "asc":
                    isDesc = false;
                    break;
                default:
                    isDesc = true;
                    break;
            }
            switch (sortBy)
            {
                case "name":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.Name).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.Name).ToList();
                    }
                    break;
                case "model":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.Model).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.Model).ToList();
                    }
                    break;
                case "id":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.id).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.id).ToList();
                    }
                    break;
                default:
                    break;
            }
            return Ok(result);
        }
        public IHttpActionResult Get(int id)
        {
            if (id > cars.Count - 1)
            {
                return NotFound();
            }
            return Ok(cars[id]);
        }
    }
}
