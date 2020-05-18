using FlyAirLine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyAirLine.Aplication.Interfaces
{
    public interface IRoutesAppService
    {
        string[] FindBestRoute(string origin, string destination, bool showAllRoutesFoud = false);
        bool ImportRoutesFromFile(string filePath);

        bool AddRoutes(IEnumerable<Route> rotes);
    }
}
