﻿using DAL;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class UsersController : ApiController
    {
        List<UserModel> users;
        UsersRepository repository;

        public UsersController(UsersRepository repository)
        {
            this.repository = repository;
            users = repository.Get().ToList();
        }

        public IHttpActionResult Get()
        {
            return Ok(users);
        }
        public IHttpActionResult Get(string find)
        {
            var result = repository.Get(find);

            return Ok(result);
        }
        public IHttpActionResult Get(string find, string sort)
        {
            var result = users.Where(x => x.Name.Equals(find)).ToList();
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
                case "cars":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.Cars).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.Cars).ToList();
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
            var user = repository.Get(id);

            return user == null ? (IHttpActionResult)NotFound() : Ok(user);            
        }
    }    
}
