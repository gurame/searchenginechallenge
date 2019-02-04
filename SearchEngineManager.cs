using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class SearchEngineManager
    {
        private List<ISearchEngineService> GetSearchEngineServices()
        {
            List<ISearchEngineService> searchEngineList = new List<ISearchEngineService>();

            var type = typeof(ISearchEngineService);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            foreach (var item in types)
            {
                var instance = (ISearchEngineService)Activator.CreateInstance(item);
                searchEngineList.Add(instance);
            }

            return searchEngineList;
        }

        private List<string> GetInputListToSearch(string input)
        {
            List<string> inputList = new List<string>();

            //include words between quotes
            foreach (Match match in Regex.Matches(input, "\"([^\"]*)\""))
            {
                inputList.Add(match.ToString());
            }

            //remove words in quotes
            foreach (var item in inputList)
            {
                input = input.Trim().Replace(item, string.Empty);
            }

            //include single words (no quotes)
            foreach(var item in input.Split(' '))
            {
                if(!string.IsNullOrEmpty(item))
                    inputList.Add(item);
            }

            return inputList;
        }

        public SearchEngineResult Search(string input)
        {
            SearchEngineResult searchEngineResult = new SearchEngineResult();

            var inputList = this.GetInputListToSearch(input);

            var searchEngines = this.GetSearchEngineServices();

            //input stats
            foreach (var inputItem in inputList)
            {
                SearchEngineInputItemResult searchItem = new SearchEngineInputItemResult(inputItem);
                foreach (var item in searchEngines)
                {
                    var result = item.Search(inputItem);
                    searchItem.AddSearchEngineResult(item.GetName(), result);
                    searchItem.AddResult(result);
                    item.AddInputWinner(inputItem, result);
                }
                searchEngineResult.AddSearchInputItemResult(searchItem);
            }

            //engine stats
            foreach (var item in searchEngines)
            {
                searchEngineResult.AddSearchEngineItemResult(item.GetName(), item.GetInputWinner(), item.GetTotalWinner());
            }

            //total winner
            long totalResult = 0;
            foreach (var item in searchEngineResult.Inputs)
            {
                if (item.TotalResult > totalResult)
                {
                    totalResult = item.TotalResult;
                    searchEngineResult.TotalWinner = item.Input;
                }
            }
        
            return searchEngineResult;
        }
    }
}
