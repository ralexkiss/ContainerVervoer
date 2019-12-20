using ContainerVervoer.Enums;
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
        public ContainerType containerType { get; set; }

        public Container(string id, int weight, ContainerType containerType)
        {
            this.id = id;
            this.weight = weight;
            this.containerType = containerType;
        }

        /// <summary>
        /// Returns a string value of a container
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + id + " - Weight: " + weight + " - ContainerType: " + containerType;
        }
    }
}
