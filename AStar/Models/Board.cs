using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AStar.Models
{
    class Board
    {
        private static int[] board;

        public Board()
        {
            randomBoard();
        }

        public Board(int[] newBoard)
        {
            board = newBoard;

        }

        public int[] getBoard()
        {
            return board;
        }

        public Board[] makeAllMoves()
        {
            int blankLoc = findBlank();
            Board[] moves = new Board[4];
            moves[0] = makeMove(blankLoc, -1);
            moves[1] = makeMove(blankLoc, 1);
            moves[2] = makeMove(blankLoc, -3);
            moves[3] = makeMove(blankLoc, 3);
            return moves;

        }

        private static Board makeMove(int blankLoc, int move)
        {
            int[] newBoard = board;
            if ((blankLoc % 3 + move >= 0) && (blankLoc % 3 + move <= 8) && (blankLoc / 3 + move >= 0) && (blankLoc / 3 + move <= 2))
            {
                newBoard[blankLoc] = newBoard[blankLoc + move];
                newBoard[blankLoc + move] = 0;
                return new Board(newBoard);
            }
            return null;
        }

        private static int findBlank()
        {
            for (int i = 0; i < board.Count(); i++)
            {
                if (board[i] == 0)
                {
                    return i;
                }
            }
            return 0;
        }

        private static void randomBoard()
        {
            Random rnd = new Random();
            do
            {
                board = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    int rand = rnd.Next(0, 9);
                    while (board.Contains(rand))
                    {
                        rand = rnd.Next(0, 9);
                    }
                    board[i] = rand;
                }
            } while (getInvCount() % 2 != 0);
        }

        private static int getInvCount()
        {
            int inv_count = 0;
            for (int i = 0; i < 3 - 1; i++)
                for (int j = i + 1; j < 3; j++)
                    if (board[j * 3 + i] > 0 && board[j * 3 + i] > board[i * 3 + i])
                        inv_count++;
            return inv_count;
        }
    }
}
