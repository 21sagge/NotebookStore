namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class CpuService : IService<CpuDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public CpuService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<CpuDto>> GetAll()
	{
		var cpus = await unitOfWork.Cpus.Read();

		return mapper.Map<IEnumerable<CpuDto>>(cpus);
	}

	public async Task<CpuDto> Find(int id)
	{
		var cpu = await unitOfWork.Cpus.Find(id);

		return mapper.Map<CpuDto>(cpu);
	}

	public async Task<bool> Create(CpuDto cpuDto)
	{
		var cpu = mapper.Map<Cpu>(cpuDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			cpu.CreatedBy = currentUser.Id;
			cpu.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			await unitOfWork.Cpus.Create(cpu);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione del processore", ex);
		}
	}

	public async Task<bool> Update(CpuDto cpuDto)
	{
		var cpu = mapper.Map<Cpu>(cpuDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (cpu.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && cpu.CreatedBy != null)
			{
				throw new UnauthorizedAccessException("Non sei autorizzato a modificare questo processore");
			}

			await unitOfWork.Cpus.Update(cpu);
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
			throw new Exception("Errore durante l'aggiornamento del processore", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";
			var cpu = await unitOfWork.Cpus.Find(id);

			if (cpu?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && cpu?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Cpus.Delete(id);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione del processore", ex);
		}
	}
}
