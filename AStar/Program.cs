using AStar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        private static HashSet<Node> initialStates = new HashSet<Node>();

        public static async Task Main()
        {
            /*int[] costAtDepth = search();
            Console.WriteLine("Depth: " + costAtDepth[0] + " Cost: " + costAtDepth[1]);*/
            HashSet<int[]> tracker = new HashSet<int[]>();


            for (int i = 0; i < 100; i++)
            {
                int[] costAtDepth = new int[] { -1, -1 };
                while (costAtDepth[0] == -1)
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
                Console.WriteLine("\n{0}", i);
            }
            List<String> data = new List<String>();
            foreach (int[] track in tracker)
            {
                data.Add("depth: " + track[0] + "average: " + (1.0 * track[2]) / (1.0 * track[1]));
            }

            await File.WriteAllLinesAsync("Data.txt", data);
        }



        private static int[] search()
        {
           /* Board initalBoard = new Board(new int[9] { 0, 1, 2, 3, 5, 4, 7, 6, 8 });
            int invCount = initalBoard.getInvCount();*/

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
            while (frontier.getFrontier().Count() != 0)
            {
                Node next = frontier.getFrontier().Dequeue();
                if (visited.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), next.getBoard().getBoard()) /*&& n.getCost() <= next.getCost()*/))
                {
                    continue;
                }
                visited.Add(next);
                Console.Write("\r{0}", visited.Count());
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
