using System;
using System.Collections.Generic;

namespace CheckerInterface
{
    public struct Point
    {
        public int r, c;
        public Point(int row, int col) {
            r = row;
            c = col;
        }
    };

    public struct Move
    {
        public Point s, d;
        public Move(Point src, Point dest) {
            s = src;
            d = dest;
        }
    }

    class Board
    {
        public int turn;
        public int[,] b;
        public List<Move> validMoves;

        public Board()
        {
            this.validMoves = new List<Move> { };
            this.turn = 1;

            this.b = new int[8, 8] {{ 9,-1, 9,-1, 9,-1, 9,-1},
                                    {-1, 9,-1, 9,-1, 9,-1, 9},
                                    { 9,-1, 9,-1, 9,-1, 9,-1},
                                    { 0, 9, 0, 9, 0, 9, 0, 9},
                                    { 9, 0, 9, 0, 9, 0, 9, 0},
                                    { 1, 9, 1, 9, 1, 9, 1, 9},
                                    { 9, 1, 9, 1, 9, 1, 9, 1},
                                    { 1, 9, 1, 9, 1, 9, 1, 9}};

            //this.b = new int[8, 8] {{ 9, 0, 9,-1, 9, 0, 9, 0},
            //                        { 0, 9, 0, 9, 0, 9, 0, 9},
            //                        { 9, 0, 9,-1, 9, 0, 9, 0},
            //                        { 0, 9, 0, 9, 0, 9, 0, 9},
            //                        { 9, 0, 9, 0, 9,-1, 9, 0},
            //                        { 0, 9, 0, 9, 0, 9, 0, 9},
            //                        { 9,-1, 9,-1, 9,-1, 9, 0},
            //                        { 2, 9, 0, 9, 0, 9, 0, 9}};

        }

        public void getBoard(ref int[,] board)
        {
            this.flipBoard();
            this.validMoves.Clear();
            this.fillValidMoves();
            
            this.turn = -this.turn;
            board = this.b;

            // write board to textfile
            string[] lines = new string[8];
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    lines[r] += b[r, c] + " ";
                }
            }
            System.IO.File.WriteAllLines("./board.txt",lines);
            printLog();
        }

        public int putMove(Point s, Point d)
        {
            Move m = new Move(s, d);
            if (this.validMoves.Contains(m))
            {
                this.updateBoard(m);
                if (Math.Abs(s.r - d.r) == 2 && findJumps(m.d))
                {
                    return 1;
                }
            }
            else
            {
                // invalid move, instant game over
                return -1;
            }
            // write board to log
            return 0;
        }

        void flipBoard()
        {
            rotate180();

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    b[r, c] = -(b[r, c]);
                }
            }
        }

        void rotate90()
        {
            int[,] ret = new int[8, 8];

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    ret[i, j] = b[8 - j - 1, i];
                }
            }
            this.b = ret;
        }

        public void rotate180()
        {
            rotate90();
            rotate90();
        }
        
        void fillValidMoves()
        {
            Point p = new Point();

            bool forced = false;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (this.b[r, c] == 1 || this.b[r, c] == 2)
                    {
                        p.r = r;
                        p.c = c;
                        if (forced)
                        {
                            this.findJumps(p);
                        }
                        else
                        {
                            if (this.findJumps(p))
                            {
                                forced = true;
                                this.validMoves.Clear();
                                this.findJumps(p);
                            }
                            else
                            {
                                this.findMoves(p);
                            }
                        }
                    }
                }
            }
        }

        bool findJumps(Point p)
        {
            bool found = false;
            if (this.b[p.r, p.c] == 1 || this.b[p.r, p.c] == 2)
            {
                //check front left diag for oppo and front left  double diag for empty
                if (p.r > 1 && p.c > 1 && (this.b[p.r - 1, p.c - 1] == -1 || this.b[p.r - 1, p.c - 1] == -2) && (this.b[p.r - 2, p.c - 2] == 0))
                {
                    //add Move(piece, that double diag)
                    validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 2, p.c - 2)));
                    found = true;
                }

                //check front right diag for oppo and front right double diag for empty
                if (p.r > 1 && p.c < 6 && (this.b[p.r - 1, p.c + 1] == -1 || this.b[p.r - 1, p.c + 1] == -2) && (this.b[p.r - 2, p.c + 2] == 0))
                {
                    //add Move(piece, that double diag)
                    validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 2, p.c + 2)));
                    found = true;
                }
            }
            
            if (this.b[p.r, p.c] == 2)
            {
                //check back diag left  for oppo and back double diag left  for empty
                if (p.r < 6 && p.c > 1 && (this.b[p.r + 1, p.c - 1] == -1 || this.b[p.r + 1, p.c - 1] == -2) && (this.b[p.r + 2, p.c - 2] == 0))
                {
                    //add Move(piece, that double diag)
                    validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 2, p.c - 2)));
                    found = true;
                }

                //check back diag right for oppo and back double diag right for empty
                if (p.r < 6 && p.c < 6 && (this.b[p.r + 1, p.c + 1] == -1 || this.b[p.r + 1, p.c + 1] == -2) && (this.b[p.r + 2, p.c + 2] == 0))
                {
                    //add Move(piece, that double diag)
                    validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 2, p.c + 2)));
                    found = true;
                }
            }
            return found;
        }

        bool findMoves(Point p)
        {

            bool found = false;

            if (this.b[p.r, p.c] == 1 || this.b[p.r, p.c] == 2)
            {
                //check front left diag for empty
                if (p.r > 0 && p.c > 0 && this.b[p.r - 1, p.c - 1] == 0)
                {
                    //add Move(piece, that double diag)
                    this.validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 1, p.c - 1)));
                    found = true;
                }

                //check front right diag for empty
                if (p.r > 0 && p.c < 7 && this.b[p.r - 1, p.c + 1] == 0)
                {
                    //add Move(piece, that double diag)
                    this.validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 1, p.c + 1)));
                    found = true;
                }

            }

            if (this.b[p.r, p.c] == 2)
            {
                //check back diag left for empty
                if (p.r < 7 && p.c > 0 && this.b[p.r + 1, p.c - 1] == 0)
                {
                    //add Move(piece, that double diag)
                    this.validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 1, p.c - 1)));
                    found = true;
                }

                //check back diag right for empty
                if (p.r < 7 && p.c < 7 && this.b[p.r + 1, p.c + 1] == 0)
                {
                    //add Move(piece, that double diag)
                    this.validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 1, p.c + 1)));
                    found = true;
                }
            }
                
            return found;
        }

        void updateBoard(Move m)
        {
            if (Math.Abs(m.d.r - m.s.r) == 1)
            {
                b[m.d.r, m.d.c] = b[m.s.r, m.s.c];
                b[m.s.r, m.s.c] = 0;
            }
            else if (Math.Abs(m.d.r - m.s.r) == 2)
            {
                b[m.d.r, m.d.c] = b[m.s.r, m.s.c];
                b[m.s.r, m.s.c] = 0;
                int kr = (m.d.r - m.s.r) / 2 + m.s.r;
                int kc = (m.d.c - m.s.c) / 2 + m.s.c;

                b[kr, kc] = 0;
            }

            if (m.d.r == 0 || m.d.r == 7)
            {
                b[m.d.r, m.d.c] = 2;
            }
        }

        public bool over()
        {
            //check if pieces from both players exist
            bool red = false;
            bool black = false;
            bool over = false;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (this.b[r,c] == -1 || this.b[r, c] == -2)
                    {
                        red = true;
                    }
                    else if (this.b[r, c] == 1 || this.b[r, c] == 2)
                    {
                        black = true;
                    }
                }
            }

            if (!red || !black)
            {
                over = true;
            }

            return over;
            
        }

        void printLog()
        {
            if(turn == 1) { this.flipBoard(); }
            string[] lines = new string[13];
            lines[0] = "╔════════════════════════╗";
            for (int r = 1; r < 9; r++)
            {
                lines[r] += "║";
                for (int c = 0; c < 8; c++)
                {
                    int thing = this.b[r-1, c];
                    if (thing == 9 || thing == -9)
                    {
                        lines[r] += "███";
                    }
                    else if (thing == 0)
                    {
                        lines[r] += "   ";
                    }
                    else if (thing == 1)
                    {
                        lines[r] += " r ";
                    }
                    else if (thing == 2)
                    {
                        lines[r] += " R ";
                    }
                    else if (thing == -1)
                    {
                        lines[r] += " b ";
                    }
                    else if (thing == -2)
                    {
                        lines[r] += " B ";
                    }
                    
                }
                lines[r] += "║";
            }
            if (turn == 1) { this.flipBoard(); }
            lines[9] = "╚════════════════════════╝";
            System.IO.File.AppendAllLines("./log.txt", lines);
        }

    } // end class
}



