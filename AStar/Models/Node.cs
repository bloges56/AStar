using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AStar.Models
{
    class Node
    {
        private Board board;
        private int cost;

        public Node()
        {
            board = new Board();
            cost = 0;

        }


        public Node(int newCost, Board newBoard)
        {
            board = new Board(newBoard.getBoard());
            cost = newCost;
        }

        public Board getBoard()
        {
            return board;
        }

        public int getCost()
        {
            return cost;
        }


        public int getMisplacedTiles()
        {
            int sum = 0;
            for (int i = 0; i < board.getBoard().Count(); i++)
            {
                if (board.getBoard()[i] != i)
                {
                    sum++;
                }
            }
            return sum;
        }
    }
}
