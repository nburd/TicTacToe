namespace TicTacToe.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TicTacToeContext : DbContext
    {
        public TicTacToeContext()
            : base("name=Model1")
        {
        }
        
        public virtual DbSet<Move> Moves { get; set; }
        
        public virtual DbSet<Game> Games { get; set; }
    }

}