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

		if (model.Hdmi == 0 &&
			model.Usb == 0)
		{
			return false;
		}

		return true;
	}
}
