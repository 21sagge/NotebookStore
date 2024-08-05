namespace IbmImporter;

public class IbmImporter
{
    public IbmImporter()
    {
    }

    public ImportResult Import()
    {
        //var notebookData = parser.Parse(args[0]);

        //if (notebookData == null)
        //{
        //    Console.WriteLine("There was an error parsing the json");
        //    return;
        //}

        //if (validator.Validate(notebookData))
        //{
        //    PrintNotebook(notebookData);
        //}
        //else
        //{
        //    Console.WriteLine("Invalid data");
        //}

        return new ImportResult();

        // Parsing file json
        // Validare
        //      Se non è valido aggiungere UnimportedNotebook all'ImportResult
        //      Se valido andare avanti
        // Importare Notebook validi
        //      Se l'importazione non va a buon fine aggiugnere UnimportedNotebook ad ImportResult
        //      Se l'importazione va a buon fine andare avanti
        // Ritornare ImportResult
    }
}
