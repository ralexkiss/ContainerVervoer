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

        public override string ToString()
        {
            string result = "Stack Information: ";
            if (Containers.Count > 0)
            {
                Containers.ForEach(container => result += "\n" + container.ToString());
                return result;
            }
            return "Stack is Empty";
        }
        #region 2 checks before adding to stack.
        public bool AddContainer(Container container)
        {
            if (ContainsValuable() || !WeightWithinLimit(container))
            {
                return false;
            }
            Containers.Add(container);
            return true;
        }
        #endregion

        #region Check if weight is within limit
        public bool WeightWithinLimit(Container container)
        {
            return Containers.Skip(1).Sum(foundContainers => foundContainers.Weight) + container.Weight <= 120000;
        }
        #endregion

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
