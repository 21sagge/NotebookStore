namespace IbmImporter.Models;

public class NotebookData
{
	public string Customer { get; set; } = string.Empty;
	public List<Notebook> Notebooks { get; set; } = new();
}
