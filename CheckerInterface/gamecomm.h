#include <iostream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include <System.IO>



using namespace std;

namespace CheckerInterface
{
    class gamecomm
    {
		bool getGameBoard(int iGameBoard[ROWS][COLS])
		{
			int x,i;
			ifstream in("board.txt");

		
		}

		bool putMove(int sourceRow,int sourceCol, int destRow,int destCol)
		{
			ofstream out("move.txt");

			if (sourceCol >= 7 || sourceCol<0)
				return false;
			if (sourceRow >= 7 || sourceRow<0)
				return false;
			if (destCol >= 7 || destCol<0)
				return false;
			if (destRow >= 7 || destRow<0)
				return false;
			out << sourceRow << "," << sourceCol << ":" << destRow << "," << destCol << endl;
			out.close();

			ofstream out("again.txt", ofstream::out | ofstream::trunc);
			out.close();

			FileSystemWatcher watcher = new FileSystemWatcher("./");
			watcher.Filter = "again.txt";
			watcher.NotifyFilter = NotifyFilters.LastWrite;
			watcher.Changed += return true;


			//if there are chances for a double jump

			return true;

			

		}



    }
}
