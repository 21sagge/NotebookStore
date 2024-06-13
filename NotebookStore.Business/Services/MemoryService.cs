namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class MemoryService : IService<MemoryDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public MemoryService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<MemoryDto>> GetAll()
	{
		var memories = await unitOfWork.Memories.Read();
		var memoryDtos = mapper.Map<IEnumerable<MemoryDto>>(memories);
		var currentUser = await userService.GetCurrentUser();

		foreach (var memoryDto in memoryDtos)
		{
			var memory = await unitOfWork.Memories.Find(memoryDto.Id);

			if (memory == null)
			{
				continue;
			}

			var createdBy = memory.CreatedBy;

			memoryDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			memoryDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return memoryDtos;
	}

	public async Task<MemoryDto?> Find(int id)
	{
		var memory = await unitOfWork.Memories.Find(id);

		if (memory == null)
		{
			return null;
		}

		var memoryDto = mapper.Map<MemoryDto>(memory);

		var currentUser = await userService.GetCurrentUser();

		if (memory.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| memory.CreatedBy == null)
		{
			memoryDto.CanUpdate = true;
			memoryDto.CanDelete = true;
		}

		return memoryDto;
	}

	public async Task<bool> Create(MemoryDto memoryDto)
	{
		var memory = mapper.Map<Memory>(memoryDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			memory.CreatedBy = currentUser.Id;
			memory.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			await unitOfWork.Memories.Create(memory);
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

	public async Task<bool> Update(MemoryDto memoryDto)
	{
		var memory = mapper.Map<Memory>(memoryDto);

		if (memory == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			if (memory.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& memory.CreatedBy != null)
			{
				return false;
			}

			memory.Capacity = memoryDto.Capacity;
			memory.Speed = memoryDto.Speed;

			await unitOfWork.Memories.Update(memory);
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
			var memory = await unitOfWork.Memories.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (memory?.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& memory?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Memories.Delete(id);
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
