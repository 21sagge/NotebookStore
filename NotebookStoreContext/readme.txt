Entrare nella cartella del progetto:
    cd NotebookStoreContext

Creazione di una nuova migrazione:
    dotnet ef migrations add "NomeMigrazione" -s ../NotebookStoreMVC

Applica migrazioni fino all'ultima:
    dotnet ef database update -s ../NotebookStoreMVC
