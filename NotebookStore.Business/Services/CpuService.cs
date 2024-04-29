namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class CpuService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public CpuService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<CpuDto>> GetCpus()
	{
		var cpus = await unitOfWork.Cpus.Read();

		return mapper.Map<IEnumerable<CpuDto>>(cpus);
	}

	public async Task<CpuDto> GetCpu(int id)
	{
		var cpu = await unitOfWork.Cpus.Find(id);

		return mapper.Map<CpuDto>(cpu);
	}

	public async Task<bool> CreateCpu(CpuDto cpuDto)
	{
		var cpu = mapper.Map<Cpu>(cpuDto);

		unitOfWork.BeginTransaction();

		try
		{
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

	public async Task<bool> UpdateCpu(CpuDto cpuDto)
	{
		var cpu = mapper.Map<Cpu>(cpuDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Cpus.Update(cpu);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento del processore", ex);
		}
	}

	public async Task<bool> DeleteCpu(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
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

	public async Task<bool> CpuExists(int id)
	{
		return await unitOfWork.Cpus.Find(id) != null;
	}
}
