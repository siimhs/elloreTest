using DAL;
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
            IEnumerable<UserModel> result = repository.Get(find, sort);
            return Ok(result);
        }
        public IHttpActionResult Get(int id)
        {
            var user = repository.Get(id);

            return user == null ? (IHttpActionResult)NotFound() : Ok(user);            
        }
    }    
}
