using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        Repository rep = new Repository();

        public ActionResult Game()
        {
            return View("Game", new Game());
        }

        public ActionResult Display(int id)
        {
            if(rep.GetCount(id) == 9)
                ViewBag.Text = "Ничья";

            string winner, winPos;
            var game = rep.Get(id);
            if (game.IsGameOver(out winner, out winPos))
            {                
                game.PaintOver(winPos);
                rep.Update(game);
                ViewBag.Text = winner;
            }

            return View("Game", rep.Get(id));
        }

        public ActionResult DisplayResult(int id)
        {   
            return View("Game", rep.Get(id));
        }

        public ViewResult DisplayResults(int? page)
        {
            if (page == null)
                page = 1;
            var pageSize = 10;
            var games = rep.GetGamesPage((int)page, pageSize);
            ViewBag.Page = page;
            ViewBag.PageCount = rep.GetGamesPageCount(pageSize);

            ViewBag.Wins = rep.GetWinsCount();
            ViewBag.Loses = rep.GetLosesCount();
            ViewBag.Draws = rep.GetDrawsCount();

            return View("Results", games);
        }

        [HttpPost]     
        public ActionResult MakeMove(string Number, int id)
        {
            if (id == 0)
                id = rep.CreateNewGame();
            
            string winner, winParse;
            if (rep.Get(id).IsGameOver(out winner, out winParse))
                return RedirectToAction("Display", new { id = id });
            else
                rep.MakeMove(id, int.Parse(Number), "X");

            var game = rep.Get(id);
            if (game.IsGameOver(out winner, out winParse))
                return RedirectToAction("Display", new { id = id });

            var move = DecisionTree.MakeDecision(game);           
            rep.MakeMove(id, move, "O");

            return RedirectToAction("Display", new { id = id });
        }    
        
          
    }
}   