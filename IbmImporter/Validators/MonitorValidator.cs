using Monitor = IbmImporter.Models.Monitor;

namespace IbmImporter;

public class MonitorValidator : IValidator<Monitor>
{
	public bool Validate(Monitor model)
	{
		if (model == null)
		{
			return false;
		}

		if (model.Width == 0 ||
			model.Height == 0 ||
			model.SupportedRefreshRate == null ||
			model.SupportedRefreshRate.Count == 0)
		{
			return false;
		}

		return true;
	}
}
