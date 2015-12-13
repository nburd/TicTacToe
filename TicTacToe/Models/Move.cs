using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class Move
    {
        public int Id { get; set;}

        public int MoveNumber { get; set; }

        public string XorO { get; set; }

        public bool WinnerPosition { get; set; }

        public Move Clone()
        {
            return new Move
            {
                MoveNumber = MoveNumber,
                XorO = XorO
            };
        }   
    }
}