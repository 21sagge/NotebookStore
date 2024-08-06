using IbmImporter.Models;

namespace IbmImporter;

public class PortsValidator : IValidator<Ports>
{
	public string Validate(Ports model)
	{
		if (model == null)
		{
			return "Ports is null";
		}

		if (model.Hdmi is null && model.Usb is null)
		{
			return "Both HDMI and USB are null";
		}

		return string.Empty;
	}
}
