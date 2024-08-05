using Microsoft.Extensions.DependencyInjection;
using IbmImporter.Models;
using IbmImporter;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a path to the json file");
    return;
}

var serviceProvider = RegisterIbmImporter.Register();

var parser = serviceProvider.GetRequiredService<IJsonFileParser>();

var validator = serviceProvider.GetRequiredService<IValidator<NotebookData>>();

var ibmImporter = new IbmImporter.IbmImporter();

var importResult = ibmImporter.Import();

Console.WriteLine($"{importResult.Success} Notebooks have been imported");

foreach(var unsuccessfulNotebook in importResult.Unsuccess)
{
    Console.WriteLine($"{unsuccessfulNotebook.Index} cannot be imported because {unsuccessfulNotebook.ErrorMessage}");
    Console.WriteLine(unsuccessfulNotebook.Notebook);
}

static void PrintNotebook(NotebookData notebookData)
{
    Console.WriteLine($"Customer: {notebookData.Customer}");
    Console.WriteLine("Notebooks:");
    foreach (var notebook in notebookData.Notebooks)
    {
        Console.WriteLine($"- {notebook.Quantity}x {notebook.Name}");
        Console.WriteLine($"  Price: {notebook.Price}");
        Console.WriteLine($"  CPU: {notebook.CPU}");
        Console.WriteLine($"  Color: {notebook.Color}");
        Console.WriteLine($"  Date of production: {notebook.DateOfProduction}");
        Console.WriteLine($"  RAM: {notebook.Ram}");
        Console.WriteLine($"  Processor model: {notebook.ProcessorModel}");
        Console.WriteLine($"  Monitor:");
        Console.WriteLine($"    Resolution: {notebook.Monitor.Width}x{notebook.Monitor.Height}");
        Console.WriteLine($"    Refresh rates: {string.Join(", ", notebook.Monitor.SupportedRefreshRate)}");
    }
}
