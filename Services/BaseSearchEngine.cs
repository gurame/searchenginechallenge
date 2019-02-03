using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Services
{
    public abstract class BaseSearchEngine
    {
        public void AddInputWinner(string input, long total)
        {
            if (total > TotalWinner)
            {
                InputWinner = input;
                TotalWinner = total;
            }
        }

        public string GetInputWinner()
        {
            return this.InputWinner;
        }

        public long GetTotalWinner()
        {
            return this.TotalWinner;
        }

        public string InputWinner { get; set; }
        public long TotalWinner { get; set; }

        public HttpResponseMessage PerformCall(string url)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                return response;
            }
        }
    }
}
