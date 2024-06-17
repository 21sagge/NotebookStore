namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class BrandService : BaseService, IService<BrandDto>
{
	private readonly IUnitOfWork unitOfWork;
	// private readonly IMapper mapper;
	// private readonly IUserService userService;

	public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
	: base(mapper, userService)
	{
		this.unitOfWork = unitOfWork;
		// this.mapper = mapper;
		// this.userService = userService;
	}

	public async Task<IEnumerable<BrandDto>> GetAll()
	{
		var brands = await unitOfWork.Brands.Read();
		var currentUser = await userService.GetCurrentUser();

		IEnumerable<BrandDto> result = brands.Select(brand =>
			AssignPermission<Brand, BrandDto>(brand, currentUser)
		);

		return result;
	}

	public async Task<BrandDto?> Find(int id)
	{
		var brand = await unitOfWork.Brands.Find(id);

		if (brand == null)
		{
			return null;
		}

		var currentUser = await userService.GetCurrentUser();

		return AssignPermission<Brand, BrandDto>(brand, currentUser);
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

			var result = AssignPermission<Brand, BrandDto>(brand, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
			}

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

		try
		{
			var currentUser = await userService.GetCurrentUser();

			var result = AssignPermission<Brand, BrandDto>(brand, currentUser);

			if (!result.CanUpdate || !result.CanDelete)
			{
				throw new Exception("Permission denied");
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
