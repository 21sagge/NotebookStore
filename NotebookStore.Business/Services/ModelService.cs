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

		return models.Select(model =>
		{
			var modelDto = mapper.Map<ModelDto>(model);

			var canUpdateModel = permissionService.CanUpdateModel(model, currentUser);

			modelDto.CanUpdate = canUpdateModel;
			modelDto.CanDelete = canUpdateModel;

			return modelDto;
		});
	}

	public async Task<ModelDto?> Find(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		if (model == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		bool canUpdateModel = permissionService.CanUpdateModel(model, currentUser);

		var modelDto = mapper.Map<ModelDto>(model);

		modelDto.CanUpdate = canUpdateModel;
		modelDto.CanDelete = canUpdateModel;

		return modelDto;
	}

	public async Task<bool> Create(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		var currentUser = await userService.GetCurrentUser();

		model.CreatedBy = currentUser.Id;
		model.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		try
		{
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

		var currentUser = await userService.GetCurrentUser();

		var canUpdateModel = permissionService.CanUpdateModel(model, currentUser);

		if (!canUpdateModel)
		{
			return false;
		}

		modelDto.CanUpdate = canUpdateModel;
		modelDto.CanDelete = canUpdateModel;

		try
		{
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

		var currentUser = await userService.GetCurrentUser();

		if (!permissionService.CanUpdateModel(model, currentUser))
		{
			return false;
		}

		try
		{
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
