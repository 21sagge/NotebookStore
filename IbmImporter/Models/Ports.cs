namespace IbmImporter.Models;

public class Ports
{
	public int? Usb { get; set; }
	public int? Hdmi { get; set; }

	public override string ToString()
	{
		return $"Usb: {Usb}, Hdmi: {Hdmi}";
	}
}
