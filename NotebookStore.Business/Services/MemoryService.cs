namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class MemoryService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public MemoryService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<MemoryDto>> GetMemories()
	{
		var memories = await unitOfWork.Memories.Read();

		return mapper.Map<IEnumerable<MemoryDto>>(memories);
	}

	public async Task<MemoryDto> GetMemory(int id)
	{
		var memory = await unitOfWork.Memories.Find(id);

		return mapper.Map<MemoryDto>(memory);
	}

	public async Task CreateMemory(MemoryDto memoryDto)
	{
		var memory = mapper.Map<Memory>(memoryDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Memories.Create(memory);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task UpdateMemory(MemoryDto memoryDto)
	{
		var memory = mapper.Map<Memory>(memoryDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Memories.Update(memory);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task DeleteMemory(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Memories.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task<bool> MemoryExists(int id)
	{
		return await unitOfWork.Memories.Find(id) != null;
	}
}
