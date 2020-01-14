using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Port
    {
        public List<Container> containersToPlace = new List<Container>();
        public CargoShip Ship;

        public Port(int length, int width, int maximumweight)
        {
            Ship = new CargoShip(length, width, maximumweight);
        }

        public string GetShip()
        {
            return Ship.ToString();
        }
    }
}
