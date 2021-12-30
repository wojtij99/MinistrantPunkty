using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinistrantKatedra3
{
    public partial class frmEsterEgg : Form
    {
        public frmEsterEgg(int x)
        {
            InitializeComponent();

            switch (x)
            {
                case 0:
                    pictureBox1.ImageLocation = "Pablo.jpg";
                    break;
                case 1:
                    pictureBox1.ImageLocation = "Darius.jpg";
                    break;
                case 2:
                    pictureBox1.ImageLocation = "Adamy.jpg";
                    break;
            }
        }
    }
}
