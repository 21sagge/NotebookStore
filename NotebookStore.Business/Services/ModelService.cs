namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class ModelService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<ModelDto>> GetModels()
	{
		var models = await unitOfWork.Models.Read();

		return mapper.Map<IEnumerable<ModelDto>>(models);
	}

	public async Task<ModelDto> GetModel(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		return mapper.Map<ModelDto>(model);
	}

	public async Task CreateModel(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Create(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task UpdateModel(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Update(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task DeleteModel(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task<bool> ModelExists(int id)
	{
		return await unitOfWork.Models.Find(id) != null;
	}
}
