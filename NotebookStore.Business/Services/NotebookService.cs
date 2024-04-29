namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class NotebookService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public NotebookService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<NotebookDto>> GetNotebooks()
	{
		var notebooks = await unitOfWork.Notebooks.Read();

		return mapper.Map<IEnumerable<NotebookDto>>(notebooks);
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

	public async Task<NotebookDto> GetNotebook(int id)
	{
		var notebook = await unitOfWork.Notebooks.Find(id);

		return mapper.Map<NotebookDto>(notebook);
	}

	public async Task<bool> CreateNotebook(NotebookDto notebookDto)
	{
		var notebook = mapper.Map<Notebook>(notebookDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Notebooks.Create(notebook);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile creare il notebook", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il notebook è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante la creazione del notebook", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> UpdateNotebook(NotebookDto notebookDto)
	{
		var notebook = mapper.Map<Notebook>(notebookDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Notebooks.Update(notebook);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile aggiornare il notebook", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il notebook è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante l'aggiornamento del notebook", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> DeleteNotebook(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Notebooks.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile eliminare il notebook", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il notebook è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante l'eliminazione del notebook", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> NotebookExists(int id)
	{
		return await unitOfWork.Notebooks.Find(id) != null;
	}
}
