using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsappMassive.Vista;

namespace WhatsappMassive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContacto fc = new frmContacto();
            fc.MdiParent = this;
            fc.Show();
            hideIcons();
        }

        private void campañasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCampana fc = new frmCampana();
            fc.MdiParent = this;
            fc.Show();
            hideIcons();
            
        }

        public void showIcons()
        {
            pictureBox1.Show();
            label1.Show();
            label2.Show();
        }
        public void hideIcons()
        {
            pictureBox1.Hide();
            label1.Hide();
            label2.Hide();
        }
        
    }
}
