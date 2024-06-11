namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class DisplayService : IService<DisplayDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public DisplayService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<DisplayDto>> GetAll()
	{
		var displays = await unitOfWork.Displays.Read();

		return mapper.Map<IEnumerable<DisplayDto>>(displays);
	}

	public async Task<DisplayDto> Find(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		return mapper.Map<DisplayDto>(display);
	}

	public async Task<bool> Create(DisplayDto displayDto)
	{
		var display = mapper.Map<Display>(displayDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			display.CreatedBy = currentUser.Id;
			display.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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

	public async Task<bool> Update(DisplayDto displayDto)
	{
		var display = mapper.Map<Display>(displayDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (display.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && display.CreatedBy != null)
			{
				throw new UnauthorizedAccessException("Non sei autorizzato a modificare questo display");
			}

			await unitOfWork.Displays.Update(display);
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
			throw new Exception("Errore durante l'aggiornamento del display", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			var display = await unitOfWork.Displays.Find(id);
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (display?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && display?.CreatedBy != null)
			{
				return false;
			}

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
}
