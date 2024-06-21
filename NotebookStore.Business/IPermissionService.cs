using NotebookStore.Entities;

namespace NotebookStore.Business;

public interface IPermissionService
{
    /// <summary>
    /// Assigns permission to the entity based on the current user.
    /// </summary>
    /// <param name="entity"> The entity to assign permission to. </param>
    /// <param name="currentUser"> The current user. </param>
    /// <typeparam name="T"> The type of the entity. </typeparam>
    /// <typeparam name="TDto"> The type of the DTO. </typeparam>
    /// <returns> The DTO with the permission assigned. </returns>
    //public TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
    //    where T : IAuditable
    //    where TDto : IAuditableDto;

    //public TOut CalculatePermission<TIn, TOut>
    //(
    //    TIn entity,
    //    UserDto currentUser,
    //    Func<TIn, TOut> mapping,
    //    Func<TOut, bool, TOut> action
    //) where TIn : IAuditable;

    bool CanUpdateBrand(Brand brand, UserDto currentUser);
    bool CanUpdateModel(Model model, UserDto currentUser);
    bool CanUpdateNotebook(Notebook notebook, UserDto currentUser);
}
