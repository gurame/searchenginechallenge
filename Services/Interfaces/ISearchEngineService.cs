using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Services.Interfaces
{
    public interface ISearchEngineService
    {
        string GetName();
        long Search(string input);
        void AddInputWinner(string input, long total);
        string GetInputWinner();
        long GetTotalWinner();
    }
}
