using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Logistics
    {
        public List<Container> containersToPlace = new List<Container>();
        public Dictionary<GridLocation, List<Container>> gridLocation = new Dictionary<GridLocation, List<Container>>();
    }
}
