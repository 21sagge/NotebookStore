namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public StorageService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<StorageDto>> GetStorages()
	{
		var storages = await unitOfWork.Storages.Read();

		return mapper.Map<IEnumerable<StorageDto>>(storages);
	}

	public async Task<StorageDto> GetStorage(int id)
	{
		var storage = await unitOfWork.Storages.Find(id);

		return mapper.Map<StorageDto>(storage);
	}

	public async Task CreateStorage(StorageDto storageDto)
	{
		var storage = mapper.Map<Storage>(storageDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Create(storage);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task UpdateStorage(StorageDto storageDto)
	{
		var storage = mapper.Map<Storage>(storageDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Update(storage);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task DeleteStorage(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task<bool> StorageExists(int id)
	{
		return await unitOfWork.Storages.Find(id) != null;
	}
}
