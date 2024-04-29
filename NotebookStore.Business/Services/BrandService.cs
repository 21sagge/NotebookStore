namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class BrandService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<BrandDto>> GetBrands()
    {
        var brands = await unitOfWork.Brands.Read();

        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> GetBrand(int id)
    {
        var brand = await unitOfWork.Brands.Find(id);

        return mapper.Map<BrandDto>(brand);
    }

    public async Task<bool> CreateBrand(BrandDto brandDto)
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

    public async Task<bool> UpdateBrand(BrandDto brandDto)
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

    public async Task<bool> DeleteBrand(int id)
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

    public async Task<bool> BrandExists(int id)
    {
        return await unitOfWork.Brands.Find(id) != null;
    }
}
