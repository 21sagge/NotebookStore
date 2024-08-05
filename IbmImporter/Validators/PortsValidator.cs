using IbmImporter.Models;

namespace IbmImporter;

public class PortsValidator : IValidator<Ports>
{
	public bool Validate(Ports model)
	{
		if (model == null)
		{
			return false;
		}

		if (model.Hdmi is null && model.Usb is null)
		{
			return false;
		}

		return true;
	}
}
