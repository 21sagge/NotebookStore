namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class DisplayService : BaseService, IService<DisplayDto>
{
	private readonly IUnitOfWork unitOfWork;
	// private readonly IMapper mapper;
	// private readonly IUserService userService;

	public DisplayService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	: base(mapper, userService)
	{
		this.unitOfWork = unitOfWork;
		// this.mapper = mapper;
		// this.userService = userService;
	}

	public async Task<IEnumerable<DisplayDto>> GetAll()
	{
		var displays = await unitOfWork.Displays.Read();
		var currentUser = await userService.GetCurrentUser();

		IEnumerable<DisplayDto> result = displays.Select(display =>
			AssignPermission<Display, DisplayDto>(display, currentUser)
		);

		return result;
	}

	public async Task<DisplayDto?> Find(int id)
	{
		var display = await unitOfWork.Displays.Find(id);

		if (display == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		return AssignPermission<Display, DisplayDto>(display, currentUser);
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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = AssignPermission<Display, DisplayDto>(display, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = AssignPermission<Display, DisplayDto>(display, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
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
