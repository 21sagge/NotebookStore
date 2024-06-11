namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class BrandService : IService<BrandDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
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
            await unitOfWork.Brands.Update(brand);
            await unitOfWork.SaveAsync();
            unitOfWork.CommitTransaction();
            return true;
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
