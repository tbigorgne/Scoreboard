using System.Collections.Generic;
using System.Web.Mvc;
using Scoreboard.Models;
using Scoreboard.ViewModels;

namespace Scoreboard.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            using (IDal dal = new Dal())
            {
                dal.InitializeContext();
            }
        }

        public ActionResult Index(bool sorting)
        {
            using (IDal dal = new Dal())
            {
                // Set data within the context in order to indicate whether list should [not be sorted/ascending/descending] depending on demand
                dal.UpdateContext(sorting);
                bool? listAscSorting = dal.GetContext().ListAscSorting;

                // Retrieve all performances, sort them as required, attach them to context and display the view
                List<Performance> performances = dal.GetAllPerformances(listAscSorting);
                dal.AddPerformancesToContext(performances);
                return View(performances);
            }
        }

        // Creation of new performance after its submission in the related form, and then display default view
        [HttpPost]
        public ActionResult Index(string nameValue, int scoreValue)
        {
            using (IDal dal = new Dal())
            {
                dal.CreatePerformance(nameValue, scoreValue);
                return Index(false);
            }
        }

        // Empty list data and then context
        public ActionResult ClearList()
        {
            using (IDal dal = new Dal())
            {
                dal.ClearList();
                return RedirectToAction("Index", new { sorting = false });
            }
        }
    }
}
