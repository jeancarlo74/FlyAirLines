using FlyAirLine.Aplication.Interfaces;
using FlyAirLine.Domain.Entities;
using FlyAirLine.Domain.Interfaces;
using FlyAirLine.Infra.CrossCutting.Common;
using FlyAirLine.Infra.Repository;
using System;
using System.Collections.Generic;

namespace FlyAirLine.Aplication.Services
{
    public class RoutesAppService : IRoutesAppService
    {
        IRouteRepository _routeRepository;

        public RoutesAppService()
        {
            _routeRepository = new RouteRepository();
        }

        public bool ImportRoutesFromFile(string filePath)
        {
            return _routeRepository.ImportRoutesFromFile(filePath);
        }

        public bool AddRoutes(IEnumerable<Route> routes)
        {
            return _routeRepository.AddRoutes(routes);
        }

        public string[] FindBestRoute(string origin, string destination, bool showAllRoutesFoud = false)
        {
            Route[] routes = _routeRepository.GetAllRoutes();

            Tuple<string, int> bestRoute = null;

            for (int index = 0; index < routes.Length; index++)
            {
                var foundRoute = MountRoute(origin, destination, routes, index);

                if (showAllRoutesFoud)
                    if (foundRoute.Item2 != -1)
                        Console.WriteLine($"route found: {foundRoute.Item1} > ${foundRoute.Item2}");

                if (bestRoute == null)
                    bestRoute = foundRoute;

                if (foundRoute.Item2 != -1)
                {
                    if (bestRoute.Item2 == -1)
                        bestRoute = foundRoute;

                    if (foundRoute.Item2 < bestRoute.Item2)
                    {
                        bestRoute = foundRoute;
                    }
                }
            }
            
            if(bestRoute is null)
            {
                return new string[] {"", "-1"};
            }
            return new string[] {bestRoute.Item1, bestRoute.Item2.ToString()};
        }

        private Tuple<string, int> MountRoute(string origin, string destination, Route[] routes, int currentRoute)
        {
            if (routes is null || routes.Length == 0)
                return Tuple.Create("", -1);

            if (routes[currentRoute].Origin != origin)
                return Tuple.Create("", -1);

            string route = $"{routes[currentRoute].Origin}";
            int value = routes[currentRoute].Value;

            if (routes[currentRoute].Destination == destination)
                return Tuple.Create($"{route} - {destination}", value);

            string newOrigin = routes[currentRoute].Destination;
            Route[] newRoutes = Utils.RemoveItem(routes, currentRoute);

            int newIndexRoute;

            for (newIndexRoute = 0; newIndexRoute < newRoutes.Length; newIndexRoute++)
            {
                if (newRoutes[newIndexRoute].Origin == newOrigin)
                    break;
            }

            if (newIndexRoute >= newRoutes.Length)
                return Tuple.Create("", -1);

            var mountedRoute = MountRoute(newOrigin, destination, newRoutes, newIndexRoute);

            if (!string.IsNullOrEmpty(mountedRoute.Item1))
            {
                route += $" - {mountedRoute.Item1}";
                value += mountedRoute.Item2;

                mountedRoute = Tuple.Create(route, value);
            }

            return mountedRoute;
        }


        private Route[] LoadRoutesSeed()
        {
            return new Route[]
            {
                new Route() { Origin= "GRU", Destination = "BRC", Value = 10 },
                new Route() { Origin= "BRC", Destination = "SCL", Value = 5  },
                new Route() { Origin= "GRU", Destination = "CDG", Value = 75 },
                new Route() { Origin= "GRU", Destination = "SCL", Value = 20 },
                new Route() { Origin= "GRU", Destination = "ORL", Value = 56 },
                new Route() { Origin= "ORL", Destination = "CDG", Value = 5  },
                new Route() { Origin= "SCL", Destination = "ORL", Value = 20 },
            };
        }

    }
}
