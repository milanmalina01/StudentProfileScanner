using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentProfileScanner
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void Startup_Load(object sender, EventArgs e)
        {
 
        }

        private void Startup_MouseClick(object sender, MouseEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
