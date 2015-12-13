using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TicTacToe.Models
{
    public class Node
    {
        public Game Game { get; set; }
        public List<Node> Children { get; set; }
        public int CurrentMove { get; set; }
        public string Winner { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public Node(Game game)
        {
            Game = game;
            Children = new List<Node>();
            Winner = "";
        }
    }

    public static class DecisionTree
    {
        private static Node Node { get; set; } = new Node(new Game());

        static DecisionTree()
        {
            CreateTree(Node);
            CreateProbability(Node);
        }

        public static void Initialize() { }

        private static void CreateTree(Node node)
        {
            var options = Enumerable.Range(0, 9).Except(node.Game.Moves.Select(x => x.MoveNumber));
            foreach (var option in options)
            {
                var newNode = new Node(node.Game.Clone())
                {
                    CurrentMove = option
                };
                node.Children.Add(newNode);

                var move = new Move
                {
                    MoveNumber = option,
                    XorO = XorO(node.Game.Moves.Count)
                };
                newNode.Game.Moves.Add(move);

                string winner, winPos;
                if (newNode.Game.IsGameOver(out winner, out winPos))
                {
                    newNode.Winner = winner;
                }
                else
                    CreateTree(newNode);
            }
        }

        private static string XorO(int count)
        {
            return (count % 2 == 0) ? "X" : "O";
        }

        private static void CreateProbability(Node node)
        {        
            if(node.Children.Count == 0)
            {
                if (node.Winner == "O")
                    node.Wins= 1;
                else if (node.Winner == "X")
                    node.Losses = 1;
                else
                    node.Draws = 1;
                return;
            }
                            
            foreach (var child in node.Children)
                CreateProbability(child);        

            if (node.Children.Any(x => x.Wins == 1 && x.Children.Count == 0))
                node.Children.RemoveAll(x => !(x.Wins == 1 && x.Children.Count == 0));            

            node.Wins = node.Children.Sum(x => x.Wins);
            node.Losses = node.Children.Sum(x => x.Losses);
            node.Draws = node.Children.Sum(x => x.Draws);

            node.Children.RemoveAll(x => x.Children.Any(y => y.Losses == 1 && y.Children.Count == 0));
        }

        public static int MakeDecision(Game game)
        {
            Node curNode = Node;
            foreach (var move in game.Moves)
                curNode = curNode.Children.Find(x => x.CurrentMove == move.MoveNumber);

            var potResult = curNode.Children.Where(x => x.Wins == 1 && x.Children.Count == 0).FirstOrDefault();
            if (potResult != null)
                return potResult.CurrentMove;

            var notPotResult = curNode.Children.Where(x => x.Losses == 1 && x.Children.Count == 0).FirstOrDefault();            
            if (notPotResult != null)
                curNode.Children.Remove(notPotResult);
            var coll = curNode.Children
                    .OrderBy(x => x.Losses)
                    .ThenByDescending(x => x.Draws)
                    .ThenByDescending(x => x.Wins);

            if (!coll.Any())
                return Enumerable.Range(0, 9).Except(game.Moves.Select(x => x.MoveNumber)).First();

            var result = coll.First();
            return result.CurrentMove;
        }  
    }
}