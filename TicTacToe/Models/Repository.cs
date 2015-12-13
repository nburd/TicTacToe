using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe.Models
{
    public class Repository
    {
        public int CreateNewGame()
        {
            using (var db = new TicTacToeContext())
            {
                var game = new Game();                
                db.Games.Add(game);
                db.SaveChanges();
                return game.Id;
            }
        }

        public Game Get(int id)
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Include("Moves").First(x => x.Id == id);
            }
        }

        public void MakeMove(int id, int move, string xoro)
        {
            using (var db = new TicTacToeContext())
            {
                var newMove = new Move();
                newMove.MoveNumber = move;
                newMove.XorO = xoro;
                db.Games.Find(id).Moves.Add(newMove);
                db.SaveChanges();
            }
        }

        public int GetCount(int id)
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Find(id).Moves.Count();
            }
        }

        public List<Game> GetAllGames()
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Include("Moves").ToList();
            }
        }    
        
        public List<Game> GetGamesPage(int page, int pageSize)
        {
            page--;
            using (var db = new TicTacToeContext())
            {
                return db.Games.Include("Moves").OrderBy(x => x.Id).Skip(pageSize * page).Take(pageSize).ToList();
            }
        }

        public int GetGamesPageCount(int pageSize)
        {
            using (var db = new TicTacToeContext())
            {
                var count = db.Games.Count();
                return db.Games.Count() / pageSize + 1;
            }
        }

        public int GetWinsCount()
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Where(x => x.Winner == "O").Count();
            }
        }

        public int GetLosesCount()
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Where(x => x.Winner == "X").Count();
            }
        }

        public int GetDrawsCount()
        {
            using (var db = new TicTacToeContext())
            {
                return db.Games.Where(x => x.Winner == "").Count();
            }
        }
        
        public void Update(Game game)
        {
            using (var db = new TicTacToeContext())
            {
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                foreach (var move in game.Moves)
                    db.Entry(move).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }   
    }
}