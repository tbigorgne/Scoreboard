using System.Collections.Generic;
using System.Linq;
using Scoreboard.ViewModels;

namespace Scoreboard.Models
{
    public class Dal : IDal
    {
        private ScoreboardDbContext scrBrd;

        public Dal()
        {
            scrBrd = new ScoreboardDbContext();
        }

        // Retrieve all performances and sort them as required
        public List<Performance> GetAllPerformances(bool? listAscSorting)
        {
            List<Performance> list = new List<Performance>();

            if (!listAscSorting.HasValue)
            {
                list = scrBrd.Performances.ToList();
            }
            else
            {
                list = listAscSorting.Value ?
                    scrBrd.Performances.OrderBy(performance => performance.Score).ToList() :
                        scrBrd.Performances.OrderByDescending(performance => performance.Score).ToList();
            }

            return list;
        }

        // Creation of performance
        public void CreatePerformance(string name, int score)
        {
            scrBrd.Performances.Add(new Performance() { Name = name, Score = score });
            scrBrd.SaveChanges();
        }

        // Empty data from the list and then clear the context
        public void ClearList()
        {
            if (scrBrd.Performances.Any())
            {
                scrBrd.Performances.RemoveRange(scrBrd.Performances);
            }
            if (scrBrd.ScoreboardContext.Any())
            {
                scrBrd.ScoreboardContext.RemoveRange(scrBrd.ScoreboardContext);
            }
            if (scrBrd.Performances.Any() || scrBrd.ScoreboardContext.Any())
            {
                scrBrd.SaveChanges();
            }
        }

        // Make sure only a single instance of the context exists
        public void InitializeContext()
        {
            if (!scrBrd.ScoreboardContext.Any())
            {
                scrBrd.ScoreboardContext.Add(new HomeViewModel());
                scrBrd.SaveChanges();
            }
        }


        // Return the context
        public HomeViewModel GetContext()
        {
            return scrBrd.ScoreboardContext.First();
        }

        // Set data within the context in order to indicate whether list should [not be sorted/ascending/descending] depending on demand
        public void UpdateContext(bool sorting)
        {
            // Get sorting indication and another indication which lets us know whether sorting was already initialized
            HomeViewModel scoreBoardContext = this.GetContext();
            bool? listAscSorting = scoreBoardContext.ListAscSorting;
            bool sortingAlreadyInitialized = scoreBoardContext.SortingAlreadyInitialized;

            // In case sorting order was required for the first time, then let's make it as ascending(first descending, and then switched below)
            listAscSorting = sorting && !sortingAlreadyInitialized ? !sorting : listAscSorting;
            // In case sorting order is required at least once, then indication remains set up to true value
            sortingAlreadyInitialized = sorting ? sorting : sortingAlreadyInitialized;

            // In case sorting order is required and defined, then let's switch the [ascending/descending] direction
            if (sorting && listAscSorting.HasValue)
            {
                listAscSorting = !listAscSorting;
            }

            // Record data within the context
            scoreBoardContext.ListAscSorting = listAscSorting;
            scoreBoardContext.SortingAlreadyInitialized = sortingAlreadyInitialized;
            scrBrd.SaveChanges();
        }

        // Attach performances to context
        public void AddPerformancesToContext(List<Performance> performances)
        {
            HomeViewModel scoreBoardContext = this.GetContext();
            scoreBoardContext.Performances = performances;
            scrBrd.SaveChanges();
        }

        // Releases resources
        public void Dispose()
        {
            scrBrd.Dispose();
        }
    }
}
