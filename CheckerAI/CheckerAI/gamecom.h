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

	bool putMove(Point points[],int length)
	{
		ofstream out("move.txt");

		for (int i = 0; i <= length;i++) {
			
			if (points[i].c > 7 || points[i].r < 0)
				return false;
			if (points[i].r > 7 || points[i].c < 0)
				return false;
			out << points[i].r << "," << points[i].c;
			if (i < length-1) {
				out << ":";
			}

		}
		out.close();
		return false;
	}		
}
