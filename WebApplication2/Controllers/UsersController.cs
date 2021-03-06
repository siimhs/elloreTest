﻿using DAL;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        UsersRepository repository;

        public UsersController(UsersRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllUsers()
        {
            var result = repository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsersBy([FromUri(Name = "find")] string name)
        {
            var result = repository.GetBy(name);

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsersBy([FromUri(Name = "find")] string name, [FromUri] string sort)
        {
            IEnumerable<UserModel> result = repository.GetBy(name, sort);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUsersBy(int id)
        {
            var user = repository.GetBy(id);

            return user == null ? (IHttpActionResult)NotFound() : Ok(user);            
        }
    }    
}
