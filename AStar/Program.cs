using AStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
    class Program
    {


        public static void Main()
        {
            int[] costAtDepth = { -1, -1 };
            int i = 0;
            while (costAtDepth[0] == -1)
            {
                costAtDepth = search();
                i++;
                Console.WriteLine(i);
            }
            Console.Write("depth:" + costAtDepth[0]);
            Console.Write("cost:" + costAtDepth[1]);
        }



        private static int[] search()
        {
            Node state = new Node();
            Frontier frontier = new Frontier();
            foreach (Board board in state.getBoard().makeAllMoves())
            {
                if (board != null)
                {
                    frontier.add(new Node(1, board));
                }
            }

            HashSet<Node> visited = new HashSet<Node>();
            visited.Add(state);
            while (frontier.getFrontier().Count() != 0 && visited.Count() <= 1500)
            {
                Node next = frontier.getFrontier().Dequeue();
                if (visited.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), next.getBoard().getBoard()) && n.getCost() == next.getCost()))
                {
                    continue;
                }
                visited.Add(next);
                if (next.getMisplacedTiles() == 0)
                {
                    return new int[] { next.getCost(), visited.Count() };
                }
                foreach (Board board in next.getBoard().makeAllMoves())
                {
                    if (board != null)
                    {
                        frontier.add(new Node(next.getCost() + 1, board));
                    }
                }
            }
            return new int[] { -1, -1 };
        }
    }
}
