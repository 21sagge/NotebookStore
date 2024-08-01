namespace IbmImporter.Models;

public class Monitor
{
	public int Width { get; set; }
	public int Height { get; set; }
	public List<int> SupportedRefreshRate { get; set; } = new();
}
