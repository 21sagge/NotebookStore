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
		var cpuDtos = mapper.Map<IEnumerable<CpuDto>>(cpus);
		var currentUser = await userService.GetCurrentUser();

		foreach (var cpuDto in cpuDtos)
		{
			var cpu = await unitOfWork.Cpus.Find(cpuDto.Id);

			if (cpu == null)
			{
				continue;
			}

			var createdBy = cpu.CreatedBy;

			cpuDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			cpuDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return cpuDtos;
	}

	public async Task<CpuDto?> Find(int id)
	{
		var cpu = await unitOfWork.Cpus.Find(id);

		if (cpu == null)
		{
			return null;
		}

		var cpuDto = mapper.Map<CpuDto>(cpu);

		var currentUser = await userService.GetCurrentUser();

		if (cpu.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| cpu.CreatedBy == null)
		{
			cpuDto.CanUpdate = true;
			cpuDto.CanDelete = true;
		}

		return cpuDto;
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

			if (cpu.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& cpu.CreatedBy != null)
			{
				return false;
			}

			cpu.Brand = cpuDto.Brand;
			cpu.Model = cpuDto.Model;

			await unitOfWork.Cpus.Update(cpu);
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
			var cpu = await unitOfWork.Cpus.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (cpu?.CreatedBy != currentUser.Id
								&& currentUser.Role != "Admin"
								&& cpu?.CreatedBy != null)
			{
				return false;
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
