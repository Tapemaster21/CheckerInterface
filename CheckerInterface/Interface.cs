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
            red = new Bitmap(CheckerInterface.Properties.Resources.Red);
            black = new Bitmap(CheckerInterface.Properties.Resources.Black);

            berd.getBoard(ref berd.b);
            updateVisualBoard();
            
        }

        ///////////////////////////////////////////////

        Board berd = new Board();
        Move cur;
        char turn;
        Bitmap red, black;


        /*
        void humanTurn()
        {
            berd.getBoard(ref berd.b);
            
            this.enableValids(0);

            //while(!globalClick1){}
            
            this.enableValids(1); //does it on cur.s

            Point temp = click2(); //wait for click event


            if (berd.b[temp.r, temp.c] == 1)
            {
                cur.s = temp;
                disableAll();
                enableValids(0);
                enableValids(1);
                updateVisualBoard()
            }
	        else
	        {

                cur.d = click2; //wait for click event
		        while( berd.putMove(cur.s, cur.d) )
		        {
			        cur.s = cur.d;
			        //cur.d = new Point();
			        disableAll();
			        enableValids(1);
			        cur.d = click2; //wait for click event
                    updateVisualBoard()
		        }
	        }
            
        }
        */
        ///////////////////////////////////////////////

        void enableValids(int val)
        {

        }
        
        void disableAll()
        {
            spot01.Enabled = false;
            spot03.Enabled = false;
            spot05.Enabled = false;
            spot07.Enabled = false;

            spot10.Enabled = false;
            spot12.Enabled = false;
            spot14.Enabled = false;
            spot16.Enabled = false;

            spot21.Enabled = false;
            spot23.Enabled = false;
            spot25.Enabled = false;
            spot27.Enabled = false;

            spot30.Enabled = false;
            spot32.Enabled = false;
            spot34.Enabled = false;
            spot36.Enabled = false;

            spot41.Enabled = false;
            spot43.Enabled = false;
            spot45.Enabled = false;
            spot47.Enabled = false;

            spot50.Enabled = false;
            spot52.Enabled = false;
            spot54.Enabled = false;
            spot56.Enabled = false;

            spot61.Enabled = false;
            spot63.Enabled = false;
            spot65.Enabled = false;
            spot67.Enabled = false;

            spot70.Enabled = false;
            spot72.Enabled = false;
            spot74.Enabled = false;
            spot76.Enabled = false;
        }

        void updateVisualBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                { 
                    PictureBox spot = this.Controls.Find(("spot" + r + "" + c), true).FirstOrDefault() as PictureBox;
                    if (spot != null)
                    {
                        if (berd.b[r, c] == -1)
                        {
                            spot.BackgroundImage = red;
                        }
                        else if (berd.b[r, c] == 1)
                        {
                            spot.BackgroundImage = black;
                        }
                        else
                        {
                            spot.BackgroundImage = null;
                        }
                    }
                }
            }
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

        private void spot01_Click(object sender, EventArgs e)
        {

        }

        private void spot03_Click(object sender, EventArgs e)
        {

        }
    }
}
