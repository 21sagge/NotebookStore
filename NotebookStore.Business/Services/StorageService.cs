namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService : IService<StorageDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public StorageService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<StorageDto>> GetAll()
	{
		var storages = await unitOfWork.Storages.Read();

		return mapper.Map<IEnumerable<StorageDto>>(storages);
	}

	public async Task<StorageDto> Find(int id)
	{
		var storage = await unitOfWork.Storages.Find(id);

		return mapper.Map<StorageDto>(storage);
	}

	public async Task<bool> Create(StorageDto storageDto)
	{
		var storage = mapper.Map<Storage>(storageDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Create(storage);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione dello storage", ex);
		}
	}

	public async Task<bool> Update(StorageDto storageDto)
	{
		var storage = mapper.Map<Storage>(storageDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Update(storage);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento dello storage", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Storages.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione dello storage", ex);
		}
	}
}
