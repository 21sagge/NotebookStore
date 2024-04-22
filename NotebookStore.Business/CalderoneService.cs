namespace NotebookStore.Business;

using NotebookStore.DAL;

public class CalderoneService
{
	private readonly IUnitOfWork unitOfWork;

	public CalderoneService(IUnitOfWork unitOfWork)
	{
		this.unitOfWork = unitOfWork;
	}

}
