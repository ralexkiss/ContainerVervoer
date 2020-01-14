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
                    Label locationBox = new Label
                    {
                        Text = "Stack: " + x + " x " + z + " ",
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Black,
                        BackColor = SystemColors.Menu,
                        Size = new Size(boxWidth, boxHeight),
                        Location = new Point((boxWidth + 5) * x + 5, (boxHeight + 5) * z + 5)

                    };
                    locationBox.Click += new EventHandler(txtBox_Click);
                    Controls.Add(locationBox);
                    z++;
                }
                x++;
                z = 0;
            }
            Label informationLabel = new Label
            {
                Size = new Size(200, 100),
                Location = new Point((boxWidth + 5) * port.Ship.width + 30, 30),
                Text = "CargoShip is safe, filled and ready to go!"
            };
            Controls.Add(informationLabel);
        }

        //Load container information
        private void txtBox_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label != null)
            {
                int x = int.Parse(label.Text.Substring(7, 2));
                int z = int.Parse(label.Text.Substring(11, 2));

                Row row = port.Ship.Rows[x];
                Stack stack = row.Stacks[z];

                MessageBox.Show(stack.ToString(), "Location: " + x + " - " + z, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
