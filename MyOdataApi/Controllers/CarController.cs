using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using MyOdataApi.Model;

namespace MyOdataApi.Controllers
{

    [EnableQuery(PageSize = 20)]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private static readonly List<Car> Cars = new List<Car>
        {
            new Car { Id = 1, Name = "Onix", Year = 2019, Color = "Cinza", Manufacturer = new Manufacturer{ Id = 1, Name = "Chevrolet" }},
            new Car { Id = 2, Name = "Versa", Year = 2018, Color = "Preto", Manufacturer = new Manufacturer{ Id = 2, Name = "Nissan" }},
            new Car { Id = 3, Name = "Fusion", Year = 2012, Color = "Branco", Manufacturer = new Manufacturer{ Id = 3, Name = "Ford" }},
            new Car { Id = 4, Name = "Civic", Year = 2016, Color = "Preto", Manufacturer = new Manufacturer{ Id = 4, Name = "Honda" }},
            new Car { Id = 5, Name = "Corolla", Year = 2017, Color = "Vermelho", Manufacturer = new Manufacturer{ Id = 5, Name = "Toyota" }},
            new Car { Id = 6, Name = "Ranger", Year = 2015, Color = "Azul", Manufacturer = new Manufacturer{ Id = 3, Name = "Ford" }}
        };


        [HttpGet]
        public IQueryable<Car> Get()
        {
            return Cars.AsQueryable();
        }

        [HttpGet]
        public Car Get(int key)
        {
            return Cars.SingleOrDefault(x => x.Id == key);
        }
    }
}