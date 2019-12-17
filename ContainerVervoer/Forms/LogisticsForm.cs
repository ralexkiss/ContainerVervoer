using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContainerVervoer.Models;
using ContainerVervoer.Logic;

namespace ContainerVervoer.Forms
{
    public partial class LogisticsForm : Form
    {
        public ShipLogic shipLogic;
        public LogisticsLogic logisticsLogic;

        public LogisticsForm(ShipLogic shipLogic)
        {
            InitializeComponent();
            CenterToScreen();
          
            this.shipLogic = shipLogic;
            logisticsLogic = new LogisticsLogic(this);

            //INITIALIZE
            ShipSizeLabel.Text = shipLogic.cargoShip.width.ToString() + " x " + shipLogic.cargoShip.length.ToString();
        }

        /// <summary>
        /// Creates Container with the inserted and selected values.
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmContainerButton_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(addContainerWeight.Text, out int weight))
            {
                logisticsLogic.CreateContainer(weight, cooledCheckbox.Checked, valuableCheckbox.Checked);
            }
            else
            {
                MessageBox.Show("Please enter a valid weight");
            }
        }

        /// <summary>
        /// Deletes the selecter container.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteContainerButton_Click(object sender, EventArgs e)
        {
            cargoDeckBox.Items.Remove(cargoDeckBox.SelectedItem);
            shipLogic.cargoShip.logistics.containersToPlace.Remove(shipLogic.cargoShip.logistics.containersToPlace.Find(i => i.id == cargoDeckBox.SelectedIndex.ToString()));
        }

        /// <summary>
        /// Adds random generated containers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            if (logisticsLogic.StartSorting())
            {
                CargoShipForm cargoShip = new CargoShipForm();
                cargoShip.Show();
                this.Hide();
                return;
            }
        }
    }
}
