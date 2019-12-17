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
    public partial class CreationForm : Form
    {
        CreationLogic creationLogic;

        public CreationForm()
        {
            InitializeComponent();
            creationLogic = new CreationLogic(this);
        }

        private void CreateShipBtn_Click(object sender, EventArgs e)
        {
            creationLogic.CreateShip();                 
        }
    }
}
