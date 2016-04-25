#include <iostream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include <vector>


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
		bool operator==(Point p) {
			return (r == p.r && c == p.c);
		}
		bool operator!=(Point p) {
			return !(r == p.r && c == p.c);
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

	void pass() {
		ofstream out("move.txt");
		out << "pass";
		out.close();
	}
	
	void putMove(vector<Point> points)
	{
		ofstream out("move.txt");

		for each(Point point in points) 
		{
			if (point.c > 7 || point.r < 0)
			{
				return;
			}
			if (point.r > 7 || point.c < 0)
			{
				return;
			}
			out << point.r << "," << point.c;
			if (point != points[points.size()-1]) 
			{
				out << ":";
			}
		}
		out.close();
	}		
}
