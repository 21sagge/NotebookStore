namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class CpuService : IService<CpuDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IPermissionService permissionService;

	public CpuService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
		this.permissionService = permissionService;
	}

	public async Task<IEnumerable<CpuDto>> GetAll()
	{
		var cpus = await unitOfWork.Cpus.Read();
		var currentUser = await userService.GetCurrentUser();

		return cpus.Select(cpu =>
		{
			var cpuDto = mapper.Map<CpuDto>(cpu);

			var canUpdateCpu = permissionService.CanUpdateCpu(cpu, currentUser);

			cpuDto.CanUpdate = canUpdateCpu;
			cpuDto.CanDelete = canUpdateCpu;

			return cpuDto;
		});
	}

	public async Task<CpuDto?> Find(int id)
	{
		var cpu = await unitOfWork.Cpus.Find(id);

		if (cpu == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		bool canUpdateCpu = permissionService.CanUpdateCpu(cpu, currentUser);

		var cpuDto = mapper.Map<CpuDto>(cpu);

		cpuDto.CanUpdate = canUpdateCpu;
		cpuDto.CanDelete = canUpdateCpu;

		return cpuDto;
	}

	public async Task<bool> Create(CpuDto cpuDto)
	{
		var cpu = mapper.Map<Cpu>(cpuDto);

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		cpu.CreatedBy = currentUser.Id;
		cpu.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		try
		{
			await unitOfWork.Cpus.Create(cpu);
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

	public async Task<bool> Update(CpuDto cpuDto)
	{
		var cpu = await unitOfWork.Cpus.Find(cpuDto.Id);

		if (cpu == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		var canUpdateCpu = permissionService.CanUpdateCpu(cpu, currentUser);

		if (!canUpdateCpu)
		{
			return false;
		}

		cpuDto.CanUpdate = canUpdateCpu;
		cpuDto.CanDelete = canUpdateCpu;

		try
		{
			await unitOfWork.Cpus.Update(mapper.Map(cpuDto, cpu));
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
		var cpu = await unitOfWork.Cpus.Find(id);

		if (cpu == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		if (!permissionService.CanUpdateCpu(cpu, currentUser))
		{
			return false;
		}

		try
		{
			await unitOfWork.Cpus.Delete(id);
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
