using FlyAirLine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyAirLine.Domain.Interfaces
{
    public interface IRouteRepository
    {
        Route[] GetAllRoutes();
        bool ImportRoutesFromFile(string filePath);

        bool AddRoutes(IEnumerable<Route> routes);
    }
}
