namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class MemoryService : IService<MemoryDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IPermissionService permissionService;

	public MemoryService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
		this.permissionService = permissionService;
	}

	public async Task<IEnumerable<MemoryDto>> GetAll()
	{
		var memories = await unitOfWork.Memories.Read();
		var currentUser = await userService.GetCurrentUser();

		IEnumerable<MemoryDto> result = memories.Select(memory =>
			permissionService.AssignPermission<Memory, MemoryDto>(memory, currentUser)
		);

		return result;
	}

	public async Task<MemoryDto?> Find(int id)
	{
		var memory = await unitOfWork.Memories.Find(id);

		if (memory == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		return permissionService.AssignPermission<Memory, MemoryDto>(memory, currentUser);
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
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Update(MemoryDto memoryDto)
	{
		var memory = await unitOfWork.Memories.Find(memoryDto.Id);

		if (memory == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = permissionService.AssignPermission<Memory, MemoryDto>(memory, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

			await unitOfWork.Memories.Update(mapper.Map(memoryDto, memory));
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Delete(int id)
	{
		var memory = await unitOfWork.Memories.Find(id);

		if (memory == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = permissionService.AssignPermission<Memory, MemoryDto>(memory, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

			await unitOfWork.Memories.Delete(id);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			unitOfWork.RollbackTransaction();
			return false;
		}
	}
}
