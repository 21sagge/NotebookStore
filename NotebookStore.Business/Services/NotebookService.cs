namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class NotebookService : IService<NotebookDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IPermissionService permissionService;

    public NotebookService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userService = userService;
        this.permissionService = permissionService;
    }

    public async Task<IEnumerable<NotebookDto>> GetAll()
    {
        var notebooks = await unitOfWork.Notebooks.Read();
        var currentUser = await userService.GetCurrentUser();

        return notebooks.Select(notebook =>
        {
            var notebookDto = mapper.Map<NotebookDto>(notebook);

            var canUpdateNotebook = permissionService.CanUpdateNotebook(notebook, currentUser);

            notebookDto.CanUpdate = canUpdateNotebook;
            notebookDto.CanDelete = canUpdateNotebook;

            return notebookDto;
        });
    }

    public async Task<NotebookDto?> Find(int id)
    {
        var notebook = await unitOfWork.Notebooks.Find(id);

        if (notebook == null)
        {
            return null;
        }

        var currentUser = await userService.GetCurrentUser();

        var canUpdateNotebook = permissionService.CanUpdateNotebook(notebook, currentUser);

        var notebookDto = mapper.Map<NotebookDto>(notebook);

        notebookDto.CanUpdate = canUpdateNotebook;
        notebookDto.CanDelete = canUpdateNotebook;

        return notebookDto;
    }

    public async Task<bool> Create(NotebookDto notebookDto)
    {
        var notebook = mapper.Map<Notebook>(notebookDto);

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        notebook.Brand = await unitOfWork.Brands.Find(notebook.BrandId);
        notebook.Model = await unitOfWork.Models.Find(notebook.ModelId);
        notebook.Cpu = await unitOfWork.Cpus.Find(notebook.CpuId);
        notebook.Display = await unitOfWork.Displays.Find(notebook.DisplayId);
        notebook.Memory = await unitOfWork.Memories.Find(notebook.MemoryId);
        notebook.Storage = await unitOfWork.Storages.Find(notebook.StorageId);

        notebook.CreatedBy = currentUser.Id;
        notebook.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (!permissionService.CanUpdateNotebook(notebook, currentUser))
        {
            return false;
        }

        try
        {
            await unitOfWork.Notebooks.Create(notebook);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            unitOfWork.RollbackTransaction();
            return false;
        }
    }

    public async Task<bool> Update(NotebookDto notebookDto)
    {
        var notebook = await unitOfWork.Notebooks.Find(notebookDto.Id);

        if (notebook == null)
        {
            return false;
        }

        var currentUser = await userService.GetCurrentUser();

        unitOfWork.BeginTransaction();

        var canUpdateNotebook = permissionService.CanUpdateNotebook(notebook, currentUser);

        if (!canUpdateNotebook)
        {
            return false;
        }

        notebookDto.CanUpdate = canUpdateNotebook;
        notebookDto.CanDelete = canUpdateNotebook;

        notebook = mapper.Map(notebookDto, notebook);

        notebook.Brand = await unitOfWork.Brands.Find(notebook.BrandId);
        notebook.Model = await unitOfWork.Models.Find(notebook.ModelId);
        notebook.Cpu = await unitOfWork.Cpus.Find(notebook.CpuId);
        notebook.Display = await unitOfWork.Displays.Find(notebook.DisplayId);
        notebook.Memory = await unitOfWork.Memories.Find(notebook.MemoryId);
        notebook.Storage = await unitOfWork.Storages.Find(notebook.StorageId);

        try
        {
            await unitOfWork.Notebooks.Update(notebook);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            unitOfWork.RollbackTransaction();
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        var notebook = await unitOfWork.Notebooks.Find(id);

        if (notebook == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        if (!permissionService.CanUpdateNotebook(notebook, currentUser))
        {
            return false;
        }

        try
        {
            await unitOfWork.Notebooks.Delete(id);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            unitOfWork.RollbackTransaction();
            return false;
        }
    }
}
