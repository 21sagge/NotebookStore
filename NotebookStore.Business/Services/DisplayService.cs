namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class DisplayService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public DisplayService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<DisplayDto>> GetDisplays()
	{
		var displays = await unitOfWork.Displays.Read();

		return mapper.Map<IEnumerable<DisplayDto>>(displays);
	}

	public async Task<DisplayDto> GetDisplay(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		return mapper.Map<DisplayDto>(display);
	}

	public async Task<bool> CreateDisplay(DisplayDto displayDto)
	{
		var display = mapper.Map<Display>(displayDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Displays.Create(display);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione del display", ex);
		}
	}

	public async Task<bool> UpdateDisplay(DisplayDto displayDto)
	{
		var display = mapper.Map<Display>(displayDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Displays.Update(display);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento del display", ex);
		}
	}

	public async Task<bool> DeleteDisplay(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Displays.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione del display", ex);
		}
	}

	public async Task<bool> DisplayExists(int id)
	{
		return await unitOfWork.Displays.Find(id) != null;
	}
}
