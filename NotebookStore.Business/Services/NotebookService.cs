namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class NotebookService : IService<NotebookDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public NotebookService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<NotebookDto>> GetAll()
	{
		var notebooks = await unitOfWork.Notebooks.Read();
		var notebookDtos = mapper.Map<IEnumerable<NotebookDto>>(notebooks);
		var currentUser = await userService.GetCurrentUser();

		foreach (var notebookDto in notebookDtos)
		{
			var notebook = await unitOfWork.Notebooks.Find(notebookDto.Id);

			if (notebook == null)
			{
				continue;
			}

			var createdBy = notebook.CreatedBy;

			notebookDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			notebookDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return notebookDtos;
	}

	public async Task<IEnumerable<BrandDto>> GetBrands()
	{
		var brands = await unitOfWork.Brands.Read();

		return mapper.Map<IEnumerable<BrandDto>>(brands);
	}

	public async Task<IEnumerable<CpuDto>> GetCpus()
	{
		var cpus = await unitOfWork.Cpus.Read();

		return mapper.Map<IEnumerable<CpuDto>>(cpus);
	}

	public async Task<IEnumerable<DisplayDto>> GetDisplays()
	{
		var displays = await unitOfWork.Displays.Read();

		return mapper.Map<IEnumerable<DisplayDto>>(displays);
	}

	public async Task<IEnumerable<MemoryDto>> GetMemories()
	{
		var memories = await unitOfWork.Memories.Read();

		return mapper.Map<IEnumerable<MemoryDto>>(memories);
	}

	public async Task<IEnumerable<ModelDto>> GetModels()
	{
		var models = await unitOfWork.Models.Read();

		return mapper.Map<IEnumerable<ModelDto>>(models);
	}

	public async Task<IEnumerable<StorageDto>> GetStorages()
	{
		var storages = await unitOfWork.Storages.Read();

		return mapper.Map<IEnumerable<StorageDto>>(storages);
	}

	public async Task<NotebookDto?> Find(int id)
	{
		var notebook = await unitOfWork.Notebooks.Find(id);

		if (notebook == null)
		{
			return null;
		}

		var notebookDto = mapper.Map<NotebookDto>(notebook);

		var currentUser = await userService.GetCurrentUser();

		if (notebook.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| notebook.CreatedBy == null)
		{
			notebookDto.CanUpdate = true;
			notebookDto.CanDelete = true;
		}

		return notebookDto;
	}

	public async Task<bool> Create(NotebookDto notebookDto)
	{
		var notebook = mapper.Map<Notebook>(notebookDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			notebook.CreatedBy = currentUser.Id;
			notebook.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			await unitOfWork.Notebooks.Create(notebook);
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

	public async Task<bool> Update(NotebookDto notebookDto)
	{
		var notebook = await unitOfWork.Notebooks.Find(notebookDto.Id);

		if (notebook == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			if (notebook.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& notebook.CreatedBy != null)
			{
				return false;
			}

			notebook.Color = notebookDto.Color;
			notebook.Price = notebookDto.Price;
			notebook.BrandId = notebookDto.BrandId;
			notebook.ModelId = notebookDto.ModelId;
			notebook.CpuId = notebookDto.CpuId;
			notebook.DisplayId = notebookDto.DisplayId;
			notebook.MemoryId = notebookDto.MemoryId;
			notebook.StorageId = notebookDto.StorageId;

			await unitOfWork.Notebooks.Update(notebook);
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
			var notebook = await unitOfWork.Notebooks.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (notebook?.CreatedBy != currentUser.Id
				&& currentUser.Role != "admin"
				&& notebook?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Notebooks.Delete(id);
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
