using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Models
{
    public class SearchEngineResult
    {
        public List<SearchEngineInputItemResult> Inputs { get; set; }

        public List<SearchEngineEngineItemResult> Engines { get; set; }

        public void AddSearchInputItemResult(SearchEngineInputItemResult item)
        {
            if (Inputs == null)
                Inputs = new List<SearchEngineInputItemResult>();

            Inputs.Add(item);
        }

        public void AddSearchEngineItemResult(string engine, string input, long result)
        {
            if (Engines == null)
                Engines = new List<SearchEngineEngineItemResult>();

            Engines.Add(new SearchEngineEngineItemResult()
            {
                Engine = engine,
                WinnerInput = input,
                WinnerResult = result
            });
        }

        public string TotalWinner { get; set; }
    }

    public class SearchEngineInputItemResult
    {
        public SearchEngineInputItemResult(string input)
        {
            this.Input = input;
        }

        public string Input { get; set; }
        
        public long TotalResult { get; set; }

        public List<SearchEngineItemEngineResult> Engines { get; set; }

        public void AddSearchEngineResult(string engine, long result)
        {
            if (Engines == null)
                Engines = new List<SearchEngineItemEngineResult>();

            Engines.Add(new SearchEngineItemEngineResult() {
                Engine = engine,
                Result = result
            });
        }

        public void AddResult(long result)
        {
            this.TotalResult += result;
        }
    }

    public class SearchEngineItemEngineResult
    {
        public string Engine { get; set; }
        public long Result { get; set; }
    }

    public class SearchEngineEngineItemResult
    {
        public string Engine { get; set; }
        public string WinnerInput { get; set; }
        public long WinnerResult { get; set; }
    }
}
