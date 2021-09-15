using AStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
    class Program
    {
        private static HashSet<Node> initialStates = new HashSet<Node>();

        public static void Main()
        {  
            HashSet<int[]> tracker = new HashSet<int[]>();
            

            for(int i = 0; i < 100; i++)
            {
                int[] costAtDepth = new int[] { -1, -1 };
                while(costAtDepth[0] == -1)
                {
                    costAtDepth = search();
                }
                if (tracker.Any(track => track[0] == costAtDepth[0]))
                {
                    int[] current = tracker.FirstOrDefault(track => track[0] == costAtDepth[0]);
                    current[1]++;
                    current[2] += costAtDepth[1];
                }
                else
                {
                    tracker.Add(new int[3] { costAtDepth[0], 1, costAtDepth[1] });
                }
                Console.WriteLine(i);
            }
            foreach(int[] track in tracker)
            {
                Console.WriteLine("depth:" + track[0] + "average" + (1.0 * track[2])/(1.0 * track[1]));
            }
        }



        private static int[] search()
        {
            Node state = new Node();
            if(initialStates.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), state.getBoard().getBoard())))
            {
                return new int[] { -1, -1 };
            }
            initialStates.Add(state);
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
            while (frontier.getFrontier().Count() != 0 && visited.Count() < 40000)
            {
                Node next = frontier.getFrontier().Dequeue();
                if (visited.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), next.getBoard().getBoard()) && n.getCost() == next.getCost()))
                {
                    continue;
                }
                visited.Add(next);
                Console.Write(visited.Count());
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
