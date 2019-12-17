using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Container
    {
        public string id { get; set; }
        public int weight { get; set; }
        public bool valuable, cooled;

    public Container(string id, int weight, bool cooled, bool valuable)
        {
            this.id = id;
            this.weight = weight;
            this.valuable = valuable;
            this.cooled = cooled;
        }

        /// <summary>
        /// Returns a string value of a container
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Container: " + id + " - Weight: " + weight + " - Valuable: " + valuable + " - Cooled: " + cooled;
        }
    }
}
