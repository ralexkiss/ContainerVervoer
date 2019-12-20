using ContainerVervoer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerVervoer.Forms
{
    public partial class CargoShipForm : Form
    {
        LogisticsForm logisticsForm;
        Logistics logistics;
        public CargoShipForm(LogisticsForm logisticsForm)
        {
            this.logisticsForm = logisticsForm;
            this.logistics = logisticsForm.logisticsLogic.cargoShip.logistics;
            InitializeComponent();
        }

        /// <summary>
        /// Loads 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CargoShipForm_Load(object sender, EventArgs e)
        {
            int boxWidth = 75;
            int boxHeight = 50;
            int borderSize = 5;
            int informationWidth = 250;

            Size = new Size(logisticsForm.shipLogic.cargoShip.width * (boxWidth + borderSize) + 20 + informationWidth, logisticsForm.shipLogic.cargoShip.length * (boxHeight + borderSize) + 45);

            //ADD ALL CONTAINER LABELS
            for (int z = 0; z < logisticsForm.shipLogic.cargoShip.length; z++)
            {
                for (int x = 0; x < logisticsForm.shipLogic.cargoShip.width; x++)
                {
                    GridLocation currentSlot = logistics.gridLocation.Keys.Where(location => location.x == x && location.z == z).First();
                    double slotWeight = logistics.gridLocation[currentSlot].Sum(i => i.weight);

                    Label locationBox = new Label();
                    locationBox.Text = currentSlot.ToString();
                    locationBox.TextAlign = ContentAlignment.MiddleCenter;
                    locationBox.ForeColor = Color.Black;
                    locationBox.BackColor = SystemColors.Menu;
                    locationBox.Size = new Size(boxWidth, boxHeight);
                    locationBox.Click += new EventHandler(txtBox_Click);
                    locationBox.Location = new Point((boxWidth + 5) * x + 5, (boxHeight + 5) * z + 5);
                    Controls.Add(locationBox);
                }
            }

            //Add Main informations
            Label informationLabel = new Label();
            double weightDifference = logisticsForm.logisticsLogic.CheckWeightDistribution(logistics.gridLocation.Keys.ToList());
            double weightPercentage = logisticsForm.logisticsLogic.weightPercentage;
            informationLabel.Text = "Total Containers: " + logistics.gridLocation.Values.Count() + "\r\n\r\nCargoShip filled out for " + Math.Round(weightPercentage, 2) + "%\r\n\r\nDistribution Difference is " + Math.Round(weightDifference, 2) + "%\r\n";
            informationLabel.Size = new Size(200, 100);
            informationLabel.Location = new Point((boxWidth + 5) * logisticsForm.shipLogic.cargoShip.width + 30, 30);
            informationLabel.Text += "\r\nCargoShip is safe, filled and ready to go!";
            
            Controls.Add(informationLabel);
        }

        //Load container information
        private void txtBox_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label != null)
            {
                int xCoord = int.Parse(label.Text.Substring(10, 1));
                int zCoord = int.Parse(label.Text.Substring(14, 1));

                GridLocation location = logistics.gridLocation.Keys.Where(locations => locations.x == xCoord && locations.z == zCoord).First();

                string information = logistics.GetInformation(location);
                foreach (var container in logistics.gridLocation[location])
                {
                    information += container.ToString() + "\r\n";
                }

                MessageBox.Show(information, "Location: " + xCoord + " - " + zCoord, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
