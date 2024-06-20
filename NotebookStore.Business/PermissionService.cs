using AutoMapper;
using NotebookStore.Entities;

namespace NotebookStore.Business;

public class PermissionService : IPermissionService
{
	private readonly IMapper mapper;

	public PermissionService(IMapper mapper)
	{
		this.mapper = mapper;
	}

	public TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
		where T : IAuditable
		where TDto : IAuditableDto
	{
		var dto = mapper.Map<TDto>(entity);

		bool canUpdateDelete =
			currentUser.Role == "Admin" ||
			currentUser.Id == entity.CreatedBy ||
			entity.CreatedBy == null;

		dto.CanUpdate = canUpdateDelete;
		dto.CanDelete = canUpdateDelete;

		return dto;
	}
}
