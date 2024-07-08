using NotebookStore.Entities;

namespace NotebookStore.Business;

public interface IPermissionService
{
    /// <summary>
    /// Calculates the permission for a brand.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the brand. </returns>
    bool CanUpdateBrand(Brand brand, UserDto currentUser);

    /// <summary>
    /// Calculates the permission for a model.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the model. </returns>
    bool CanUpdateModel(Model model, UserDto currentUser);

    /// <summary>
    /// Calculates the permission for a cpu.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the cpu. </returns>
    bool CanUpdateCpu(Cpu cpu, UserDto currentUser);

    /// <summary>
    /// Calculates the permission for a memory.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the memory. </returns>
    bool CanUpdateMemory(Memory memory, UserDto currentUser);

    /// <summary>
    /// Calculates the permission for a display.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the display. </returns>
    bool CanUpdateDisplay(Display display, UserDto currentUser);

    /// <summary>
    /// Calculates the permission for a storage.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the storage. </returns>
    bool CanUpdateStorage(Storage storage, UserDto currentUser);

    /// <summary>
    /// Checks if the current user has permission to update a notebook based on the entities of the notebook.
    /// </summary>
    /// <returns> A boolean value indicating whether the current user can update the notebook. </returns>
    bool CanUpdateNotebook(Notebook notebook, UserDto currentUser);
}
