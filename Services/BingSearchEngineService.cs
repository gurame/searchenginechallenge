using HtmlAgilityPack;
using SearchEngine.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngine.Services
{
    public class BingSearchEngineService : BaseSearchEngineService, ISearchEngineService
    {
        public string GetName()
        {
            return "Bing";
        }

        public long Search(string input)
        {
            var response = PerformCall($"https://www.bing.com/search?q={Uri.EscapeUriString(input)}");

            if (!response.IsSuccessStatusCode)
                return -1;

            var responseAsString = response.Content.ReadAsStringAsync().Result;

            var doc = new HtmlDocument();
            doc.LoadHtml(responseAsString);

            try
            {
                string value = string.Empty;

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='sb_count']"))
                {
                    value = node.InnerText;
                }

                return long.Parse(new String(value.Where(Char.IsDigit).ToArray()));
            }
            catch (Exception)
            {
                return -1;
            }

            
        }
    }
}
