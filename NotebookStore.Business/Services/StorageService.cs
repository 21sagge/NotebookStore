namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService : PermissionService, IService<StorageDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    //public AssignPermission Handler { get; set; }

    public StorageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    : base(mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userService = userService;

        //this.Handler = (a, b, c) => a;
        //this.Handler = AssignPermission;
    }

    public async Task<IEnumerable<StorageDto>> GetAll()
    {
        var storages = await unitOfWork.Storages.Read();
        var currentUser = await userService.GetCurrentUser();

        IEnumerable<StorageDto> result = storages.Select(storage =>
            AssignPermission<Storage, StorageDto>(storage, currentUser)
        );

        return result;

        //foreach (var storageDto in storageDtos)
        //{
        //    var storage = await unitOfWork.Storages.Find(storageDto.Id);

        //    if (storage == null)
        //    {
        //        continue;
        //    }

        //    var createdBy = storage.CreatedBy;

        //    storageDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
        //    storageDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
        //}

        //return storageDtos;
    }

    public async Task<StorageDto?> Find(int id)
    {
        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return null;
        }

        var currentUser = await userService.GetCurrentUser();

        return AssignPermission<Storage, StorageDto>(storage, currentUser);

        // return mapper.Map<StorageDto>(storage);

        //if (storage.CreatedBy == currentUser.Id
        //    || currentUser.Role == "Admin"
        //    || storage.CreatedBy == null)
        //{
        //    storageDto.CanUpdate = true;
        //    storageDto.CanDelete = true;
        //}

        //return storageDto;
    }

    public async Task<bool> Create(StorageDto storageDto)
    {
        var storage = mapper.Map<Storage>(storageDto);

        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();

            storage.CreatedBy = currentUser.Id;
            storage.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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

        try
        {
            var currentUser = await userService.GetCurrentUser();

            var result = AssignPermission<Storage, StorageDto>(storage, currentUser);

            if (!result.CanUpdate || !result.CanDelete)
            {
                throw new Exception("Permission denied");
            }

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

        try
        {
            var currentUser = await userService.GetCurrentUser();

            var result = AssignPermission<Storage, StorageDto>(storage, currentUser);

            if (!result.CanUpdate || !result.CanDelete)
            {
                throw new Exception("Permission denied");
            }

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
