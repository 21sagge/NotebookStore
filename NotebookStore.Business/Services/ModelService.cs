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

		return mapper.Map<IEnumerable<ModelDto>>(models);
	}

	public async Task<ModelDto> Find(int id)
	{
		var model = await unitOfWork.Models.Find(id);

		return mapper.Map<ModelDto>(model);
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
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione del modello", ex);
		}
	}

	public async Task<bool> Update(ModelDto modelDto)
	{
		var model = mapper.Map<Model>(modelDto);

		unitOfWork.BeginTransaction();

		try
		{
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (model.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && model.CreatedBy != null)
			{
				throw new UnauthorizedAccessException("Non hai i permessi per modificare questo modello");
			}

			await unitOfWork.Models.Update(model);
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
			throw new Exception("Errore durante l'aggiornamento del modello", ex);
		}
	}

	public async Task<bool> Delete(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			var model = await unitOfWork.Models.Find(id);
			var currentUser = await userService.GetCurrentUser();
			currentUser.Role = await userService.IsInRole(currentUser.Id, "Admin") ? "Admin" : "User";

			if (model?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && model?.CreatedBy != null)
			{
				return false;
			}

			await unitOfWork.Models.Delete(id);
			await unitOfWork.SaveAsync();

			unitOfWork.CommitTransaction();

			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione del modello", ex);
		}
	}
}
