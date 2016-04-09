using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerInterface
{

    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
            radioHuman1.Checked = true;
            radioHuman2.Checked = true;
        }

        private void radioComputer2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioHuman1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer1.Checked)
            {
                label1.Show();
                browseBox1.Show();
            }
            else
            {
                label1.Hide();
                browseBox1.Hide();
            }
        }

        private void radioHuman2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer2.Checked)
            {
                label2.Show();
                browseBox2.Show();
            }
            else
            {
                label2.Hide();
                browseBox2.Hide();
            }
        }

        private void browseBox1_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
            browseBox1.Text = openFileDialog1.FileName;
        }

        private void pictureBox45_Click(object sender, EventArgs e)
        {

        }
    }
}
