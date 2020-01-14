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
        public int Weight { get; set; }

        public ContainerType Type { get; }

        public Container(int weight, ContainerType type)
        {
            SetWeight(weight);
            Type = type;
        }

        private void SetWeight(int weight)
        {
            if (weight < 4000 || weight > 30000)
            {
                throw new ArgumentException("Weight must be between the minimum of 4000 and the maximum of 30000.");
            }
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Container: Type: {Type}, Weight: {Weight}";
        }
    }
}
