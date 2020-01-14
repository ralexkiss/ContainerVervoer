using System;
using System.Windows.Forms;
using ContainerVervoer.Models;
using ContainerVervoer.Enums;

namespace ContainerVervoer.Forms
{
    public partial class LogisticsForm : Form
    {
        Port port;
        public LogisticsForm(Port port)
        {
            InitializeComponent();
            CenterToScreen();

            this.port = port;

            ShipSizeLabel.Text = port.Ship.length + " x " + port.Ship.width;
            containerType.DataSource = Enum.GetValues(typeof(ContainerType));
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
                Container container = new Container(weight, (ContainerType)containerType.SelectedValue);
                cargoDeckBox.Items.Add(container.ToString());
                port.containersToPlace.Add(container);

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
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            /*if (startsorting)
            {
                CargoShipForm cargoShip = new CargoShipForm(this);
                cargoShip.Show();
                this.Hide();
                return;
            }*/
        }

        private void GMDButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
