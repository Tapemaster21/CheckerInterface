#include "gamecom.h"
#include <vector>
#include <cmath>
#include <time.h>


using namespace Checkers;
void fillValidMoves();
bool findJumps(Point p);
bool findMoves(Point p);
void printBoard();
void printValidMoves();
void updateBoard(Move m);

int board[8][8] = { };
vector<Move> validMoves;
vector<Point> points;
bool jumpMode = false;


void main() {

	getGameBoard(board);
	printBoard();

	fillValidMoves();
	printValidMoves();

	srand(time(NULL));
	int x = rand() % (validMoves.size());

	cout << "Selected " << x+1 << "\n\t" << validMoves[x].s.r << "," << validMoves[x].s.c << " " << validMoves[x].d.r << "," << validMoves[x].d.c << endl;

	points = { validMoves[x].s,validMoves[x].d };
	
	//looking for a second jump it doesnt get in here )= no idea why
	Point dest = validMoves[x].d;
	cout << dest.r << "," << dest.c << endl;
	updateBoard(validMoves[x]);

	printBoard();

	if (findJumps(dest)) {
		cout << "shit";
	}
	points.push_back(validMoves[validMoves.size()-1].d);
	




	//conversion Bullshit
	Point *ps = new Point[points.size()];
	for (int i = 0; i < points.size(); i++) {
		ps[i] = points[i];
	}

	printValidMoves();

	putMove(ps,points.size());
	system("pause");
}

void printBoard()
{
	for (int r = 0; r < 8; r++)
	{
		for (int c = 0; c < 8; c++)
		{
			int thing = board[r][c];
			if (thing == 9 || thing == -9)
			{
				cout << (char)219;
			}
			else if (thing == 0)
			{
				cout << ' ';
			}
			else if (thing == 1)
			{
				cout << 'o';
			}
			else if (thing == 2)
			{
				cout << 'O';
			}
			else if (thing == -1)
			{
				cout << 'x';
			}
			else if (thing == -2)
			{
				cout << 'X';
			}
		}
		cout << endl;
	}
	cout << endl;
	return;
}

void printValidMoves()
{
	cout << validMoves.size() << " Moves: " << endl;
	for each (Move m in validMoves)
	{
		cout << "\t" << m.s.r << "," << m.s.c << " " << m.d.r << "," << m.d.c << endl;
	}
	cout << endl;
}

void fillValidMoves() {
	Point p;

	bool forced = false;
	for (int r = 0; r < 8; r++)
	{
		for (int c = 0; c < 8; c++)
		{
			if (board[r][c] == 1)
			{
				p.r = r;
				p.c = c;
				if (forced)
				{
					findJumps(p);
				}
				else
				{
					if (findJumps(p))
					{
						jumpMode = true;
						forced = true;
						validMoves.clear();
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

	if (board[p.r][p.c] == 1 || board[p.r][p.c] == 2)
	{
		cout << "front checks ";
		//check front left diag for oppo and front left  double diag for empty
		if (p.r > 1 && p.c > 1 && (board[p.r - 1][p.c - 1] == -1 || board[p.r - 1][p.c - 1] == -2) && (board[p.r - 2][p.c - 2] == 0))
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r - 2, p.c - 2)));
			found = true;
		}

		//check front right diag for oppo and front right double diag for empty
		if (p.r > 1 && p.c < 6 && (board[p.r - 1][p.c + 1] == -1 || board[p.r - 1][p.c + 1] == -2) && (board[p.r - 2][p.c + 2] == 0))
		{
			cout << "front Right ";
			//add Move(piece, that double diag)
			validMoves.push_back(Move( p, Point(p.r - 2, p.c + 2)));
			found = true;
		}
	}

	if (board[p.r][p.c] == 2)
	{
		//check back diag left  for oppo and back double diag left  for empty
		if (p.r < 6 && p.c > 1 && (board[p.r + 1][p.c - 1] == -1 || board[p.r + 1][p.c - 1] == -2) && (board[p.r + 2][p.c - 2] == 0))
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 2, p.c - 2)));
			found = true;
		}

		//check back diag right for oppo and back double diag right for empty
		if (p.r < 6 && p.c < 6 && (board[p.r + 1][p.c + 1] == -1 || board[p.r + 1][p.c + 1] == -2) && (board[p.r + 2][p.c + 2] == 0))
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 2, p.c + 2)));
			found = true;
		}
	}
	return found;
}

bool findMoves(Point p)
{

	bool found = false;

	if (board[p.r][p.c] == 1 || board[p.r][p.c] == 2)
	{
		//check front left diag for empty
		if (p.r > 0 && p.c > 0 && board[p.r - 1][p.c - 1] == 0)
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r - 1, p.c - 1)));
			found = true;
		}

		//check front right diag for empty
		if (p.r > 0 && p.c < 7 && board[p.r - 1][p.c + 1] == 0)
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r - 1, p.c + 1)));
			found = true;
		}

	}

	if (board[p.r][p.c] == 2)
	{
		//check back diag left for empty
		if (p.r < 7 && p.c > 0 && board[p.r + 1][p.c - 1] == 0)
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move( Point(p.r, p.c), Point(p.r + 1, p.c - 1)));
			found = true;
		}

		//check back diag right for empty
		if (p.r < 7 && p.c < 7 && board[p.r + 1][p.c + 1] == 0)
		{
			//add Move(piece][that double diag)
			validMoves.push_back(Move( Point(p.r, p.c), Point(p.r + 1, p.c + 1)));
			found = true;
		}
	}

	return found;
}

void updateBoard(Move m)
{
	if (abs(m.d.r - m.s.r) == 1)
	{
		board[m.d.r][m.d.c] = board[m.s.r][m.s.c];
		board[m.s.r][m.s.c] = 0;
	}
	else if (abs(m.d.r - m.s.r) == 2)
	{
		board[m.d.r][m.d.c] = board[m.s.r][m.s.c];
		board[m.s.r][m.s.c] = 0;
		int kr = (m.d.r - m.s.r) / 2 + m.s.r;
		int kc = (m.d.c - m.s.c) / 2 + m.s.c;

		board[kr][kc] = 0;
	}

	if (m.d.r == 0)
	{
		board[m.d.r][m.d.c] = 2;
	}


	// Write board to a board.txt file
}