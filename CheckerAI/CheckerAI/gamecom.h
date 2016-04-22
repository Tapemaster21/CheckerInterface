#include <iostream>
#include <fstream>
#include <string>
#include <stdlib.h>


using namespace std;

namespace Checkers
{
	struct Point {
		int r, c;
		Point(int row, int col) {
			r = row;
			c = col;
		}
		Point() {
			r = 0; c = 0;
		}
	};
	struct Move
	{
		Point s, d;
		Move(Point src, Point dest) {
			s = src;
			d = dest;
		}
	};

	bool getGameBoard(int board[8][8])
	{
		int x, i;
		ifstream fin;
		fin.open("board.txt");
		
		if (fin.is_open()) {
			//cout << "opened file "<< endl;
			for (int r = 0; r <= 7; r++)
			{
				for (int c = 0; c <= 7; c++)
				{
					fin >> board[r][c];
					//cout << board[r][c];
				}

			}
			fin.close();
			return true;
			
		}
	}
			

		

		bool putMove(int sourceRow, int sourceCol, int destRow, int destCol)
		{
			ofstream out("move.txt");

			if (sourceCol > 7 || sourceCol < 0)
				return false;
			if (sourceRow > 7 || sourceRow < 0)
				return false;
			if (destCol > 7 || destCol < 0)
				return false;
			if (destRow > 7 || destRow < 0)
				return false;
			out << sourceRow << "," << sourceCol << ":" << destRow << "," << destCol << endl;
			out.flush();
			out.close();

			ifstream in("again");
			for (int i = 0; i < 10000; i++)
			{
				if (in)
				{
					in.close();
					remove("again");
					return true;
				}
			}
			return false;
		}
		bool putMovePoints(Point s, Point d) {
			return putMove(s.r, s.c, d.r, d.c);
		}
		bool putMoveMove(Move m) {
			return putMovePoints(m.s, m.d);
		}
		
		
}
