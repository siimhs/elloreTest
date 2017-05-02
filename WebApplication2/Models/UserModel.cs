using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public CarModel Cars { get; set; }
    }
}