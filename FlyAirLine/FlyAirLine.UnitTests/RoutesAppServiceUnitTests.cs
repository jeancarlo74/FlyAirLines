using FlyAirLine.Aplication.Services;
using FlyAirLine.Aplication.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FlyAirLine.Domain.Entities;

namespace FlyAirLine.UnitTests
{
    [TestClass]
    public class RoutesAppServiceUnitTests
    {
        IRoutesAppService _routesAppService;

        public RoutesAppServiceUnitTests()
        {
            _routesAppService = new RoutesAppService();
        }

        [TestMethod]
        public void Test_ImportRoutesFromFile()
        {
            string inputFile = @"C:\projetos\scr\FlyAirLine\input-file.txt";

            var retorno = _routesAppService.ImportRoutesFromFile(inputFile);

            Assert.IsTrue(retorno);
        }

        [TestMethod]
        public void Test_FindBestRoute_Rota1()
        {
            string origem = "GRU";
            string destino = "CDG";

            string rotaEsperada = "GRU - BRC - SCL - ORL - CDG";
            string valorEsperado = "40";

            var resultado = _routesAppService.FindBestRoute(origem, destino);

            Assert.AreEqual(resultado.Length, 2);
            Assert.AreEqual(rotaEsperada, resultado[0]);
            Assert.AreEqual(valorEsperado, resultado[1]);
        }

        [TestMethod]
        public void Test_FindBestRoute_Rota2()
        {
            string origem = "BRC";
            string destino = "CDG";

            string rotaEsperada = "BRC - SCL - ORL - CDG";
            string valorEsperado = "30";

            var resultado = _routesAppService.FindBestRoute(origem, destino);

            Assert.AreEqual(resultado.Length, 2);
            Assert.AreEqual(rotaEsperada, resultado[0]);
            Assert.AreEqual(valorEsperado, resultado[1]);
        }

        [TestMethod]
        public void Test_AddRoutes()
        {
            List<Route> novasRotas = new List<Route>()
            {
                new Route() { Origin= "GRU", Destination = "BRC", Value = 10 },
                new Route() { Origin= "BRC", Destination = "SCL", Value = 5  },
                new Route() { Origin= "GRU", Destination = "CDG", Value = 75 },
                new Route() { Origin= "GRU", Destination = "SCL", Value = 20 },
                new Route() { Origin= "GRU", Destination = "ORL", Value = 56 },
                new Route() { Origin= "ORL", Destination = "CDG", Value = 5  },
                new Route() { Origin= "SCL", Destination = "ORL", Value = 20 },
            };

            var retorno = _routesAppService.AddRoutes(novasRotas);

            Assert.IsTrue(retorno);
        }

    }
}