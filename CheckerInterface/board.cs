using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerInterface
{
    public struct Point {
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
            turn = 1;
            b = new int[8, 8]     {{9,-1,9,-1,9,-1,9,-1},
                                    {-1,9,-1,9,-1,9,-1,9},
                                    {9,-1,9,-1,9,-1,9,-1},
                                    {0,9,0,9,0,9,0,9},
                                    {9,0,9,0,9,0,9,0},
                                    {1,9,1,9,1,9,1,9},
                                    {9,1,9,1,9,1,9,1},
                                    {1,9,1,9,1,9,1,9}};

        }

        public void getBoard(ref int[,] board)
        {
            flipBoard();
            //validMoves.Clear();
            fillValidMoves();
            //this.b.CopyTo(board,0);
        }

        public bool putMove(Point s, Point d)
        {
            // We need to read in from text file
            Move m = new Move(s, d);
            if (this.validMoves.Contains(m))
            {
                this.updateBoard(m);
                this.validMoves.Clear();
                return findJumps(m.d);
            }
            else
            {
                // invalid move
                // instatnt game over
                return false; 
            }
        }

        void flipBoard()
        {
            rotate90();
            rotate90();
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
            //ret.CopyTo(b,0);
        }

        void fillValidMoves()
        {
            Point p;
            bool forced = false;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (b[r, c] == 1)
                    {
                        if (forced)
                        {
                            p.r = r;
                            p.c = c;
                            findJumps(p);
                        }
                        else
                        {
                            p.r = r;
                            p.c = c;
                            if (findJumps(p))
                            {
                                forced = true;
                                validMoves.Clear();
                                findJumps(p);
                            }
                            else
                            {
                                findMoves(p);
                            }
                        }
                    }
                }
            }
        }

        bool findJumps(Point p)
        {
            bool found = false;
            if (p.r > 2 && p.r < 6 && p.c > 2 && p.c > 6)
            {
                if (b[p.r, p.c] == 2)
                {
                    //check back diag left  for oppo and back double diag left  for empty
                    if (b[p.r + 1, p.c - 1] == -1 && (b[p.r + 2, p.c - 2] == 0))
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 2, p.c - 2)));
                        found = true;
                    }

                    //check back diag right for oppo and back double diag right for empty
                    if (b[p.r + 1, p.c + 1] == -1 && (b[p.r + 2, p.c + 2] == 0))
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 2, p.c + 2)));
                        found = true;
                    }
                }

                if (b[p.r, p.c] == 2 || b[p.r, p.c] == 1)
                {
                    //check front left diag for oppo and front left  double diag for empty
                    if (b[p.r - 1, p.c - 1] == -1 && (b[p.r - 2, p.c - 2] == 0))
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 2, p.c - 2)));
                        found = true;
                    }

                    //check front right diag for oppo and front right double diag for empty
                    if (b[p.r - 1, p.c + 1] == -1 && (b[p.r - 2, p.c + 2] == 0))
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 2, p.c + 2)));
                        found = true;
                    }
                }
            }
            return found;
        }

        bool findMoves(Point p)
        {
            bool found = false;

            if (p.r > 1 && p.r < 7 && p.c > 1 && p.c > 7)
            {
                if (b[p.r, p.c] == 2)
                {
                    //check back diag left  for oppo and back double diag left  for empty
                    if (b[p.r + 1, p.c - 1] == 0)
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 1, p.c - 1)));
                        found = true;
                    }

                    //check back diag right for oppo and back double diag right for empty
                    if (b[p.r + 1, p.c + 1] == 0)
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r + 1, p.c + 1)));
                        found = true;
                    }
                }

                if (b[p.r, p.c] == 2 || b[p.r, p.c] == 1)
                {
                    //check front left diag for oppo and front left  double diag for empty
                    if (b[p.r - 1, p.c - 1] == 0)
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 1, p.c - 1)));
                        found = true;
                    }

                    //check front right diag for oppo and front right double diag for empty
                    if (b[p.r - 1, p.c + 1] == 0)
                    {
                        //add Move(piece, that double diag)
                        validMoves.Add(new Move(new Point(p.r, p.c), new Point(p.r - 1, p.c + 1)));
                        found = true;
                    }

                }
            }
            return found;
        }

        void updateBoard(Move m)
        {
            if (Math.Abs(m.d.r - m.s.r) == 1)
            {
                b[m.d.r, m.d.c] = 1;
                b[m.s.r, m.s.c] = 0;
            }
            else if (Math.Abs(m.d.r - m.s.r) == 2)
            {
                b[m.d.r, m.d.c] = 1;
                b[m.s.r, m.s.c] = 0;
                int kr = (m.d.r - m.s.r) / 2 + m.s.r;
                int kc = (m.d.c - m.s.c) / 2 + m.s.c;

                b[kr, kc] = 0;
            }
            if (m.d.r == 0)
            {
                b[m.d.r, m.d.c] = 2;
            }

            // Write board to a board.txt file
        }

        bool over()
        {
            //check if pieces from both players exist
            bool red = false;
            bool black = false;
            bool over = false;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (b[r,c] == -1)
                    {
                        red = true;
                    }
                    else if (b[r, c] == 1)
                    {
                        black = true;
                    }
                    if (red && black)
                    {
                        over = true;
                    }
                }
                over = false;
            }
            return over;
            
        }
    } // end class
}
