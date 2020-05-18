using System;
using System.Collections.Generic;
using FlyAirLine.Aplication.Services;
using FlyAirLine.Aplication.Interfaces;

namespace FlyAirLine
{
    public static class Program
    {

        static void Main(string[] args)
        {
            IRoutesAppService routesAppService = new RoutesAppService();

            if (args.Length == 0)
            {
                Console.Write("necessary to inform the input file");
                return;
            }

            if (!routesAppService.ImportRoutesFromFile(args[0]))
            {
                Console.Write("problem reading the reported file");
                return;
            }

            Console.Clear();

            while (true)
            {
                Console.Write("please enter the route: ");
                string input = Console.ReadLine();

                var rota = input.Split('-');

                if (rota.Length != 2)
                {
                    Console.Write("invalid route");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                if (rota[0].Length != 3 || rota[1].Length != 3)
                {
                    Console.Write("invalid route");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                string origin = rota[0].ToUpper();
                string destination = rota[1].ToUpper();

                var bestRoute = routesAppService.FindBestRoute(origin, destination, false);

                string message = string.IsNullOrEmpty(bestRoute[0])
                                    ?  $"sorry, reported route not found"
                                    :  $"best route: {bestRoute[0]} > ${bestRoute[1]}";

                Console.WriteLine("");
                Console.WriteLine(message);

                Console.ReadKey();
                Console.Clear();
            }
        }

    }
}
