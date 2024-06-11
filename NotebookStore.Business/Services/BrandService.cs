namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class BrandService : IService<BrandDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userService = userService;
    }

    public async Task<IEnumerable<BrandDto>> GetAll()
    {
        var brands = await unitOfWork.Brands.Read();

        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> Find(int id)
    {
        var brand = await unitOfWork.Brands.Find(id);

        return mapper.Map<BrandDto>(brand);
    }

    public async Task<bool> Create(BrandDto brandDto)
    {
        var brand = mapper.Map<Brand>(brandDto);

        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();

            brand.CreatedBy = currentUser.Id;
            brand.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await unitOfWork.Brands.Create(brand);
            await unitOfWork.SaveAsync();

            unitOfWork.CommitTransaction();

            return true;
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception("Errore durante la creazione del brand", ex);
        }
    }

    public async Task<bool> Update(BrandDto brandDto)
    {
        var brand = mapper.Map<Brand>(brandDto);

        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();
            currentUser.Role = await userService.IsInRole(currentUser.Id, "admin") ? "admin" : "user";

            if (brand.CreatedBy != currentUser.Id && currentUser.Role != "admin" && brand.CreatedBy != null)
            {
                throw new UnauthorizedAccessException("Non sei autorizzato a modificare questo brand");
            }

            await unitOfWork.Brands.Update(brand);
            await unitOfWork.SaveAsync();
            unitOfWork.CommitTransaction();
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            unitOfWork.RollbackTransaction();
            return false;
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception("Errore durante l'aggiornamento del brand", ex);
        }
    }

    public async Task<bool> Delete(int id)
    {
        unitOfWork.BeginTransaction();

        try
        {
            var currentUser = await userService.GetCurrentUser();
            currentUser.Role = await userService.IsInRole(currentUser.Id, "admin") ? "admin" : "user";
            var brand = await unitOfWork.Brands.Find(id);

            if (brand?.CreatedBy != currentUser.Id && currentUser.Role != "admin" && brand?.CreatedBy != null)
            {
                return false;
            }

            await unitOfWork.Brands.Delete(id);
            await unitOfWork.SaveAsync();
            unitOfWork.CommitTransaction();
            return true;
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            throw new Exception("Errore durante l'eliminazione del brand", ex);
        }
    }
}
