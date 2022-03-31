

## Prerequisites
* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) must be installed on the computer for this code to be compiled and run.

## How to test this code?
* Clone this repo to local machine.
* Open command prompt or terminal.
* Navigate to `src` folder.
* Run followint command to restore dependencies.
  ```
    dotnet restore
  ```
* Follow below steps to test the apis Via Swagger UI.
  * Run command to host and run the Web API on local develoment server.
    ```
      dotnet run --project StarBoutique.WebApi/StarBoutique.WebApi.csproj
    ```
  * Open browser and browse http://localhost:5082/swagger/index.html.
  * Expand individual API endpoints and click on *Try it out*.
  * Click on *Try it out*.
  * Provide appropriate input value.
  * Click *Execute* button.
  * Observe the outcome in the Response body.
* Follow below steps to run unit tests.
  ```
    dotnet test StarBoutique.Tests/StarBoutique.Tests.csproj
  ```