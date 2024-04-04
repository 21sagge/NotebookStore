# Notebook Store

This is a console application that simulates a notebook store. It uses Entity Framework Core for data access.

## Project Structure

-   `NotebookStore.Entities`: This project contains the entity models for the application. You can find it [here](../NotebookStore.Entities/NotebookStore.Entities.csproj).
-   `NotebookStoreContext`: This project contains the DbContext and migrations for Entity Framework Core. You can find it [here](../NotebookStoreContext/NotebookStoreContext.csproj).
-   `NotebookStoreTestConsole`: This is the console application that uses the above projects. You can find it [here](./NotebookStoreTestConsole.csproj).

## How to Run

1. Ensure you have .NET 7.0 or later installed.
2. Navigate to the `NotebookStoreTestConsole` directory.
3. Run `dotnet run` to start the application.

## Dependencies

This project uses several packages, including:

-   `Microsoft.EntityFrameworkCore`
-   `Microsoft.EntityFrameworkCore.Proxies`
-   `Microsoft.EntityFrameworkCore.Design`
-   `Microsoft.EntityFrameworkCore.Tools`
-   `Microsoft.EntityFrameworkCore.Sqlite`

These dependencies are automatically restored when you build the project.
