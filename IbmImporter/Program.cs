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

var notebookData = parser.Parse(args[0]);

if (notebookData == null) return;

if (validator.Validate(notebookData))
{
	PrintNotebook(notebookData);
}
else
{
	Console.WriteLine("Invalid data");
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
