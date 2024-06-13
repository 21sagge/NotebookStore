namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService : IService<StorageDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public StorageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<StorageDto>> GetAll()
	{
		var storages = await unitOfWork.Storages.Read();
		var storageDtos = mapper.Map<IEnumerable<StorageDto>>(storages);
		var currentUser = await userService.GetCurrentUser();

		foreach (var storageDto in storageDtos)
		{
			var storage = await unitOfWork.Storages.Find(storageDto.Id);

			if (storage == null)
			{
				continue;
			}

			var createdBy = storage.CreatedBy;

			storageDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			storageDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return storageDtos;
	}

	public async Task<StorageDto?> Find(int id)
	{
		var storage = await unitOfWork.Storages.Find(id);

		if (storage == null)
		{
			return null;
		}

		var storageDto = mapper.Map<StorageDto>(storage);

		var currentUser = await userService.GetCurrentUser();

		if (storage.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| storage.CreatedBy == null)
		{
			storageDto.CanUpdate = true;
			storageDto.CanDelete = true;
		}

		return storageDto;
	}

	public async Task<bool> Create(StorageDto storageDto)
	{
		var storage = mapper.Map<Storage>(storageDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			storage.CreatedBy = currentUser.Id;
			storage.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			await unitOfWork.Storages.Create(storage);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Update(StorageDto storageDto)
	{
		var storage = await unitOfWork.Storages.Find(storageDto.Id);

		if (storage == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			if (storage.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& storage.CreatedBy != null)
			{
				return false;
			}

			storage.Type = storageDto.Type;
			storage.Capacity = storageDto.Capacity;

			await unitOfWork.Storages.Update(storage);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			var storage = await unitOfWork.Storages.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (storage?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && storage?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Storages.Delete(id);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			return false;
		}
	}
}
