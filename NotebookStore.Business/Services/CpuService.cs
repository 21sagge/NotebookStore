namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class CpuService : PermissionService, IService<CpuDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public CpuService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	: base(mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<CpuDto>> GetAll()
	{
		var cpus = await unitOfWork.Cpus.Read();
		var currentUser = await userService.GetCurrentUser();

		IEnumerable<CpuDto> result = cpus.Select(cpu =>
			AssignPermission<Cpu, CpuDto>(cpu, currentUser)
		);

		return result;
	}

	public async Task<CpuDto?> Find(int id)
	{
		var cpu = await unitOfWork.Cpus.Find(id);

		if (cpu == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		return AssignPermission<Cpu, CpuDto>(cpu, currentUser);
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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = AssignPermission<Cpu, CpuDto>(cpu, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = AssignPermission<Cpu, CpuDto>(cpu, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

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
