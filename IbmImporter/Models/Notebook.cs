namespace IbmImporter.Models;

public class Notebook
{
	public int Quantity { get; set; }
	public string Name { get; set; } = string.Empty;
	public double Price { get; set; }
	public int CPU { get; set; }
	public string Color { get; set; } = string.Empty;
	public DateTime DateOfProduction { get; set; }
	public int Ram { get; set; }
	public string ProcessorModel { get; set; } = string.Empty;
	public Monitor Monitor { get; set; } = new();
	public Ports Ports { get; set; } = new();
}
