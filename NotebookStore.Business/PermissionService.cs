using System.Drawing.Drawing2D;
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

    //public TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
    //    where T : IAuditable
    //    where TDto : IAuditableDto
    //{
    //    var dto = mapper.Map<TDto>(entity);

    //    bool canUpdateDelete =
    //        currentUser.Role == "Admin" ||
    //        currentUser.Id == entity.CreatedBy ||
    //        entity.CreatedBy == null;

    //    dto.CanUpdate = canUpdateDelete;
    //    dto.CanDelete = canUpdateDelete;

    //    return dto;
    //}

    //public TOut CalculatePermission<TIn, TOut>(
    //    TIn entity,
    //    UserDto currentUser,
    //    Func<TIn, TOut> mapping,
    //    Func<TOut, bool, TOut> action) where TIn : IAuditable
    //{
    //    var dto = mapping(entity);

    //    bool canUpdateDelete =
    //        currentUser.Role == "Admin" ||
    //        currentUser.Id == entity.CreatedBy ||
    //        entity.CreatedBy == null;

    //    return action(dto, canUpdateDelete);
    //}

    //public BrandDto CalculateBrand(Brand entity, UserDto currentUser)
    //{
    //    return CalculatePermission
    //        (
    //            entity,
    //            currentUser,
    //            mapper.Map<BrandDto>,
    //            (dto, canAccess) =>
    //                {
    //                    dto.CanUpdate = canAccess;
    //                    dto.CanDelete = canAccess;
    //                    return dto;
    //                }
    //        );
    //}

    public bool CanUpdateBrand(Brand entity, UserDto currentUser)
    {
        return
            currentUser.Role == "Admin" ||
            currentUser.Id == entity.CreatedBy ||
            entity.CreatedBy == null;
    }

    public bool CanUpdateModel(Model entity, UserDto currentUser)
    {
        return
            currentUser.Role == "Admin" ||
            currentUser.Id == entity.CreatedBy ||
            entity.CreatedBy == null;
    }

    public bool CanUpdateNotebook(Notebook notebook, UserDto currentUser)
    {
        bool canUpdateBrand = CanUpdateBrand(notebook.Brand, currentUser);
        bool canUpdateModel = CanUpdateModel(notebook.Model, currentUser);

        return canUpdateBrand && canUpdateModel;
    }
}
