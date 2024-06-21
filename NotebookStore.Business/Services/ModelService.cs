namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class ModelService : IService<ModelDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;
    private readonly IPermissionService permissionService;

    public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
        this.permissionService = permissionService;
    }

	public async Task<IEnumerable<ModelDto>> GetAll()
	{
		var models = await unitOfWork.Models.Read();
		var currentUser = await userService.GetCurrentUser();

		IEnumerable<ModelDto> result = models.Select(model =>
			permissionService.AssignPermission<Model, ModelDto>(model, currentUser)
		);

		return result;
	}

	public async Task<ModelDto?> Find(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		if (model == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		return permissionService.AssignPermission<Model, ModelDto>(model, currentUser);
	}

	public async Task<bool> Create(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			model.CreatedBy = currentUser.Id;
			model.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			await unitOfWork.Models.Create(model);
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

	public async Task<bool> Update(ModelDto modelDto)
	{
		var model = await unitOfWork.Models.Find(modelDto.Id);

		if (model == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = permissionService.AssignPermission<Model, ModelDto>(model, currentUser);

			if (!result.CanDelete || !result.CanUpdate)
			{
				throw new Exception("Permission denied");
			}

			await unitOfWork.Models.Update(mapper.Map(modelDto, model));
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
		var model = await unitOfWork.Models.Find(id);

		if (model == null)
		{
			return false;
		}

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = permissionService.AssignPermission<Model, ModelDto>(model, currentUser);

			if (!result.CanDelete || !result.CanUpdate)
			{
				throw new Exception("Permission denied");
			}

			await unitOfWork.Models.Delete(id);
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
}
