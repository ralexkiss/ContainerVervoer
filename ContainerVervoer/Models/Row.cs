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
            for (int i = 0; i < length; i++)
            {
                Stacks.Add(new Stack());
            }
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
            foreach (Stack stack in Stacks) // Maybe with a .Skip(1)?? (Should test it what works better, talk with bert.
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
            for (int i = 0; i < Stacks.Count; i++)
            {
                if (i == Stacks.Count || i == 0)
                {
                    if (i == 0 && Stacks[i + 1].GetHeight() < Stacks[i].GetHeight() && Stacks[i].AddContainer(container) || 
                        i == Stacks.Count && Stacks[i - 1].GetHeight() < Stacks[i].GetHeight() && Stacks[i].AddContainer(container))
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
