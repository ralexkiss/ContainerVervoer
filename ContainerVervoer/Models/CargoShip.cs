using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class CargoShip
    {
        public List<Row> Rows = new List<Row>();
        public int length { get; }
        public int width { get; }
        public int maximumWeight { get; }


        public CargoShip(int length, int width, int maximumWeight)
        {
            for (int i = 0; i < width; i++)
            {
                Rows.Add(new Row(length));
            }
            this.width = width;
            this.length = length;
            this.maximumWeight = maximumWeight;
        }

        public int GetWeight()
        {
            return Rows.Sum(row => row.GetWeight());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        public bool AddContainers(List<Container> containers)
        {
            if (containers.Sum(container => container.Weight) + GetWeight() > maximumWeight / 2 || 
                containers.Sum(container => container.Weight) + GetWeight() > maximumWeight)
            {
                return false;
            }
            foreach (Container container in containers)
            {
                PlaceContainer(container);
            }
            return true;
        }

        /// <summary>
        /// Checks if the container can be placed in the middle and places it there, otherwise sends it further to check if need to get placed left or right.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceContainer(Container container)
        {
            if (Rows.Count % 2 != 0)
            {
                if (Rows[Rows.Count / 2].AddContainer(container))
                {
                    return true;
                }
            }
            return PlaceAtLocation(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceAtLocation(Container container)
        {
            return (Rows.GetRange(0, Rows.Count / 2).Sum(row => row.GetWeight()) <= Rows.GetRange(Rows.Count % 2 == 0 ? Rows.Count / 2 : Rows.Count / 2 + 1, Rows.Count / 2).Sum(row => row.GetWeight()) 
                ? PlaceOnTheLeft(container) 
                : PlaceOnTheRight(container));
        }

        /// <summary>
        /// Places the given container on the left side of the ship for balance.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceOnTheLeft(Container container)
        {
            foreach (Row row in Rows.GetRange(0, Rows.Count / 2))
            {
                if (row.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Places the given container on the right side of the ship for balance.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceOnTheRight(Container container)
        {
            foreach (Row row in Rows.GetRange(Rows.Count % 2 == 0 ? Rows.Count / 2 : Rows.Count / 2 + 1, Rows.Count / 2))
            {
                if (row.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
