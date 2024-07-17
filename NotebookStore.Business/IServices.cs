namespace NotebookStore.Business;

public interface IServices
{
    IService<BrandDto> Brands { get; }
    IService<CpuDto> Cpus { get; }
    IService<DisplayDto> Displays { get; }
    IService<MemoryDto> Memories { get; }
    IService<ModelDto> Models { get; }
    IService<StorageDto> Storages { get; }
    IService<NotebookDto> Notebooks { get; }
    IUserService Users { get; }
    IRoleService Roles { get; }
    IPermissionService Permissions { get; }
}
