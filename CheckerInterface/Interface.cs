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
        Board berd;
        Move cur;
        char turn;
        Bitmap red, black;


        public Interface()
        {
            InitializeComponent();
            radioHuman1.Checked = true;
            radioHuman2.Checked = true;
            red = new Bitmap(CheckerInterface.Properties.Resources.Red);
            black = new Bitmap(CheckerInterface.Properties.Resources.Black);

        }

        ///////////////////////////////////////////////
        
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
            MessageBox.Show("ValidMoves "+berd.validMoves.Count());
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    //if 0 does on cur.s
                    //if 1 does on cur.d
                    PictureBox spot = this.Controls.Find(("spot" + r + "" + c), true).FirstOrDefault() as PictureBox;
                    if (spot != null)
                    {
                        switch(val)
                        {
                            case 0:
                                if(berd.b[r,c] == 1)
                                {
                                    if (berd.validMoves != null)
                                    {
                                        foreach (Move m in berd.validMoves)
                                        {
                                            //MessageBox.Show(r + ", " + c);
                                            if (m.s.r == r && m.s.c == c)
                                            {
                                                spot.Enabled = true;
                                                spot.BackColor = Color.LimeGreen;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 1:
                                if (berd.b[r, c] == 0)
                                {
                                    if (berd.validMoves != null)
                                    {
                                        foreach (Move m in berd.validMoves)
                                        {
                                            //MessageBox.Show(r + ", " + c);
                                            if (m.s.r == cur.s.r && m.s.c == cur.s.c && m.d.r == r && m.d.c == c)
                                            {
                                                spot.Enabled = true;
                                                spot.BackColor = Color.LimeGreen;
                                            }
                                        }
                                    }
                                }
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
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

        void placeCheckers()
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

        private void browseBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            browseBox2.Text = openFileDialog1.FileName;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            berd = new Board();
            berd.getBoard(ref berd.b);
            this.placeCheckers();

            this.disableAll();
            this.enableValids(0);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

        }

        private void click(int r, int c)
        {
            //MessageBox.Show("spot " + r + ", " + c);
            foreach (Move m in berd.validMoves)
            {
                PictureBox spots = this.Controls.Find(("spot" + m.s.r + "" + m.s.c), true).FirstOrDefault() as PictureBox;
                PictureBox spotd = this.Controls.Find(("spot" + m.d.r + "" + m.d.c), true).FirstOrDefault() as PictureBox;
                if (spots != null && spotd != null)
                {
                    if (m.s.r == r && m.s.c == c)
                    {
                        spots.Enabled = false;
                        spots.BackColor = Color.Cyan;
                        cur.s = new Point(r,c);
                    }
                    else
                    {
                        spots.BackColor = Color.Tan;
                    }
                }
            }
            this.enableValids(1);
        }

        private void spot01_Click(object sender, EventArgs e)
        {
            click(0, 1);
        }

        private void spot03_Click(object sender, EventArgs e)
        {
            click(0, 3);
        }

        private void spot05_Click(object sender, EventArgs e)
        {
            click(0, 5);
        }

        private void spot07_Click(object sender, EventArgs e)
        {
            click(0, 7);
        }

        private void spot10_Click(object sender, EventArgs e)
        {
            click(1, 0);
        }

        private void spot12_Click(object sender, EventArgs e)
        {
            click(1, 2);
        }

        private void spot14_Click(object sender, EventArgs e)
        {
            click(1, 4);
        }

        private void spot16_Click(object sender, EventArgs e)
        {
            click(1, 6);
        }

        private void spot21_Click(object sender, EventArgs e)
        {
            click(2, 1);
        }

        private void spot23_Click(object sender, EventArgs e)
        {
            click(2, 3);
        }

        private void spot25_Click(object sender, EventArgs e)
        {
            click(2, 5);
        }

        private void spot27_Click(object sender, EventArgs e)
        {
            click(2, 7);
        }

        private void spot30_Click(object sender, EventArgs e)
        {
            click(3, 0);
        }

        private void spot32_Click(object sender, EventArgs e)
        {
            click(3, 2);
        }

        private void spot34_Click(object sender, EventArgs e)
        {
            click(3, 4);
        }

        private void spot36_Click(object sender, EventArgs e)
        {
            click(3, 6);
        }

        private void spot41_Click(object sender, EventArgs e)
        {
            click(4, 1);
        }

        private void spot43_Click(object sender, EventArgs e)
        {
            click(4, 3);
        }

        private void spot45_Click(object sender, EventArgs e)
        {
            click(4, 5);
        }

        private void spot47_Click(object sender, EventArgs e)
        {
            click(4, 7);
        }

        private void spot50_Click(object sender, EventArgs e)
        {
            click(5, 0);
        }

        private void spot52_Click(object sender, EventArgs e)
        {
            click(5, 2);
        }

        private void spot54_Click(object sender, EventArgs e)
        {
            click(5, 4);
        }

        private void spot56_Click(object sender, EventArgs e)
        {
            click(5, 6);
        }

        private void spot61_Click(object sender, EventArgs e)
        {
            click(6, 1);
        }

        private void spot63_Click(object sender, EventArgs e)
        {
            click(6, 3);
        }

        private void spot65_Click(object sender, EventArgs e)
        {
            click(6, 5);
        }

        private void spot67_Click(object sender, EventArgs e)
        {
            click(6, 7);
        }

        private void spot70_Click(object sender, EventArgs e)
        {
            click(7, 0);
        }

        private void spot72_Click(object sender, EventArgs e)
        {
            click(7, 2);
        }

        private void spot74_Click(object sender, EventArgs e)
        {
            click(7, 4);
        }

        private void spot76_Click(object sender, EventArgs e)
        {
            click(7, 6);
        }
    }
}
