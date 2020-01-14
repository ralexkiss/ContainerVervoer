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
        private Port port;
        public CargoShipForm(Port port)
        {
            InitializeComponent();
            this.port = port;
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

            int x = 0;
            int z = 0;

            foreach (Row row in port.Ship.Rows)
            {  
                foreach (Stack stack in row.Stacks)
                {
                    Label locationBox = new Label();
                    locationBox.Text = "Stack: " + x + " x " + z;
                    locationBox.TextAlign = ContentAlignment.MiddleCenter;
                    locationBox.ForeColor = Color.Black;
                    locationBox.BackColor = SystemColors.Menu;
                    locationBox.Size = new Size(boxWidth, boxHeight);
                    locationBox.Click += new EventHandler(txtBox_Click);
                    locationBox.Location = new Point((boxWidth + 5) * x + 5, (boxHeight + 5) * z + 5);
                    Controls.Add(locationBox);
                    z++;
                }
                x++;
                z = 0;
            }

            /*
            for (int x = 0; x < port.Ship.length; x++)
            {
                for (int z = 0; z < port.Ship.width; z++)
                {
                    Label locationBox = new Label();
                    locationBox.Text = "Stack";
                    locationBox.TextAlign = ContentAlignment.MiddleCenter;
                    locationBox.ForeColor = Color.Black;
                    locationBox.BackColor = SystemColors.Menu;
                    locationBox.Size = new Size(boxWidth, boxHeight);
                    locationBox.Click += new EventHandler(txtBox_Click);
                    locationBox.Location = new Point((boxWidth + 5) * x + 5, (boxHeight + 5) * z + 5);
                    Controls.Add(locationBox);
                }
            }*/

            //Add Main informations
            Label informationLabel = new Label();
            informationLabel.Size = new Size(200, 100);
            informationLabel.Location = new Point((boxWidth + 5) * port.Ship.width + 30, 30);
            informationLabel.Text += "\r\nCargoShip is safe, filled and ready to go!";
            Controls.Add(informationLabel);
        }

        //Load container information
        private void txtBox_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label != null)
            {
                int x = int.Parse(label.Text.Substring(7, 1));
                int z = int.Parse(label.Text.Substring(11, 1));

                Row row = port.Ship.Rows[x];
                Stack stack = row.Stacks[z];

                MessageBox.Show(stack.ToString(), "Location: " + x + " - " + z, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
