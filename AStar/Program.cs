﻿using AStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
    class Program
    {
        private static Node initialState = new Node();
        private static Frontier initialFrontier = new Frontier();

        public static void Main()
        {
            foreach (Board board in initialState.getBoard().makeAllMoves())
            {
                if (board != null)
                {
                    initialFrontier.add(new Node(1, board));
                }
            }

            LinkedList<Node> path = new LinkedList<Node>();
            path.Append(initialState);
            List<Node> visited = new List<Node>();
            visited.Add(initialState);
            if (search(initialFrontier, path, visited))
            {
                Console.WriteLine("Solution Found!");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }


        private static bool search(Frontier frontier, LinkedList<Node> path, List<Node> visited)
        {
            if (frontier.getFrontier().Count() == 0)
            {
                return false;
            }
            Node current = frontier.getFrontier().Dequeue();
            if (visited.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), current.getBoard().getBoard()) && n.getCost() == current.getCost()))
            {
                return false;
            }
            path.Append(current);
            if (current.getMisplacedTiles() == 0)
            {
                return true;
            }
            visited.Add(current);
            foreach (Board board in current.getBoard().makeAllMoves())
            {
                if (board != null)
                {
                    Node nodeToAdd = new Node(current.getCost() + 1, board);
                    if (!visited.Any(n => Enumerable.SequenceEqual(n.getBoard().getBoard(), nodeToAdd.getBoard().getBoard()) && n.getCost() == nodeToAdd.getCost() ))
                    {
                        frontier.add(nodeToAdd);
                    }
                }
            }
            return search(frontier, path, visited);
        }
    }
}
