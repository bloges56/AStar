using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AStar.Models
{
    class Board
    {
        private int[] board;

        public Board()
        {
            randomBoard();
        }

        public Board(int[] setBoard)
        {
            board = new int[9];
            Array.Copy(setBoard, board, 9);

        }

        public int[] getBoard()
        {
            return board;
        }

        public Board[] makeAllMoves()
        {
            int blankLoc = findBlank();
            Board[] moves = new Board[4];

            Board leftMoveBoard = new Board(board);
            if(leftMoveBoard.makeMove(blankLoc, -1))
            {
                moves[0] = leftMoveBoard;
            }
            

            Board rightMoveBoard = new Board(board);
            if(rightMoveBoard.makeMove(blankLoc, 1))
            {
                moves[1] = rightMoveBoard;
            }
            

            Board upMoveBoard = new Board(board);
            if(upMoveBoard.makeMove(blankLoc, -3))
            {
                moves[2] = upMoveBoard;
            }

            Board downMoveBoard = new Board(board);
            if(downMoveBoard.makeMove(blankLoc, 3))
            {
                moves[3] = downMoveBoard;
            }
            
            return moves;

        }

        private bool makeMove(int blankLoc, int move)
        {
            int left = -1;
            int right = 1;
            int up = -3;
            int down = 3;
            if ((move == left && blankLoc % 3 != 0) || 
                (move == right && blankLoc % 3 != 2) || 
                (move == up && blankLoc / 3 != 0) || 
                (move == down && blankLoc / 3 != 2))
            {
                board[blankLoc] = board[blankLoc + move];
                board[blankLoc + move] = 0;
                return true;
            }
            return false;
        }

        private int findBlank()
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

        private void randomBoard()
        {
            Random rnd = new Random();
            do{
                board = new int[9];
                List<int> nums = new List<int>();
                for(int i = 0; i <9; i++)
                {
                    nums.Add(i);
                }
                for (int i = 0; i < 9; i++)
                {
                    int rand = rnd.Next(0, nums.Count());
                    int value = nums[rand];
                    board[i] = value;
                    nums.Remove(value);
                }        
            } while (getInvCount() % 2 != 0);
        }

        private int getInvCount()
        {
            int inv_count = 0;
            for(int i = 0; i < 9; i++)
            {
               for(int j = i+1; j < 9; j++)
                {
                    if(board[i] > 0 && board[j] < board[i])
                    {
                        inv_count++;
                    }
                }
            }
            return inv_count;
        }
    }
}
