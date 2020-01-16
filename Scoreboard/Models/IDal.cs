using System;
using System.Collections.Generic;
using Scoreboard.ViewModels;

namespace Scoreboard.Models
{
    public interface IDal : IDisposable
    {
        List<Performance> GetAllPerformances(bool? listAscSorting);

        void CreatePerformance(string name, int score);

        void ClearList();

        void InitializeContext();

        HomeViewModel GetContext();

        void UpdateContext(bool sorting);

        void AddPerformancesToContext(List<Performance> performances);
    }
}
