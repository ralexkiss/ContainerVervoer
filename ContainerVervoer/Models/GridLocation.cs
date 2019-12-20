using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class GridLocation
    {
        public int x, z;

        public GridLocation(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            string final = "Location: " + x + " - " + z;
            return final;
        }
    }
}
