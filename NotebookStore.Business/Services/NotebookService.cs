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

        IEnumerable<NotebookDto> result = notebooks.Select(notebook =>
        {
            var can = permissionService.CanUpdateNotebook(notebook, currentUser);

            var notebookDto = mapper.Map<NotebookDto>(notebook);

            notebookDto.CanUpdate = can;
            notebookDto.CanDelete = can;

            return notebookDto;
        });

        return result;
    }

    public async Task<NotebookDto?> Find(int id)
    {
        var notebook = await unitOfWork.Notebooks.Find(id);

        if (notebook == null)
        {
            return null;
        }

        var currentUser = await userService.GetCurrentUser();

        var result = permissionService.AssignPermission<Notebook, NotebookDto>(notebook, currentUser);

        return result;
    }

    public async Task<bool> Create(NotebookDto notebookDto)
    {
        var notebook = mapper.Map<Notebook>(notebookDto);

        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();

            notebook.CreatedBy = currentUser.Id;
            notebook.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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

        notebook.BrandId = notebookDto.BrandId;
        notebook.ModelId = notebookDto.ModelId;
        notebook.CpuId = notebookDto.CpuId;
        notebook.DisplayId = notebookDto.DisplayId;
        notebook.MemoryId = notebookDto.MemoryId;
        notebook.StorageId = notebookDto.StorageId;
        notebook.Color = notebookDto.Color;
        notebook.Price = notebookDto.Price;

        var currentUser = await userService.GetCurrentUser();

        if (!permissionService.CanUpdateNotebook(notebook, currentUser))
        {
            return false;
        }

        unitOfWork.BeginTransaction();

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

        try
        {
            var currentUser = await userService.GetCurrentUser();

            var result = permissionService.AssignPermission<Notebook, NotebookDto>(notebook, currentUser);

            if (!result.CanUpdate || !result.CanDelete)
            {
                throw new Exception("Permission denied");
            }

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
