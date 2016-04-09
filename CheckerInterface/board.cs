using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerInterface
{
    class Board
    {
        public int turn;
        private char[,] b;
        //[0] row [1] col
        int[] peice = new int[2]; 
        

        Board()
        {
            turn = 1;
            b = new char[8, 8]     {{'x','r','x','r','x','r','x','r'},
                                    {'r','x','r','x','r','x','r','x'},
                                    {'x','r','x','r','x','r','x','r'},
                                    {' ','x',' ','x',' ','x',' ','x'},
                                    {'x',' ','x',' ','x',' ','x',' '},
                                    {'b','x','b','x','b','x','b','x'},
                                    {'x','b','x','b','x','b','x','b'},
                                    {'b','x','b','x','b','x','b','x'}}; 
                       
        }

        void getBoard(ref char[,] bird) {
            bird = b;
        }

        int putMove( int c, int r) {

            return 1;
        }

        void selectPeice(int r, int c) {
            this.peice[0] = r;
            this.peice[1] = c;
        }
               



    }


}
