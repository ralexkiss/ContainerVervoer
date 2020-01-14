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
        List<Stack> Stacks;

        public Row(int length)
        {
            Stacks = Enumerable.Repeat(new Stack(), length).ToList();
        }
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

        public bool AddCooledContainers(Container container)
        {
            return Stacks.First().AddContainer(container);
        }

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

        // TODO: Recode this since this isn't really readable + could probably also be simpler
        public bool AddValuableContainers(Container container)
        {
            for (int i = 1; i < Stacks.Count; i++)
            {
                if (i == Stacks.Count)
                {
                    if (Stacks[i - 1].GetHeight() < Stacks[i].GetHeight() && Stacks[i].AddContainer(container))
                    {
                        return true;
                    }
                }
                else
                {
                    if (Stacks[i - 1].GetHeight() >= Stacks[i].GetHeight() || Stacks[i + 1].GetHeight() >= Stacks[i].GetHeight())
                    {
                        continue;
                    }
                    if (Stacks[i].AddContainer(container))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int GetWeight()
        {
            return Stacks.Sum(stacks => stacks.GetWeight());
        }
    }
}
