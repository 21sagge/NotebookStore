using IbmImporter;
using Microsoft.Extensions.DependencyInjection;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a path to the json file");
    return;
}

var serviceProvider = RegisterIbmImporter.Register();

var ibmImporter = serviceProvider.GetRequiredService<DataImporter>();

var importResult = ibmImporter.Import(args[0]);

Console.WriteLine($"{importResult.Success} of {importResult.Success + importResult.Unsuccess.Count} Notebooks have been imported");

foreach (var unsuccessfulNotebook in importResult.Unsuccess)
{
    Console.WriteLine($"Notebook {unsuccessfulNotebook.Index} cannot be imported: {unsuccessfulNotebook.ErrorMessage}");
    Console.WriteLine();
    Console.WriteLine(unsuccessfulNotebook.Notebook);
}
