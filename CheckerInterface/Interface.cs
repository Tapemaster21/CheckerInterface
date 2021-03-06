﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace CheckerInterface
{

    public partial class Interface : Form
    {
        Board berd; 
        Move cur; // current move being performed
        Bitmap red, black, redK, blackK;
        bool player1 = false; // these tell if the player is human
        bool player2 = false;
        int turnTime; // in milliseconds


        public Interface()
        {
            InitializeComponent();
            System.IO.File.Create("./move.txt").Close();
            System.IO.File.Create("./board.txt").Close();

            radioHuman1.Checked = true;
            radioHuman2.Checked = true;
            red = new Bitmap(CheckerInterface.Properties.Resources.Red);
            black = new Bitmap(CheckerInterface.Properties.Resources.Black);
            redK = new Bitmap(CheckerInterface.Properties.Resources.RedK);
            blackK = new Bitmap(CheckerInterface.Properties.Resources.BlackK);
            buttonReset.Enabled = false;
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
                                if(berd.b[r,c] == 1 || berd.b[r, c] == 2)
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
                            else if (berd.b[r, c] == -2)
                            {
                                spot.BackgroundImage = redK;
                            }
                            else if (berd.b[r, c] == 2)
                            {
                                spot.BackgroundImage = blackK;
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
                            else if (berd.b[r, c] == 2)
                            {
                                spot.BackgroundImage = redK;
                            }
                            else if (berd.b[r, c] == -2)
                            {
                                spot.BackgroundImage = blackK;
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
            this.Refresh();
        }
        
        private void click(int r, int c)
        {
           
            if (berd.turn == 1 && player2)
            {
                r = 7 - r;
                c = 7 - c;
            }
                        
            List<Move> temp = new List<Move>(berd.validMoves);
            foreach (Move m in temp)
            {
                //MessageBox.Show(r+","+c+": "+m.s.r+","+m.s.c+" "+m.d.r+","+m.d.c);

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
                            if ((!player1 && berd.turn == -1) || (!player2 && berd.turn == 1)) // if ur robit
                            {
                                // robits dont need to change checkers, they're infallable beings 
                                // containing all knowledge, and thusly do not need to click the wrong checker intitally. 
                                MessageBox.Show((berd.turn == 1 ? "Player 2 " : "Player 1 ") + "won because " + (berd.turn == -1 ? "Player 2 " : "Player 1 ") + "made an invalid move.");
                                return;
                            }
                            else // youre a human, so you mess up alllll the time
                            {
                                this.enableValids(2);

                                spots.Enabled = false;
                                spots.BackColor = Color.Cyan;
                                cur.s = new Point(r, c);

                                this.enableValids(0);
                                this.enableValids(1);
                            }
                        }
                        return;
                    }
                    else if (m.d.r == r && m.d.c == c) // if click in move spot
                    {
                        cur.d = new Point(r, c);
                        int banana = berd.putMove(cur.s, cur.d);
                        if (banana == 1) // if you have a second jump
                        {
                            if ((!player1 && berd.turn == -1) || (!player2 && berd.turn == 1)) // if a robit
                            {
                                this.placeCheckers();
                                cur.s = cur.d;
                                cur.d = new Point();
                                return;
                            }
                            else // human turn
                            {
                                this.enableValids(2);

                                this.placeCheckers();
                                spotd.BackColor = Color.Cyan;
                                cur.s = cur.d;
                                cur.d = new Point();

                                this.enableValids(1);
                                return;
                            }
                        }
                        else if (banana == 0) // if no second jump
                        {
                            this.enableValids(2);

                            cur = new Move();
                            this.placeCheckers();
                            if (berd.over())
                            {
                                string player = (berd.turn == 1 ? "Player 2 " : "Player 1 ");
                                MessageBox.Show(player + "won!");
                                buttonReset.Focus();
                                return;
                            }
                            else
                            {
                                changeTurn();
                            }
                            return;
                        }
                        else // you made invalid move ya dingus
                        {
                            MessageBox.Show((berd.turn == 1 ? "Player 2 " : "Player 1 ") + "won because " + (berd.turn == -1 ? "Player 2 " : "Player 1 ") + "made an invalid move.");
                            return;
                        }

                    }
                }
            }
            // Your clicks were made on something that isn't a move place or a checker and you messed up somehow. 
            MessageBox.Show((berd.turn == 1 ? "Player 2 " : "Player 1 ") + "won because " + (berd.turn == -1 ? "Player 2 " : "Player 1 ") + "made an invalid move.");
        }

        private void processStart(int number)
        {
            System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo();
            startinfo.FileName = (number == 1 ? browseBox1.Text : browseBox2.Text);
            startinfo.UseShellExecute = (number == 1 ? check1.Checked : check2.Checked);
            //startinfo.RedirectStandardOutput = (number == 1 ? !check1.Checked : !check2.Checked);
            startinfo.CreateNoWindow = (number == 1 ? !check1.Checked : !check2.Checked);
            using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(startinfo))
            {
                if (!proc.WaitForExit(turnTime))
                {
                    proc.Kill();
                    MessageBox.Show("Player " + (number == 1 ? "2" : "1") + " wins!\nPlayer " + number + " took too long on their turn.");
                    return;
                }
            }
            inputAI();
        }

        private void changeTurn()
        {            
            berd.getBoard(ref berd.b);
            this.placeCheckers();
            if (!player1 && berd.turn == -1)
            {
                processStart(1);
            }
            else if (!player2 && berd.turn == 1)
            {
                processStart(2);
            }
            else
            {
                this.enableValids(0);
            }
            if (berd.validMoves.Count == 0)
            {
                MessageBox.Show("You have no valid moves.\n You must pass your turn.");
                changeTurn();
                return;
            }
        }

        private void inputAI()
        {
            if (File.ReadAllLines("./move.txt").Count() == 0) // if moves.txt is empty
            {
                MessageBox.Show("ERROR! No lines read from moves.txt", "ERROR",MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            else // otherwise normal
            { 
                string line = File.ReadAllLines("./move.txt")[0];

                if (line != "pass")
                {
                    //s.r,s.c:d.r,d.c
                    string[] split = line.Split(':');
                    int r, c;
                    foreach (string s in split)
                    {
                        r = Convert.ToInt32(s.Split(',')[0]);
                        c = Convert.ToInt32(s.Split(',')[1]);

                        this.click(r, c);
                    }
                }
                else //the player has passed
                {
                    this.enableValids(2);
                    cur = new Move();
                    changeTurn();
                }
            }
        }


        //---------------BUTTONS-----------//

        private void radioHuman1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer1.Checked)
            {
                label3.Visible = true;
                comboBox1.Visible = true;
                turnTime = comboBox1.Text == "Unlimited" ? -1 : (Convert.ToInt32(comboBox1.Text) * 1000);

                player1 = false;
                label1.Show();
                browseBox1.Show();
                check1.Show();
            }
            else
            {
                if(!radioComputer2.Checked)
                {
                    label3.Visible = false;
                    comboBox1.Visible = false;
                }
                player1 = true;
                label1.Hide();
                browseBox1.Hide();
                check1.Hide();
            }
            
        }

        private void radioHuman2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer2.Checked)
            {
                label3.Visible = true;
                comboBox1.Visible = true;
                turnTime = comboBox1.Text == "Unlimited" ? -1 : (Convert.ToInt32(comboBox1.Text) * 1000);

                player2 = false;
                label2.Show();
                browseBox2.Show();
                check2.Show();
            }
            else
            {
                if (!radioComputer1.Checked)
                {
                    label3.Visible = false;
                    comboBox1.Visible = false;
                }
                player2 = true;
                label2.Hide();
                browseBox2.Hide();
                check2.Hide();
            }
        }

        private void browseBox1_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.ShowDialog();
            browseBox1.Text = openFileDialog1.FileName;
        }

        private void browseBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.ShowDialog();
            browseBox2.Text = openFileDialog1.FileName;
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            buttonReset.Enabled = true;
            berd = new Board();

            System.IO.File.Create("./log.txt").Close();

            berd.getBoard(ref berd.b);
            this.placeCheckers();

            this.disableAll();

            if(player1)
            {
                this.enableValids(0);
            }
            else
            {
                processStart(1);
            }


        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            //radioHuman1.Checked = true;
            //radioHuman2.Checked = true;
            //browseBox1.Text = "";
            //browseBox2.Text = "";


            berd = new Board();
                /*
                int[8, 8]  {{9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {0,9,0,9,0,9,0,9}};
                                    */
            this.placeCheckers();
            this.disableAll();
            buttonStart.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            turnTime = comboBox1.Text == "Unlimited" ? -1 : (Convert.ToInt32(comboBox1.Text) * 1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "./log.txt");
        }


        #region spot clicks

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
        #endregion
    }
}
