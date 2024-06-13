namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class ModelService : IService<ModelDto>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;
	private readonly IUserService userService;

	public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
		this.userService = userService;
	}

	public async Task<IEnumerable<ModelDto>> GetAll()
	{
		var models = await unitOfWork.Models.Read();
		var modelDtos = mapper.Map<IEnumerable<ModelDto>>(models);
		var currentUser = await userService.GetCurrentUser();

		foreach (var modelDto in modelDtos)
		{
			var model = await unitOfWork.Models.Find(modelDto.Id);

			if (model == null)
			{
				continue;
			}

			var createdBy = model.CreatedBy;

			modelDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			modelDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return modelDtos;
	}

	public async Task<ModelDto?> Find(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		if (model == null)
		{
			return null;
		}

		var modelDto = mapper.Map<ModelDto>(model);

		var currentUser = await userService.GetCurrentUser();

		if (model.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| model.CreatedBy == null)
		{
			modelDto.CanUpdate = true;
			modelDto.CanDelete = true;
		}

		return modelDto;
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
		catch (Exception)
		{
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

			if (model.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& model.CreatedBy != null)
			{
				return false;
			}

			model.Name = modelDto.Name;

			await unitOfWork.Models.Update(model);
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
			var model = await unitOfWork.Models.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (model?.CreatedBy != currentUser.Id
				&& currentUser.Role != "Admin"
				&& model?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Models.Delete(id);
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
