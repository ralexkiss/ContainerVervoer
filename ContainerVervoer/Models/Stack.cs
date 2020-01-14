using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Stack
    {
        public List<Container> Containers = new List<Container>();

        public bool AddContainer(Container container)
        {
            if (ContainsValuable() || !WeightWithinLimit(container))
            {
                return false;
            }
            Containers.Add(container);
            return true;
        }

        public bool WeightWithinLimit(Container container)
        {
            return Containers.Skip(1).Sum(foundContainer => foundContainer.Weight) + container.Weight <= 120000;
        }

        public bool ContainsValuable()
        {
            return Containers.Any(container => container.Type == ContainerType.Valuable);
        }

        public int GetHeight()
        {
            return Containers.Count;
        }

        public int GetWeight()
        {
            return Containers.Sum(container => container.Weight);
        }
    }
}
