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
		var displayDtos = mapper.Map<IEnumerable<DisplayDto>>(displays);
		var currentUser = await userService.GetCurrentUser();

		foreach (var displayDto in displayDtos)
		{
			var display = await unitOfWork.Displays.Find(displayDto.Id);

			if (display == null)
			{
				continue;
			}

			var createdBy = display.CreatedBy;

			displayDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			displayDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return displayDtos;
	}

	public async Task<DisplayDto?> Find(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		if (display == null)
		{
			return null;
		}

		var displayDto = mapper.Map<DisplayDto>(display);

		var currentUser = await userService.GetCurrentUser();

		if (display.CreatedBy == currentUser.Id
			|| currentUser.Role == "admin"
			|| display.CreatedBy == null)
		{
			displayDto.CanUpdate = true;
			displayDto.CanDelete = true;
		}

		return displayDto;
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
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Update(DisplayDto displayDto)
	{
		var display = await unitOfWork.Displays.Find(displayDto.Id);

		if (display == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			if (display.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& display.CreatedBy != null)
			{
				return false;
			}

			display.Size = displayDto.Size;
			display.PanelType = displayDto.PanelType;
			display.ResolutionWidth = displayDto.ResolutionWidth;
			display.ResolutionHeight = displayDto.ResolutionHeight;

			await unitOfWork.Displays.Update(display);
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
			var display = await unitOfWork.Displays.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (display?.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& display?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Displays.Delete(id);
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
