using ContainerVervoer.Forms;
using ContainerVervoer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerVervoer.Logic
{
    public class LogisticsLogic
    {
        LogisticsForm logisticsForm;
        Logistics logistics;

        public LogisticsLogic(LogisticsForm logisticsForm)
        {
            this.logisticsForm = logisticsForm;
            this.logistics = logisticsForm.shipLogic.cargoShip.logistics;
        }

        public bool StartSorting()
        {
            if (SortCooledContainers())
            {
                if (SortNormalContainers())
                {
                    if (SortValuableContainers())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        
        public bool SortCooledContainers()
        {
            if (logistics.containersToPlace.Where(container => container.cooled == true).Count() > (logistics.gridLocation.Keys.Where(location => location.z == 0).Count() * 5))
            {
                MessageBox.Show("It's not possible to place this many cooled containers!");
                return false;
            }
            return true;
        }


        public bool SortNormalContainers()
        {
            return true;
        }


        public bool SortValuableContainers()
        {
            return true;
        }



        /// <summary>
        /// Creates a new container and adds it to the list of containers that need to get placed on the CargoShip.
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="cooled"></param>
        /// <param name="valuable"></param>
        public void CreateContainer(int weight, bool cooled, bool valuable)
        {
            weight += 4;
            String id = Guid.NewGuid().ToString();
            if (!ExceedsMaxWeight(weight))
            {
                Container container = new Container(id, weight, cooled, valuable);
                logisticsForm.cargoDeckBox.Items.Add(container.ToString());
                logisticsForm.shipLogic.AddContainerToCargoShip(container);
            }
        }

        /// <summary>
        /// Checks if the maximum weight is exceeded.
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public bool ExceedsMaxWeight(int weight)
        {
            if (weight > 30)
            {
                MessageBox.Show("Container cannot be over 30.000 kg!");
                return true;
            }
            return false;
        }
    }
}
