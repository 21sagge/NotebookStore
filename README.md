# Notebook Store

This is an application that simulates a notebook store. It uses Entity Framework Core for data access and ASP.NET Core MVC for the front end.

## Project Structure

-   `NotebookStoreContext`: This project contains the DbContext and migrations for Entity Framework Core.
-   `NotebookStore.Entities`: This project contains the entity models for the application.
-   `NotebookStore.DAL`: This is the data access layer for the project.
-   `NotebookStore.Business`: This is the business layer for the project.
-   `NotebookStoreMVC`: This is the MVC application for the project.

## How to Run

1. Ensure you have .NET 7.0 or later installed.
2. Navigate to the `NotebookStoreMVC` directory.
3. Run 'dotnet watch' to start the application.
