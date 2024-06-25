namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class BrandService : IService<BrandDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IPermissionService permissionService;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPermissionService permissionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userService = userService;
        this.permissionService = permissionService;
    }

    public async Task<IEnumerable<BrandDto>> GetAll()
    {
        var brands = await unitOfWork.Brands.Read();
        var currentUser = await userService.GetCurrentUser();

        return brands.Select(brand =>
        {
            var brandDto = mapper.Map<BrandDto>(brand);

            var canUpdateBrand = permissionService.CanUpdateBrand(brand, currentUser);

            brandDto.CanUpdate = canUpdateBrand;
            brandDto.CanDelete = canUpdateBrand;

            return brandDto;
        });
    }

    public async Task<BrandDto?> Find(int id)
    {
        var brand = await unitOfWork.Brands.Find(id);

        if (brand == null)
        {
            return null;
        }

        var currentUser = await userService.GetCurrentUser();

        bool canUpdateBrand = permissionService.CanUpdateBrand(brand, currentUser);

        var brandDto = mapper.Map<BrandDto>(brand);

        brandDto.CanUpdate = canUpdateBrand;
        brandDto.CanDelete = canUpdateBrand;

        return brandDto;
    }

    public async Task<bool> Create(BrandDto brandDto)
    {
        var brand = mapper.Map<Brand>(brandDto);

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        brand.CreatedBy = currentUser.Id;
        brand.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        try
        {
            await unitOfWork.Brands.Create(brand);
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

    public async Task<bool> Update(BrandDto brandDto)
    {
        var brand = await unitOfWork.Brands.Find(brandDto.Id);

        if (brand == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        var canUpdateBrand = permissionService.CanUpdateBrand(brand, currentUser);

        if (!canUpdateBrand)
        {
            return false;
        }

        brandDto.CanUpdate = canUpdateBrand;
        brandDto.CanDelete = canUpdateBrand;

        try
        {
            await unitOfWork.Brands.Update(mapper.Map(brandDto, brand));
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
        var brand = await unitOfWork.Brands.Find(id);

        if (brand == null)
        {
            return false;
        }

        unitOfWork.BeginTransaction();

        var currentUser = await userService.GetCurrentUser();

        if (!permissionService.CanUpdateBrand(brand, currentUser))
        {
            return false;
        }

        try
        {
            await unitOfWork.Brands.Delete(id);
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
}
