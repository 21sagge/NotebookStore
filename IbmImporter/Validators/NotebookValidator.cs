using IbmImporter.Models;
using Monitor = IbmImporter.Models.Monitor;

namespace IbmImporter;

public class NotebookValidator : IValidator<Notebook>
{
	private readonly IValidator<Monitor> monitorValidator;
	private readonly IValidator<Ports> portsValidator;

	public NotebookValidator(IValidator<Monitor> monitorValidator, IValidator<Ports> portsValidator)
	{
		this.monitorValidator = monitorValidator;
		this.portsValidator = portsValidator;
	}

	public string Validate(Notebook model)
	{
		if (model == null)
		{
			return "Notebook is null";
		}

		if (string.IsNullOrEmpty(model.Name)) return "Name is null or empty";

		if (model.Price == 0) return "Price is 0";

		if (model.Ram == 0) return "Ram is 0";

		if (model.CPU == 0) return "CPU is 0";

		if (string.IsNullOrEmpty(model.Color)) return "Color is null or empty";

		if (string.IsNullOrEmpty(model.ProcessorModel)) return "Processor model is null or empty";

		if (model.DateOfProduction == DateTime.MinValue) return "Date of production is not set";

		if (model.Monitor == null) return "Monitor is null";

		var monitorValidationResult = monitorValidator.Validate(model.Monitor);
		if (!string.IsNullOrEmpty(monitorValidator.Validate(model.Monitor))) return monitorValidationResult;

		var portsValidationResult = portsValidator.Validate(model.Ports);
		if (!string.IsNullOrEmpty(portsValidationResult)) return portsValidationResult;

		return string.Empty;
	}
}
