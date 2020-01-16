using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Scoreboard.ViewModels;

namespace Scoreboard.Models
{
    public class Performance
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Value is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value is mandatory")]
        [Range(0, int.MaxValue, ErrorMessage = "Only valid integer number are accepted")]
        public int Score { get; set; }
    }

    public class ScoreboardDbContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }

        public DbSet<HomeViewModel> ScoreboardContext { get; set; }
    }
}
