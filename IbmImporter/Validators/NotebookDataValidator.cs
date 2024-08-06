using IbmImporter.Models;

namespace IbmImporter;

public class NotebookDataValidator : IValidator<NotebookData>
{
	private readonly IValidator<Notebook> notebookValidator;

	public NotebookDataValidator(IValidator<Notebook> notebookValidator)
	{
		this.notebookValidator = notebookValidator;
	}

	public string Validate(NotebookData model)
	{
		if (model == null)
		{
			return "NotebookData is null";
		}

		if (string.IsNullOrEmpty(model.Customer) ||
			model.Notebooks == null ||
			model.Notebooks.Count == 0 ||
			model.Notebooks.Any(notebook => notebookValidator.Validate(notebook) != string.Empty))
		{
			return "NotebookData is invalid";
		}

		return string.Empty;
	}
}
