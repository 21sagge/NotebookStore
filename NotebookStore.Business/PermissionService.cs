using AutoMapper;

namespace NotebookStore.Business;

public class PermissionService : IPermissionService
{
	private readonly IMapper mapper;

	public PermissionService(IMapper mapper)
	{
		this.mapper = mapper;
	}

	public TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
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
