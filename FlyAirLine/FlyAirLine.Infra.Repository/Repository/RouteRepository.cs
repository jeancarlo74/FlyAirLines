using FlyAirLine.Domain.Entities;
using FlyAirLine.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace FlyAirLine.Infra.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private string _localPathCsvFile;
        private string _localCsvFile;

        public RouteRepository()
        {
            _localPathCsvFile = Path.Combine(AppContext.BaseDirectory, "LocalFiles");
            _localCsvFile = Path.Combine(_localPathCsvFile, "base_routes.csv");
        }

        public Route[] GetAllRoutes()
        {
            return LoadRoutes();
        }

        public bool AddRoutes(IEnumerable<Route> routes)
        {
            try
            {
                if (!Directory.Exists(_localPathCsvFile))
                    Directory.CreateDirectory(_localPathCsvFile);

                using (StreamWriter writer = new StreamWriter(_localCsvFile,true))
                    foreach (var route in routes)
                        writer.WriteLine($"{route.Origin},{route.Destination},{route.Value}");

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ImportRoutesFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                string fileName = Path.GetFileName(filePath);

                if (!Directory.Exists(_localPathCsvFile))
                    Directory.CreateDirectory(_localPathCsvFile);

                File.Copy(filePath, _localCsvFile, true);

                var routes = LoadRoutes();

                return (routes.Length > 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private Route[] LoadRoutes()
        {
            try
            {
                if (!File.Exists(_localCsvFile))
                    return new Route[] { };

                var lines = File.ReadAllLines(_localCsvFile);

                if (lines.Length == 0)
                    return new Route[] { };

                var rotes = new Route[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    var fields = lines[i].Split(',');
                    rotes[i] = new Route() { Origin = fields[0], Destination = fields[1], Value = Convert.ToInt32(fields[2]) };
                }

                return rotes;
            }
            catch (Exception)
            {
                return new Route[] { };
            }
        }
    }
}
