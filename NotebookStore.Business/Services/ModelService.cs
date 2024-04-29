namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class ModelService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<ModelDto>> GetModels()
	{
		var models = await unitOfWork.Models.Read();

		return mapper.Map<IEnumerable<ModelDto>>(models);
	}

	public async Task<ModelDto> GetModel(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		return mapper.Map<ModelDto>(model);
	}

	public async Task<bool> CreateModel(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Create(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile creare il modello", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il modello è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante la creazione del modello", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> UpdateModel(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Update(model);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile aggiornare il modello", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il modello è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante l'aggiornamento del modello", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> DeleteModel(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Models.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (DbUpdateException ex)
		{
			throw new DbUpdateException("Impossibile eliminare il modello", ex);
		}
		catch (ArgumentNullException ex)
		{
			throw new ArgumentNullException("Il modello è nullo", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Errore durante l'eliminazione del modello", ex);
		}
		finally
		{
			unitOfWork.RollbackTransaction();
		}
	}

	public async Task<bool> ModelExists(int id)
	{
		return await unitOfWork.Models.Find(id) != null;
	}
}
