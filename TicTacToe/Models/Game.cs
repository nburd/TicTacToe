using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Game
    {
        public int Id { get; set; }

        public virtual ICollection<Move> Moves { get; set; }

        public string Winner { get; set; }

        public Game()
        {
            Moves = new HashSet<Move>();
        }

        public Game Clone()
        {
            var game = new Game();
            foreach (var move in Moves)
                game.Moves.Add(move.Clone());
            return game;
        }

        public bool IsGameOver(out string winner, out string winPos)
        {                                    
            string[,] moves = new string[3,3];

            foreach (var move in Moves)
                moves[move.MoveNumber / 3, move.MoveNumber % 3] = move.XorO;
            
            for (int i = 0; i < 3; i++)
                if (moves[i, 0] == moves[i, 1] && moves[i, 0] == moves[i, 2] && moves[i, 0] != null)
                {
                    Winner = winner = moves[i, 0];
                    winPos = "row" + i.ToString();
                    return true;
                }

            for (int i = 0; i < 3; i++)
                if (moves[0, i] == moves[1, i] && moves[0, i] == moves[2, i] && moves[0, i] != null)
                {
                    Winner = winner = moves[0, i];
                    winPos = "column" + i.ToString();
                    return true;
                }

            if(moves[0, 0] == moves[1, 1] && moves[1, 1] == moves[2, 2] && moves[0, 0] != null)
            {
                Winner = winner = moves[0, 0];
                winPos = "diagonal1";
                return true;
            }

            if(moves[0, 2] == moves[1, 1] && moves[1, 1] == moves[2, 0] && moves[0, 2] != null)
            {
                Winner = winner = moves[0, 2];
                winPos = "diagonal2";
                return true;
            }

            Winner = winner = string.Empty;
            winPos = string.Empty;
            if (Moves.Count == 9)            
                return true;
            Winner = null;
            return false;
        }

        public void PaintOver(string res)
        {
            switch(res)
            {
                case "row0":
                    Moves.Where(x => x.MoveNumber == 0 || x.MoveNumber == 1 || x.MoveNumber == 2)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "row1":
                    Moves.Where(x => x.MoveNumber == 3 || x.MoveNumber == 4 || x.MoveNumber == 5)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "row2":
                    Moves.Where(x => x.MoveNumber == 6 || x.MoveNumber == 7 || x.MoveNumber == 8)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "column0":
                    Moves.Where(x => x.MoveNumber == 0 || x.MoveNumber == 3 || x.MoveNumber == 6)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "column1":
                    Moves.Where(x => x.MoveNumber == 1 || x.MoveNumber == 4 || x.MoveNumber == 7)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "column2":
                    Moves.Where(x => x.MoveNumber == 2 || x.MoveNumber == 5 || x.MoveNumber == 8)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "diagonal1":
                    Moves.Where(x => x.MoveNumber == 0 || x.MoveNumber == 4 || x.MoveNumber == 8)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
                case "diagonal2":
                    Moves.Where(x => x.MoveNumber == 2 || x.MoveNumber == 4 || x.MoveNumber == 6)
                        .ToList().ForEach(x => x.WinnerPosition = true);
                    return;
            }
        }

    }
}