using Monitor = IbmImporter.Models.Monitor;

namespace IbmImporter;

public class MonitorValidator : IValidator<Monitor>
{
	public string Validate(Monitor model)
	{
		if (model == null)
		{
			return "Monitor is null";
		}

		if (model.Width == 0) return "Width is 0";

		if (model.Height == 0) return "Height is 0";

		if (model.SupportedRefreshRate.Count == 0) return "Supported refresh rate is empty";

		return string.Empty;
	}
}
