namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class ModelService : IService<ModelDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<ModelDto>> GetAll()
	{
		var models = await unitOfWork.Models.Read();

		return mapper.Map<IEnumerable<ModelDto>>(models);
	}

	public async Task<ModelDto> Find(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		return mapper.Map<ModelDto>(model);
	}

	public async Task<bool> Create(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Create(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione del modello", ex);
		}
	}

	public async Task<bool> Update(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Update(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento del modello", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione del modello", ex);
		}
	}
}
