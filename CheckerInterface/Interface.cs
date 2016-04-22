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
        Bitmap red, black;
        int player1, player2;


        public Interface()
        {
            InitializeComponent();
            radioHuman1.Checked = true;
            radioHuman2.Checked = true;
            red = new Bitmap(CheckerInterface.Properties.Resources.Red);
            black = new Bitmap(CheckerInterface.Properties.Resources.Black);

        }
        
        void go()
        {
            berd.getBoard(ref berd.b);
            this.placeCheckers();

            this.disableAll();
            this.enableValids(0);
        }

        void enableValids(int val)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    PictureBox spot = this.Controls.Find(("spot" + r + "" + c), true).FirstOrDefault() as PictureBox;
                    if(berd.turn == 1)
                    {
                        spot = this.Controls.Find(("spot" + (7-r) + "" + (7-c)), true).FirstOrDefault() as PictureBox;
                    }

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
                                            if (m.s.r == r && m.s.c == c && spot.BackColor != Color.Cyan)
                                            {
                                                spot.Enabled = true;
                                                spot.BackColor = Color.Green;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 1:
                                // this enables the spaces that are the valid moves for the checker in cur.s
                                if (berd.b[r, c] == 0)
                                {
                                    if (berd.validMoves != null)
                                    {
                                        foreach (Move m in berd.validMoves)
                                        {
                                            if (m.s.r == cur.s.r && m.s.c == cur.s.c && m.d.r == r && m.d.c == c)
                                            {
                                                spot.Enabled = true;
                                                spot.BackColor = Color.LimeGreen;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                // this is basically a disableValids( previous move )
                                foreach (Move m in berd.validMoves)
                                {
                                        spot.Enabled = false;
                                        spot.BackColor = Color.Tan;
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
            if (berd.turn == 1) { berd.rotate180(); }
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                { 
                    PictureBox spot = this.Controls.Find(("spot" + r + "" + c), true).FirstOrDefault() as PictureBox;
                    if (spot != null)
                    {
                        if(berd.turn == 1)
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
                        else
                        {
                            if (berd.b[r, c] == 1)
                            {
                                spot.BackgroundImage = red;
                            }
                            else if (berd.b[r, c] == -1)
                            {
                                spot.BackgroundImage = black;
                            }
                            else
                            {
                                spot.BackgroundImage = null;
                            }
                        }

                        spot.BackColor = Color.Tan;
                    }

                }
            }
            if (berd.turn == 1){ berd.rotate180(); }
        }

        private void radioHuman1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer1.Checked)
            {
                player1 = 1;
                label1.Show();
                browseBox1.Show();
            }
            else
            {
                player1 = 0;
                label1.Hide();
                browseBox1.Hide();
            }
        }

        private void radioHuman2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer2.Checked)
            {
                player2 = 1;
                label2.Show();
                browseBox2.Show();
            }
            else
            {
                player2 = 0;
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
            
            this.go();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            berd.b = new int[8, 8]  {{9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9}};
            this.placeCheckers();
            this.disableAll();
        }

        private void click(int r, int c)
        {
            if (berd.turn == 1)
            {
                r = 7 - r;
                c = 7 - c;
            }
            if (berd.validMoves == null)
            { MessageBox.Show("you done did it now"); }

            bool moved = false;

            List<Move> temp = new List<Move>(berd.validMoves);
            foreach (Move m in temp)
            {
                PictureBox spots = this.Controls.Find(("spot" + m.s.r + "" + m.s.c), true).FirstOrDefault() as PictureBox;
                PictureBox spotd = this.Controls.Find(("spot" + m.d.r + "" + m.d.c), true).FirstOrDefault() as PictureBox;

                if (berd.turn == 1)
                {
                    spots = this.Controls.Find(("spot" + (7-m.s.r) + "" + (7-m.s.c)), true).FirstOrDefault() as PictureBox;
                    spotd = this.Controls.Find(("spot" + (7-m.d.r) + "" + (7-m.d.c)), true).FirstOrDefault() as PictureBox;
                }

                if (spots != null && spotd != null)
                {
                    if (m.s.r == r && m.s.c == c) // if click on checker
                    {
                        if (cur.s.r == 0 && cur.s.c == 0) // if this is first time click checker
                        {
                            spots.Enabled = false;
                            spots.BackColor = Color.Cyan;
                            cur.s = new Point(r, c);

                            this.enableValids(1);
                        }
                        else // you're changing checker that you want to move, cur.s
                        {
                            this.enableValids(2);

                            spots.Enabled = false;
                            spots.BackColor = Color.Cyan;
                            cur.s = new Point(r, c);

                            this.enableValids(0);
                            this.enableValids(1);
                        }
                    }
                    else if (m.d.r == r && m.d.c == c) // if click in move spot
                    {
                        
                        cur.d = new Point(r, c);
                        if(berd.putMove(cur.s, cur.d)) // if you have a second jump
                        {
                            this.enableValids(2);

                            this.placeCheckers();
                            spotd.BackColor = Color.Cyan;
                            cur.s = cur.d;
                            cur.d = new Point();

                            this.enableValids(1);
                        }
                        else // you're done
                        {
                            this.enableValids(2);
                            moved = true;
                            cur = new Move();
                        }
                        //this.go();
                    }
                    else
                    {
                        //  This is just a catch. There should never be a time where 
                        //  click is called and it isn't in a green spot
                        //  due to how we enable and disable positions.
                        
                    }
                }
            }

            if(moved)
            {
                berd.getBoard(ref berd.b);
                this.placeCheckers();
                this.enableValids(0);
            }
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
