using ContainerVervoer.Enums;
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

        public bool StartSorting()
        {
            return Ship.AddContainers(SortContainersList(containersToPlace));
        }

        public List<Container> SortContainersList(List<Container> containers)
        {
            return containers.Where(container => container.Type == ContainerType.Cooled).ToList()
                .Concat(containers.Where(container => container.Type == ContainerType.Normal).ToList())
                .Concat(containers.Where(container => container.Type == ContainerType.Valuable).ToList())
                .ToList();
        }

        public string GetShip()
        {
            return Ship.ToString();
        }
    }
}
