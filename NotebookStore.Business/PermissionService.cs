using NotebookStore.Entities;

namespace NotebookStore.Business;

internal class PermissionService : IPermissionService
{
    public bool CanUpdateBrand(Brand entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Brand")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateModel(Model entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Model")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateCpu(Cpu entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Cpu")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateStorage(Storage entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Storage")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateMemory(Memory entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Memory")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateDisplay(Display entity, UserDto currentUser) =>
        entity != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == entity.CreatedBy && currentUser.Claims!.Contains("Edit Display")) ||
            entity.CreatedBy == null
        );

    public bool CanUpdateNotebook(Notebook notebook, UserDto currentUser) =>
        notebook != null && (
            currentUser.Roles.Contains("Admin") ||
            (currentUser.Id == notebook.CreatedBy && currentUser.Claims!.Contains("Edit Notebook")) ||
            notebook.CreatedBy == null
        ) &&
        CanUpdateBrand(notebook.Brand!, currentUser) &&
        CanUpdateModel(notebook.Model!, currentUser) &&
        CanUpdateCpu(notebook.Cpu!, currentUser) &&
        CanUpdateStorage(notebook.Storage!, currentUser) &&
        CanUpdateMemory(notebook.Memory!, currentUser) &&
        CanUpdateDisplay(notebook.Display!, currentUser);
}
