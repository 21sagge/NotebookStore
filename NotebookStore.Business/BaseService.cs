using AutoMapper;

namespace NotebookStore.Business;

public abstract class BaseService
{
	protected readonly IMapper mapper;
	protected readonly IUserService userService;

	public BaseService(IMapper mapper, IUserService userService)
	{
		this.mapper = mapper;
		this.userService = userService;
	}

	/// <summary>
	/// Assigns permission to the entity.
	/// </summary>
	/// <returns>The DTO with the permission assigned.</returns>
	protected TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
		where T : class
		where TDto : class
	{
		var dto = mapper.Map<TDto>(entity);

		var createdByProperty = typeof(T).GetProperty("CreatedBy");
		string? createdBy = (string?)(createdByProperty?.GetValue(entity));

		var canUpdateProperty = typeof(TDto).GetProperty("CanUpdate");
		var canDeleteProperty = typeof(TDto).GetProperty("CanDelete");

		bool canUpdateDelete =
			createdBy == currentUser.Id ||
			currentUser.Role == "Admin" ||
			createdBy == null;

		canUpdateProperty?.SetValue(dto, canUpdateDelete);
		canDeleteProperty?.SetValue(dto, canUpdateDelete);

		return dto;
	}
}
