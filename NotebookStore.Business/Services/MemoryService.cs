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

		return mapper.Map<IEnumerable<MemoryDto>>(memories);
	}

	public async Task<MemoryDto> Find(int id)
	{
		var memory = await unitOfWork.Memories.Find(id);

		return mapper.Map<MemoryDto>(memory);
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
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione della memoria", ex);
		}
	}

	public async Task<bool> Update(MemoryDto memoryDto)
	{
		var memory = mapper.Map<Memory>(memoryDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (memory.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && memory.CreatedBy != null)
			{
				throw new UnauthorizedAccessException("Non sei autorizzato a modificare questa memoria");
			}

			await unitOfWork.Memories.Update(memory);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();
			
			return true;
		}
		catch (UnauthorizedAccessException)
		{
			unitOfWork.RollbackTransaction();
			return false;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento della memoria", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			var memory = await unitOfWork.Memories.Find(id);
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (memory?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && memory?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Memories.Delete(id);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione della memoria", ex);
		}
	}
}
