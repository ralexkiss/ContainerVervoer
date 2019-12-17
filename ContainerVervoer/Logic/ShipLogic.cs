using ContainerVervoer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Logic
{
    public class ShipLogic
    {
        public CargoShip cargoShip;
        public double WeightPercentage;

        public ShipLogic(int width, int length)
        {
            cargoShip = new CargoShip(width, length);
            CreateLocationsOnDeck(width, length);
        }

        /// <summary>
        /// Adds a container to the CargoShips list of cargo.
        /// </summary>
        /// <param name="container"></param>
        public void AddContainerToCargoShip(Container container)
        {
            cargoShip.logistics.containersToPlace.Add(container);
        }

        /// <summary>
        /// Creates locations on the deck based on the given width & lenght.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        public void CreateLocationsOnDeck(int width, int length)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    cargoShip.logistics.gridLocation.Add(new GridLocation(x, z), new List<Container>());
                }
            }
        }
    }
}
