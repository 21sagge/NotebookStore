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

	public bool Validate(Notebook model)
	{
		if (model == null)
		{
			return false;
		}

		if (string.IsNullOrEmpty(model.Name) ||
			string.IsNullOrEmpty(model.Color) ||
			string.IsNullOrEmpty(model.ProcessorModel) ||
			monitorValidator.Validate(model.Monitor) == false ||
			portsValidator.Validate(model.Ports) == false)
		{
			return false;
		}

		return true;
	}
}
