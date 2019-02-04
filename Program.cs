using SearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length == 0 ? Console.ReadLine() : string.Join(" ", args);
            
            SearchEngineManager manager = new SearchEngineManager();
            SearchEngineResult result = manager.Search(input);

            Console.WriteLine($"Results");
            Console.WriteLine($"##############################");
            foreach (var item in result.Inputs)
            {
                Console.Write($"{item.Input} : ");
                foreach (var engine in item.Engines)
                {
                    Console.Write($"{engine.Engine}: {engine.GetResult()} ");
                }
                Console.Write(System.Environment.NewLine);
            }
            Console.WriteLine($"##############################");
            foreach (var item in result.Engines)
            {
                Console.Write($"{item.Engine} winner: {item.GetWinnerInput()} ");
                Console.Write(System.Environment.NewLine);
            }
            Console.WriteLine($"##############################");
            Console.Write($"Total winner: {result.TotalWinner} ");

            Console.ReadKey();
        }
    }
}
