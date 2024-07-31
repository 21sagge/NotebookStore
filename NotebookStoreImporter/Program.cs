using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business;
using NotebookStoreImporter;

var services = new ServiceCollection();

services.AddSingleton<ISerializer, JsonHandler>();

var serviceProvider = services.BuildServiceProvider();

var serializer = serviceProvider.GetRequiredService<ISerializer>();

var jsonString = File.ReadAllText("IBM.json");

var notebookStore = serializer.Deserialize<NotebookData>(jsonString);

PrintNotebook(notebookStore);

static void PrintNotebook(NotebookData? notebookData)
{
	if (notebookData is null)
	{
		Console.WriteLine("Notebook is null");
		return;
	}

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
