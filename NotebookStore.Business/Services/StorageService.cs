namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService : IService<StorageDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IPermissionService permissionService;

    public StorageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userService = userService;
        this.permissionService = permissionService;
    }

    public async Task<IEnumerable<StorageDto>> GetAll()
    {
        var storages = await unitOfWork.Storages.Read();
        var currentUser = await userService.GetCurrentUser();

        return storages.Select(storage =>
        {
            var storageDto = mapper.Map<StorageDto>(storage);

            var canUpdateStorage = permissionService.CanUpdateStorage(storage, currentUser);

            storageDto.CanUpdate = canUpdateStorage;
            storageDto.CanDelete = canUpdateStorage;

            return storageDto;
        });
    }

    public async Task<StorageDto?> Find(int id)
    {
        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return null;
        }

        var currentUser = await userService.GetCurrentUser();

        bool canUpdateStorage = permissionService.CanUpdateStorage(storage, currentUser);

        var storageDto = mapper.Map<StorageDto>(storage);

        storageDto.CanUpdate = canUpdateStorage;
        storageDto.CanDelete = canUpdateStorage;

        return storageDto;
    }

    public async Task<bool> Create(StorageDto storageDto)
    {
        var storage = mapper.Map<Storage>(storageDto);

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        storage.CreatedBy = currentUser.Id;
        storage.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        try
        {
            await unitOfWork.Storages.Create(storage);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception)
        {
            unitOfWork.RollbackTransaction();
            return false;
        }
    }

    public async Task<bool> Update(StorageDto storageDto)
    {
        var storage = await unitOfWork.Storages.Find(storageDto.Id);

        if (storage == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        var canUpdateStorage = permissionService.CanUpdateStorage(storage, currentUser);

        if (!canUpdateStorage)
        {
            return false;
        }

        storageDto.CanUpdate = canUpdateStorage;
        storageDto.CanDelete = canUpdateStorage;

        try
        {
            await unitOfWork.Storages.Update(mapper.Map(storageDto, storage));
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
        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        if (!permissionService.CanUpdateStorage(storage, currentUser))
        {
            return false;
        }

        try
        {
            await unitOfWork.Storages.Delete(id);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception)
        {
            unitOfWork.RollbackTransaction();
            return false;
        }
    }

    // private readonly Func<Storage, UserDto, IMapper, StorageDto> AssignPermission =
    //     (Storage storage, UserDto currentUser, IMapper mapper) =>
    //     {
    //         var storageDto = mapper.Map<StorageDto>(storage);

    //         var createdBy = storage?.CreatedBy;

    //         storageDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
    //         storageDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;

    //         return storageDto;
    //     };
}
