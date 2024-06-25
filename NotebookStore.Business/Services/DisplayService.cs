namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class DisplayService : IService<DisplayDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IPermissionService permissionService;

	public DisplayService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
		this.permissionService = permissionService;
	}

	public async Task<IEnumerable<DisplayDto>> GetAll()
	{
		var displays = await unitOfWork.Displays.Read();
		var currentUser = await userService.GetCurrentUser();

		return displays.Select(display =>
		{
			var displayDto = mapper.Map<DisplayDto>(display);

			var canUpdateDisplay = permissionService.CanUpdateDisplay(display, currentUser);

			displayDto.CanUpdate = canUpdateDisplay;
			displayDto.CanDelete = canUpdateDisplay;

			return displayDto;
		});
	}

	public async Task<DisplayDto?> Find(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		if (display == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		bool canUpdateDisplay = permissionService.CanUpdateDisplay(display, currentUser);

		var displayDto = mapper.Map<DisplayDto>(display);

		displayDto.CanUpdate = canUpdateDisplay;
		displayDto.CanDelete = canUpdateDisplay;

		return displayDto;
	}

	public async Task<bool> Create(DisplayDto displayDto)
	{
		var display = mapper.Map<Display>(displayDto);

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		display.CreatedBy = currentUser.Id;
		display.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		try
		{
			await unitOfWork.Displays.Create(display);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
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

		var currentUser = await userService.GetCurrentUser();

		var canUpdateDisplay = permissionService.CanUpdateDisplay(display, currentUser);

		if (!canUpdateDisplay)
		{
			return false;
		}

		displayDto.CanUpdate = canUpdateDisplay;
		displayDto.CanDelete = canUpdateDisplay;

		try
		{
			await unitOfWork.Displays.Update(mapper.Map(displayDto, display));
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			unitOfWork.RollbackTransaction();
			return false;
		}
	}

	public async Task<bool> Delete(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		if (display == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		if (!permissionService.CanUpdateDisplay(display, currentUser))
		{
			return false;
		}

		try
		{
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
