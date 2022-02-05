# Configuration and usage
## Database
This project uses MySQL as its' database provider, so you should start by setting a MySQL server up. You can find [MySqlCreationScript.sql](MySqlCreationScript.sql) at the root of the repository for database initialization. To import sample data to the database, run [ImportSampleData.sql](ImportSampleData.sql).

## Usage

There are 2 App.config files in the project, one for the Web API and one for unit testing. The App.config files are found in [Etteplan.EtteplanMORE.ServiceManual.Web](./Etteplan.EtteplanMORE.ServiceManual.Web) and [Etteplan.EtteplanMORE.ServiceManual.UnitTests](./Etteplan.EtteplanMORE.ServiceManual.UnitTests) folders. After setting a database up and running, edit the connectionString values in the App.config files to point to your database.

#### Running the app
To run the application, select *EtteplanMORE.ServiceManual.Web* as your starting project in your IDE and press Run. If you're not using an IDE, run ```dotnet run``` using bash or cmd.

#### Troubleshooting
If the application fails to run, check that you have the required NuGet packages using ```dotnet restore``` and that you have *.NET Core 2.2* runtime installed on your computer.

## API documentation

Postman API documentation: https://documenter.getpostman.com/view/17381809/UVeFMRn2

## Unit tests

The project includes some unit tests for the application's services. The UnitTests folder contains App.config, where you should edit the connectionString to match your database containing the provided sample data.

To run the unit tests, run ```dotnet test``` using bash or cmd, or use your preferred IDE's / code editor's built-in unit testing tools.

## NuGet packages

The following NuGet packages are used in this application:

```
MySql.Data
Dapper
xUnit
xUnit.runner.console
xUnit.runner.visualstudio
Microsoft.AspNetCore.All
```
