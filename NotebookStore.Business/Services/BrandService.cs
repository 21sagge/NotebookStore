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
		var brandDtos = mapper.Map<IEnumerable<BrandDto>>(brands);
		var currentUser = await userService.GetCurrentUser();

		foreach (var brandDto in brandDtos)
		{
			var brand = await unitOfWork.Brands.Find(brandDto.Id);

			if (brand == null)
			{
				continue;
			}

			var createdBy = brand.CreatedBy;

			brandDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
			brandDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
		}

		return brandDtos;
	}

	public async Task<BrandDto?> Find(int id)
	{
		var brand = await unitOfWork.Brands.Find(id);

		if (brand == null)
		{
			return null;
		}

		var brandDto = mapper.Map<BrandDto>(brand);

		var currentUser = await userService.GetCurrentUser();

		if (brand.CreatedBy == currentUser.Id
			|| currentUser.Role == "Admin"
			|| brand.CreatedBy == null)
		{
			brandDto.CanUpdate = true;
			brandDto.CanDelete = true;
		}

		return brandDto;
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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			if (brand.CreatedBy != currentUser.Id
				&& currentUser.Role != "admin"
				&& brand.CreatedBy != null)
			{
				return false;
			}

			brand.Name = brandDto.Name;

			await unitOfWork.Brands.Update(brand);
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
			var brand = await unitOfWork.Brands.Find(id);
			var currentUser = await userService.GetCurrentUser();

			if (brand?.CreatedBy != currentUser.Id
				&& currentUser.Role != "admin"
				&& brand?.CreatedBy != null)
			{
				return false;
			}

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
