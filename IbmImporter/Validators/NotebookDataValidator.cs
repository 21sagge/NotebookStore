using IbmImporter.Models;

namespace IbmImporter;

public class NotebookDataValidator : IValidator<NotebookData>
{
	private readonly IValidator<Notebook> notebookValidator;

	public NotebookDataValidator(IValidator<Notebook> notebookValidator)
	{
		this.notebookValidator = notebookValidator;
	}

	public bool Validate(NotebookData model)
	{
		if (model == null)
		{
			return false;
		}

		if (string.IsNullOrEmpty(model.Customer) ||
			model.Notebooks.TrueForAll(notebook => notebookValidator.Validate(notebook) == false))
		{
			return false;
		}

		return true;
	}
}
