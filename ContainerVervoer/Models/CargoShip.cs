using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class CargoShip
    {
        List<Row> Rows { get; }
        public int length { get; }
        public int width { get; }
        public int maximumWeight { get; }


        public CargoShip(int length, int width, int maximumWeight)
        {
            Rows = Enumerable.Repeat(new Row(length), width).ToList();
            this.width = width;
            this.length = length;
            this.maximumWeight = maximumWeight;
        }

        public int GetWeight()
        {
            return Rows.Sum(row => row.GetWeight());
        }

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

        public bool PlaceAtLocation(Container container)
        {
            return (Rows.GetRange(0, Rows.Count / 2).Sum(row => row.GetWeight()) <= Rows.GetRange(Rows.Count % 2 == 0 ? Rows.Count / 2 : Rows.Count / 2 + 1, Rows.Count / 2).Sum(row => row.GetWeight()) 
                ? PlaceOnTheLeft(container) 
                : PlaceOnTheRight(container));
        }

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
