using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class CargoShip
    {
        public Logistics logistics = new Logistics();
        public int width { get; set; }
        public int length { get; set; }

        public CargoShip(int width, int length)
        {
            this.width = width;
            this.length = length;
        }

    }
}
