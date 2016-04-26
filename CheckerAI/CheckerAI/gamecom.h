#include <iostream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include <vector>


using namespace std;

namespace Checkers
{
	//You'll want to use this.
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

	//This is handy for filling valid moves list.
	struct Move
	{
		Point s, d;
		Move(Point src, Point dest) {
			s = src;
			d = dest;
		}
	};


	//When calling this function the board is read from file and copied into the int array passed
	//the board is always in the players perspective
	//--meaning the board is oriented so you are at the bottom moving upwards
	//--think two players at either end of a table
	//you are always 1 and your opponent is always -1
	//you cannot go on 9's and 0's are empty
	void getGameBoard(int board[8][8])
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
			return;
			
		}
	}

	void pass() {
		ofstream out("move.txt");
		out << "pass";
		out.close();
	}
	


	//Pass a vector of all the Points(included Struct) you want to move to
	//first point is the peice being moved
	//second is where it is moved to
	//third forth etc.. are for continual jumping
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
