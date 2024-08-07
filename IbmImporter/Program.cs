using IbmImporter;
using Microsoft.Extensions.DependencyInjection;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a path to the json file");
    return;
}

var serviceProvider = RegisterIbmImporter.Register();

var ibmImporter = serviceProvider.GetRequiredService<DataImporter>();

var importResult = await ibmImporter.ImportAsync(args[0]);

DisplayImportSummary(importResult);

static void DisplayImportSummary(ImportResult importResult)
{
    Console.WriteLine($"{importResult.Success} of {importResult.Success + importResult.Unsuccess.Count} Notebooks have been imported");

    foreach (var unsuccessfulNotebook in importResult.Unsuccess)
    {
        Console.WriteLine($"\nNotebook {unsuccessfulNotebook.Index} cannot be imported: {unsuccessfulNotebook.ErrorMessage}");
        Console.WriteLine("\n" + unsuccessfulNotebook.Notebook);
    }
}
