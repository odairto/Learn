using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOdataApi.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
