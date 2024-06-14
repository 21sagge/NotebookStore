namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class StorageService : IService<StorageDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    //public AssignPermission Handler { get; set; }

    public StorageService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
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

        IEnumerable<StorageDto> result = storages.Select<Storage, StorageDto>(storage =>
            AssignPermission(storage, currentUser, mapper)
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

        var result = AssignPermission(storage, currentUser, mapper);

        return result;

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
        //AssignPermission(storageDto,)

        var storage = await unitOfWork.Storages.Find(storageDto.Id);

        if (storage == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();

            if (storage.CreatedBy != currentUser.Id
                && currentUser.Role != "Admin"
                && storage.CreatedBy != null)
            {
                return false;
            }

            storage.Type = storageDto.Type;
            storage.Capacity = storageDto.Capacity;

            await unitOfWork.Storages.Update(storage);
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

    public async Task<bool> Delete(int id)
    {
        unitOfWork.BeginTransaction();

        try
        {
            var storage = await unitOfWork.Storages.Find(id);
            var currentUser = await userService.GetCurrentUser();

            if (storage?.CreatedBy != currentUser.Id && currentUser.Role != "Admin" && storage?.CreatedBy != null)
            {
                return false;
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

    #region private function
    private Func<Storage, UserDto, IMapper, StorageDto> AssignPermission =
        (Storage storage, UserDto currentUser, IMapper mapper) =>
        {
            var result = mapper.Map<StorageDto>(storage);

            var createdBy = storage?.CreatedBy;

            result.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
            result.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;

            return result;
        };
    #endregion
}
