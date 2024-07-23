using NotebookStore.Entities;

namespace NotebookStore.Business;

internal class PermissionService : IPermissionService
{
    public bool CanUpdateBrand(Brand entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditBrand)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateModel(Model entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditModel)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateCpu(Cpu entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditCpu)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateStorage(Storage entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditStorage)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateMemory(Memory entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditMemory)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateDisplay(Display entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains(Claims.EditDisplay)) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateNotebook(Notebook notebook, UserDto currentUser) =>
        notebook != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == notebook.CreatedBy && currentUser.Claims!.Contains(Claims.EditNotebook)) ||
            notebook.CreatedBy == null
        ) &&
        CanUpdateBrand(notebook.Brand!, currentUser) &&
        CanUpdateModel(notebook.Model!, currentUser) &&
        CanUpdateCpu(notebook.Cpu!, currentUser) &&
        CanUpdateStorage(notebook.Storage!, currentUser) &&
        CanUpdateMemory(notebook.Memory!, currentUser) &&
        CanUpdateDisplay(notebook.Display!, currentUser);
}
