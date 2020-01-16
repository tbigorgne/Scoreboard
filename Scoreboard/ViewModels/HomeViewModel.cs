using System.Collections.Generic;
using Scoreboard.Models;

namespace Scoreboard.ViewModels
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public bool? ListAscSorting { get; set; }
        public bool SortingAlreadyInitialized { get; set; } = false;
        public List<Performance> Performances { get; set; }
    }
}
