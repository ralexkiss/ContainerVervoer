using ContainerVervoer.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerVervoer.Logic
{
    public class CreationLogic
    {
        CreationForm creation;

        public CreationLogic(CreationForm creation)
        {
            this.creation = creation;
        }

        public void CreateShip()
        {
            int shipsWidth = Convert.ToInt32(creation.shipWidthValue.Value);
            int shipsLenght = Convert.ToInt32(creation.shipLengthValue.Value);
            if (shipsWidth >= 3 && shipsLenght >= 3)
            {
                LogisticsForm logistics = new LogisticsForm(new ShipLogic(shipsWidth, shipsLenght));
                logistics.Show();
                creation.Hide();
                return;
            }
            else
            {
                MessageBox.Show("Please increase the size of the ship. Length & Width can't be smaller than 3!", "Incorrect Size", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
