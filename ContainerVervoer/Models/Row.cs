using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Row
    {
        public List<Stack> Stacks = new List<Stack>();
        public Row(int length)
        {
            GenerateStacks(length);
        }

        #region Generates the stacks
        public void GenerateStacks(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Stacks.Add(new Stack());
            }
        }
        #endregion

        #region Checks the Container Type
        public bool AddContainer(Container container)
        {
            switch(container.Type)
            {
                case ContainerType.Cooled:
                    return AddCooledContainers(container);
                case ContainerType.Normal:
                    return AddNormalContainers(container);
                case ContainerType.Valuable:
                    return AddValuableContainers(container);
            }
            return false;
        }
        #endregion

        #region Cooled Containers
        public bool AddCooledContainers(Container container)
        {
            return Stacks.First().AddContainer(container);
        }
        #endregion

        #region Normal Containers
        public bool AddNormalContainers(Container container)
        {
            foreach (Stack stack in Stacks.Skip(1))
            {
                if (stack.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Valuable Containers
        public bool AddValuableContainers(Container container)
        {
            for (int i = 0; i < Stacks.Count; i++)
            {
                if (i % 2 == 0) // Checks if the stack is an even stack.
                {
                    if (Stacks[i + 1] != null) // Checks if the next stack exists
                    {
                        if (Stacks[i + 1].Containers.Count <= Stacks[i].Containers.Count) // Checks if the stack infront is lower in height than the one where valuable would be added
                        {
                            if (Stacks[i].AddContainer(container)) // Adds the Container
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region Getter for Weight
        public int GetWeight()
        {
            return Stacks.Sum(stacks => stacks.GetWeight());
        }
        #endregion
    }
}
