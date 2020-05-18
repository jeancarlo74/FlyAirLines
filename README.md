# Rota de Viagem #

## Como executar a aplicação ##
A aplicação foi contruída na plataforma **.Net Core 3.0**
### Console ###
1) Compilar a aplicação
```shell
C:\[diretório dos fontes]\FlyAirLine\FlyAirLine.Console>dotnet build 
```
2) Executar o console, usando o comando *"dotnet run"* passando como o parâmetro o *[caminho]\[arquivo]*
```
C:\[diretório dos fontes]\FlyAirLine\FlyAirLine.Console>dotnet run input-file.txt 
```

### API ###
1) Compilar a aplicação
```shell
C:\[diretório dos fontes]\FlyAirLine\FlyAirLine.API>dotnet build 
```
2) Subir a API server
```
C:\[diretório dos fontes]\FlyAirLine\FlyAirLine.API>dotnet run
```

## Estrutura dos arquivos/pacotes ##
```
FlyAirLine
├───FlyAirLine.API
│   ├───Controllers
│   │   └───RoutesController.cs
│   ├───DTO
│   │   └───TripDTO.cs
│   ├───appsettings.Development.json
│   ├───appsettings.json
│   ├───FlyAirLine.API.csproj
│   ├───FlyAirLine.API.csproj.user
│   ├───Program.cs
│   └───Startup.cs
│
├───FlyAirLine.Aplication
│   ├───Interfaces
│   │   └───IRoutesAppService.cs
│   ├───Services
│   │   └───RoutesAppService.cs
│   └───FlyAirLine.Aplication.csproj
│
├───FlyAirLine.Console
│   ├───FlyAirLine.Console.csproj
│   ├───input-file.txt
│   └───Program.cs
│
├───FlyAirLine.Domain
│   ├───Entities
│   │   └───Route.cs
│   ├───Interfaces
│   │   └───IRouteRepository.cs
│   └───FlyAirLine.Aplication.csproj
│
├───FlyAirLine.Infra.Common
│   ├───FlyAirLine.Aplication.csproj
│   └───Utils.cs
│
└───FlyAirLine.Infra.Repository
│   ├───FlyAirLine.Infra.Repository.csproj
│   └───Repository
│       └───RouteRepository.cs
│
└───FlyAirLine.UnitTests
    ├───FlyAirLine.UnitTests.csproj
    └───RoutesAppServiceUnitTests.cs        
```
## Estrutura do Projeto no Visual Studio ##
```
Solution
├─── 1.0-Presentation
│      ├─── 1.1-Console
│      └─── 1.2-API
├─── 2.0-Aplication
├─── 3.0-Domain
│      ├─── Entities
│      └─── Interfaces
├─── 4.0-Infrastructure
│       ├─── 4.1-Data
│       │       └─── Repository
│       └─── 4.2-CrossCutting
│               └─── Common
└─── UnitTests
```

## Decisões de design adotadas para a solução ##
Foi utilizado o DDD (Domain Driven Design) deviddo seguintes as vantagens oferecidas por essa abordagem:
- Favorecer reutilização 
- Mínimo de acoplamento

O objetivo de se aplicar desse conceito, é ter um sofware:
* Escalável.
* Testável.
* Com uma manutenção fácil e tranquila.
* E escrito com boas práticas.

## Descrição da API Rest ##
### /api/Routes/ ##
Retorna a melhor rota para a viagem, isto é, a rota com o menor preço possível independentemente do número de conexões necessárias.
#### GET SearchBestRoute #### 
Consulta de melhor rota entre dois pontos.

Exemplo de JSON de envio:
```Json
{
    "Origin": "GRU",
    "Destination": "CDG"
}
```
Exemplo de JSON de Retorno:
```Json
{
    "route": "GRU - BRC - SCL - ORL - CDG",
    "value": "40"
}
```
#### POST AddNewRoutes #### 
Registro de novas rotas.

Exemplo de Json de envio:
```Json
[
    {
        "Origin": "GRU",
        "Destination": "BRC",
        "Value": 10
    },
    {
        "Origin": "BRC",
        "Destination": "SCL",
        "Value": 5
    }
]
```
