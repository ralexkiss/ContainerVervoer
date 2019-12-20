using ContainerVervoer.Enums;
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

        #region Global Variables
        public CargoShip cargoShip;
        public double weightPercentage;
        #endregion

        public LogisticsLogic(LogisticsForm logisticsForm)
        {
            this.logisticsForm = logisticsForm;
            this.cargoShip = logisticsForm.shipLogic.cargoShip;
            this.logistics = cargoShip.logistics;
        }

        public bool StartSorting()
        {
            if (!StartCheckList())
            {
                return false;
            }
            if (!SortContainers())
            {
                return false;
            }
            if (!FinishCheckList())
            {
                return false;
            }
            return true;
        }
        public bool StartCheckList()
        {
            #region
            if (logistics.containersToPlace.Where(container => container.containerType == ContainerType.Cooled).Count() > (logistics.gridLocation.Keys.Where(location => location.z == 0).Count() * 5))
            {
                MessageBox.Show("It's not possible to place this many cooled containers!", logistics.containersToPlace.Where(container => container.containerType == ContainerType.Cooled).Count().ToString());
                return false;
            }
            #endregion
            return true;
        }

        public bool FinishCheckList()
        {
            #region Variables
            double weightDifference = logisticsForm.logisticsLogic.CheckWeightDistribution(logistics.gridLocation.Keys.ToList());
            double weightPercentage = logisticsForm.logisticsLogic.weightPercentage;
            #endregion
            if (weightDifference > 20)
            {
                MessageBox.Show("Weight distribution is larger than 20%.", "CargoShip is unsafe and will not be loaded!");
                return false;
            }
            if (weightPercentage < 50)
            {
                MessageBox.Show("CargoShip would be filled out for less than 50% of its capacity.", "CargoShip is unsafe and will not be loaded!");
                return false;
            }
            if (weightPercentage > 100)
            {
                MessageBox.Show("CargoShip would be filled out for more than 100% of its capacity.", "CargoShip is unsafe and will not be loaded!");
                return false;
            }
            return true;
        }


        #region SortContainers
        /// <summary>
        /// Sorts the Containers
        /// </summary>
        /// <returns></returns>
        public bool SortContainers()
        {
            #region Variables
            int valuableContainerAmount = logistics.containersToPlace.Count(c => c.containerType == ContainerType.Valuable);
            int zCoordLocal = 0;
            weightPercentage = (double)logistics.containersToPlace.Sum(i => i.weight) / (cargoShip.length * cargoShip.width * 150) * 100;
            #endregion
            #region Sort Cooled Containers
            foreach (Container container in logistics.containersToPlace.Where(container => container.containerType == ContainerType.Cooled).OrderByDescending(o => o.weight).ToList())
            {
                int xCoord = FindOptimalXBasedOnWeight(0, false, container);

                if (xCoord < 0)
                {   
                    MessageBox.Show("No space for more Cooled Containers");
                    break;
                }
                logistics.gridLocation[logistics.gridLocation.Keys.Where(location => location.x == xCoord && location.z == 0).First()].Add(container);
                logistics.containersToPlace.Remove(container);
            }
            #endregion
            #region Sort Normal Containers
            foreach (Container container in logistics.containersToPlace.Where(container => container.containerType == ContainerType.Normal).OrderByDescending(o => o.weight).ToList())
            {
                    for (int zCoord = zCoordLocal; zCoord < cargoShip.length; zCoord++)
                    {
                        int inputXCoord = FindOptimalXBasedOnWeight(zCoord, true, container);

                        if (inputXCoord != -1)
                        {
                            logistics.gridLocation[logistics.gridLocation.Keys.Where(location => location.x == inputXCoord && location.z == zCoord).First()].Add(container);
                            logistics.containersToPlace.Remove(container);
                            break;
                        }
                        zCoordLocal++;
                }
            }
            #endregion
            #region Sort Valuable Containers
            foreach (Container container in logistics.containersToPlace.Where(container => container.containerType == ContainerType.Valuable).OrderByDescending(o => o.weight).ToList())
            {
                for (int zCoord = zCoordLocal; zCoord < cargoShip.length; zCoord++)
                {
                    int inputXCoord = FindOptimalXBasedOnWeight(zCoord, false, container);

                    if (inputXCoord != -1)
                    {
                        logistics.gridLocation[logistics.gridLocation.Keys.Where(location => location.x == inputXCoord && location.z == zCoord).First()].Add(container);
                        logistics.containersToPlace.Remove(container);
                    }
                    zCoordLocal++;
                }
            }
            #endregion
            return true;
        }
        #endregion

        #region Main Algoritm
        /// <summary>
        /// Finds the optimal X to place the container based on the type, weight and more.
        /// </summary>
        /// <param name="zCoord"></param>
        /// <param name="checkForValuable"></param>
        /// <param name="containerToAdd"></param>
        /// <returns></returns>
        public int FindOptimalXBasedOnWeight(int zCoord, bool checkForValuable, Container containerToAdd)
        {
            //INITIALIZE
            int weightLeft = 0, weightRight = 0, weightMiddle = 0;
            int columnXCoord, minWeight;
            int forLoopMax;
            GridLocation locationOnDeck;
            List<int> terminatedX = new List<int>();

            //Even CargoShip
            if (cargoShip.width % 2 == 0)
            {
                //Calculates the weight of the left slots
                for (int i = 0; i < cargoShip.width / 2; i++)
                {
                    locationOnDeck = logistics.gridLocation.Keys.Where(location => location.x == i && location.z == zCoord).First();
                    if (CheckToTerminate(locationOnDeck, checkForValuable, containerToAdd)) terminatedX.Add(locationOnDeck.x);
                    weightLeft += GetTotalWeight(locationOnDeck);
                }

                //Calculates the weight of the right slots
                for (int i = cargoShip.width / 2; i < cargoShip.width; i++)
                {
                    locationOnDeck = logistics.gridLocation.Keys.Where(location => location.x == i && location.z == zCoord).First();
                    if (CheckToTerminate(locationOnDeck, checkForValuable, containerToAdd)) terminatedX.Add(locationOnDeck.x);
                    weightRight += GetTotalWeight(locationOnDeck);
                }

                //Compares the two weights
                if (weightLeft <= weightRight)
                {
                    columnXCoord = 0;
                    forLoopMax = cargoShip.width / 2;
                }
                else
                {
                    columnXCoord = cargoShip.width / 2;
                    forLoopMax = cargoShip.width;
                }
            }

            //Uneven CargoShip
            else
            {
                int halfOfCargoShip = (cargoShip.width - 1) / 2;

                //Calculates the weight of the left slots.
                for (int i = 0; i < halfOfCargoShip; i++)
                {
                    locationOnDeck = logistics.gridLocation.Keys.Where(location => location.x == i && location.z == zCoord).First();
                    if (CheckToTerminate(locationOnDeck, checkForValuable, containerToAdd)) terminatedX.Add(locationOnDeck.x);
                    weightLeft += GetTotalWeight(locationOnDeck);
                }

                //Calculates the weight of the right slots.
                for (int i = halfOfCargoShip + 1; i < cargoShip.width; i++)
                {
                    locationOnDeck = logistics.gridLocation.Keys.Where(location => location.x == i && location.z == zCoord).First();
                    if (CheckToTerminate(locationOnDeck, checkForValuable, containerToAdd)) terminatedX.Add(locationOnDeck.x);
                    weightRight += GetTotalWeight(locationOnDeck);
                }

                //Calculates the weight of the middle slots.
                locationOnDeck = logistics.gridLocation.Keys.Where(location => location.x == halfOfCargoShip && location.z == zCoord).First();
                if (CheckToTerminate(locationOnDeck, checkForValuable, containerToAdd)) terminatedX.Add(locationOnDeck.x);
                weightMiddle = GetTotalWeight(locationOnDeck);

                //Compares the two weights
                if (weightLeft / halfOfCargoShip <= weightRight / halfOfCargoShip) // Left Side
                {
                    columnXCoord = 0;
                    forLoopMax = halfOfCargoShip;
                }
                else if (weightRight / halfOfCargoShip <= weightMiddle) // Right Side
                {
                    columnXCoord = halfOfCargoShip + 1;
                    forLoopMax = cargoShip.width;
                }
                else // In the middle
                {
                    columnXCoord = halfOfCargoShip;
                    forLoopMax = halfOfCargoShip + 1;
                }
            }

            //Checks if X is legit.
            for (int i = columnXCoord; i < forLoopMax; i++)
            {
                if (terminatedX.Contains(columnXCoord))
                {
                    columnXCoord++;
                    //IF ALL SLOTS ARE TERMINATED RETURN ERROR
                    if (columnXCoord >= forLoopMax) return -1;
                }
            }

            //Gets the least heavy container from the selected side.      
            minWeight = GetTotalWeight(logistics.gridLocation.Keys.Where(location => location.x == columnXCoord && location.z == zCoord).First());
            for (int i = columnXCoord; i < forLoopMax; i++)
            {
                // Only check the legit coordinates.
                if (!terminatedX.Contains(i))
                {
                    int newWeight = GetTotalWeight(logistics.gridLocation.Keys.Where(location => location.x == i && location.z == zCoord).First());
                    if (newWeight < minWeight)
                    {
                        minWeight = newWeight;
                        columnXCoord = i;
                    }
                }
            }

            return columnXCoord;
        }
        #endregion

        #region Stack Termination
        /// <summary>
        /// Checks if the container can be placed on top of the other containers.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="valuableCheck"></param>
        /// <param name="containerToAdd"></param>
        /// <returns></returns>
        public bool CheckToTerminate(GridLocation location, bool valuableCheck, Container containerToAdd)
        {
            //IF NEED TO ACCOUNT FOR VALUABLE CONTAINERS
            if (valuableCheck)
            {
                if (GetTotalWeight(location) + containerToAdd.weight > 120)
                {
                    return true;
                }
                return false;
            }

            //OTHERWISE
            else
            {
                if (GetTotalWeight(location) + containerToAdd.weight > 150)
                {
                    return true;
                }

                //CHECK IF THERE ISNT ANOTHER VALUABLE CONTAINER ON TOP OF ANOTHER
                if (logistics.gridLocation[location].Count != 0)
                {
                    Container topContainer = logistics.gridLocation[location][logistics.gridLocation[location].Count - 1];
                    if (topContainer.containerType == ContainerType.Valuable)
                    {
                        return true;
                    }
                }

                return false;
            }

        }
        #endregion

        #region Weight Distribution
        /// <summary>
        /// Checks if the weight of the ship is distributed equally
        /// </summary>
        /// <param name="gridLocations"></param>
        /// <returns></returns>
        public double CheckWeightDistribution(List<GridLocation> gridLocations)
        {       
            #region Local Variables
            GridLocation locationOnDeck;
            double weightLeft = 0, weightRight = 0;
            int halfOfCargoShip = (cargoShip.width - 1) / 2;
            #endregion

            #region Left side of the deck
            for (int zCoord = 0; zCoord < cargoShip.length; zCoord++)
            {
                if (cargoShip.width % 2 == 0)
                {
                    for (int i = 0; i < cargoShip.width / 2; i++)
                    {
                        locationOnDeck = gridLocations.Find(tempSlot => tempSlot.x == i && tempSlot.z == zCoord);
                        weightLeft += GetTotalWeight(locationOnDeck);
                    }
                }
                else
                {
                    for (int i = 0; i < halfOfCargoShip; i++)
                    {
                        locationOnDeck = gridLocations.Find(tempSlot => tempSlot.x == i && tempSlot.z == zCoord);
                        weightLeft += GetTotalWeight(locationOnDeck);
                    }
                }
            }
            #endregion

            #region Right side of the deck
            for (int zCoord = 0; zCoord < cargoShip.length; zCoord++)
            {
                if (cargoShip.width % 2 == 0)
                {
                    for (int i = cargoShip.width / 2; i < cargoShip.width; i++)
                    {
                        locationOnDeck = gridLocations.Find(tempSlot => tempSlot.x == i && tempSlot.z == zCoord);
                        weightRight += GetTotalWeight(locationOnDeck);
                    }
                }
                else
                {
                    for (int i = halfOfCargoShip + 1; i < cargoShip.width; i++)
                    {
                        locationOnDeck = gridLocations.Find(tempSlot => tempSlot.x == i && tempSlot.z == zCoord);
                        weightRight += GetTotalWeight(locationOnDeck);
                    }
                }
            }
            #endregion

            #region Calculates and returns the Weight Distribution
            double totalWeight = weightLeft + weightRight;
            double weightDifference = Math.Abs(weightLeft - weightRight);
            return weightDifference / totalWeight * 100;
            #endregion
        }

        #region Returns the Total Weight on a location.
        public int GetTotalWeight(GridLocation location)
        {
            return logistics.gridLocation[location].Sum(i => i.weight);
        }
        #endregion
        #endregion

        #region Create containers
        /// <summary>
        /// Creates a new container and adds it to the list of containers that need to get placed on the CargoShip.
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="cooled"></param>
        /// <param name="valuable"></param>
        public void CreateContainer(int weight, string type)
        {
            weight += 4;
            string id = Guid.NewGuid().ToString();
            if (!ExceedsMaxWeight(weight))
            {
                Enum.TryParse(type, out ContainerType containerType);
                Container container = new Container(id, weight, containerType);
                logisticsForm.cargoDeckBox.Items.Add(container.ToString());
                logisticsForm.shipLogic.AddContainerToCargoShip(container);
            }
        }
        #endregion

        #region Checks if the maximum weight of a container is exceeded.
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
        #endregion

        #region Generate Random Containers
        /// <summary>
        /// Generates random containers 
        /// </summary>
        public void GenerateRandomContainers()
        {
            #region Variables
            int shipLength = cargoShip.length;
            int shipWidth = cargoShip.width;
            int tempWeight = 0;
            Random random = new Random();
            #endregion

            #region Create Random Cooled Containers
            int cooledWeight = 0;
            while (cooledWeight < shipWidth * 150 - random.Next(60, shipWidth * 30))
            {
                tempWeight = random.Next(4, 30);
                Container container = new Container(Guid.NewGuid().ToString(), tempWeight, ContainerType.Cooled);
                logisticsForm.cargoDeckBox.Items.Add(container.ToString());
                logisticsForm.shipLogic.AddContainerToCargoShip(container);
                cooledWeight += tempWeight;
            }
            #endregion

            #region Create Random Normal Containers
            int normalWeight = 0;
            while (normalWeight < (shipWidth * 120 - random.Next(90, shipWidth * 30)) * shipLength - 1)
            {
                tempWeight = random.Next(4, 30);
                Container container = new Container(Guid.NewGuid().ToString(), tempWeight, ContainerType.Normal);
                logisticsForm.cargoDeckBox.Items.Add(container.ToString());
                logisticsForm.shipLogic.AddContainerToCargoShip(container);
                normalWeight += tempWeight;
            }
            #endregion

            #region Create Random Valuable Containers
            for (int i = 0; i < (shipLength * shipWidth) - random.Next(1, shipWidth * shipLength / 2); i++)
            {
                tempWeight = random.Next(4, 30);
                Container container = new Container(Guid.NewGuid().ToString(), tempWeight, ContainerType.Valuable);
                logisticsForm.cargoDeckBox.Items.Add(container.ToString());
                logisticsForm.shipLogic.AddContainerToCargoShip(container);
            }
            #endregion
        }
        #endregion
    }
}
