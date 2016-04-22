#include "gamecom.h"
#include <vector>
#include <cmath>


using namespace Checkers;
void fillValidMoves();
bool findJumps(Point p);
bool findMoves(Point p);


int board[8][8] = {};
vector<Move> validMoves;


void main() {

	getGameBoard(board);

	fillValidMoves();

	int x = rand() % (validMoves.size()/sizeof(Move));

	putMoveMove(validMoves[x]);

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
						forced = true;
						validMoves.empty();
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
bool findJumps(Point p) {

	bool found = false;
	if (board[p.r][p.c] == 1 || board[p.r][p.c] == 2)
	{
		//check front left diag for oppo and front left  double diag for empty
		if (p.r > 1 && p.c > 1 && board[p.r - 1][p.c - 1] == -1 && (board[p.r - 2][p.c - 2] == 0))
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c),Point(p.r - 2, p.c - 2)));
			found = true;
		}

		//check front right diag for oppo and front right double diag for empty
		if (p.r > 1 && p.c < 6 && board[p.r - 1][p.c + 1] == -1 && (board[p.r - 2][p.c + 2] == 0))
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r - 2, p.c + 2)));
			found = true;
		}
	}

	if (board[p.r][p.c] == 2)
	{
		//check back diag left  for oppo and back double diag left  for empty
		if (p.r < 6 && p.c > 1 && board[p.r + 1][p.c - 1] == -1 && (board[p.r + 2][p.c - 2] == 0))
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 2, p.c - 2)));
			found = true;
		}

		//check back diag right for oppo and back double diag right for empty
		if (p.r < 6 && p.c < 6 && board[p.r + 1][p.c + 1] == -1 && (board[p.r + 2][p.c + 2] == 0))
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 2, p.c + 2)));
			found = true;
		}
	}
	return found;
}

bool findMoves(Point p) {
	bool found = false;

	if (board[p.r][p.c] == 1 || board[p.r][p.c] == 2)
	{
		//check front left diag for empty
		if (p.r > 0 && p.c > 0 && board[p.r - 1, p.c - 1] == 0)
		{
			//add Move(piece, that double diag)
			validMoves.push_back((Move(Point(p.r, p.c), Point(p.r - 1, p.c - 1))));
			found = true;
		}

		//check front right diag for empty
		if (p.r > 0 && p.c < 7 && board[p.r - 1, p.c + 1] == 0)
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c),Point(p.r - 1, p.c + 1)));
			found = true;
		}

	}

	if (board[p.r][p.c] == 2)
	{
		//check back diag left for empty
		if (p.r < 7 && p.c > 0 && board[p.r + 1, p.c - 1] == 0)
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 1, p.c - 1)));
			found = true;
		}

		//check back diag right for empty
		if (p.r < 7 && p.c < 7 && board[p.r + 1, p.c + 1] == 0)
		{
			//add Move(piece, that double diag)
			validMoves.push_back(Move(Point(p.r, p.c), Point(p.r + 1, p.c + 1)));
			found = true;
		}
	}

	return found;
}