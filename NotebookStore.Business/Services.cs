using AutoMapper;
using NotebookStore.DAL;

namespace NotebookStore.Business;

public class Services: IServices
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Services(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public IService<BrandDto> Brands => new BrandService(_unitOfWork, _mapper);
	public IService<CpuDto> Cpus => new CpuService(_unitOfWork, _mapper);
	public IService<DisplayDto> Displays => new DisplayService(_unitOfWork, _mapper);
	public IService<MemoryDto> Memories => new MemoryService(_unitOfWork, _mapper);
	public IService<ModelDto> Models => new ModelService(_unitOfWork, _mapper);
	public IService<StorageDto> Storages => new StorageService(_unitOfWork, _mapper);
	public IService<NotebookDto> Notebooks => new NotebookService(_unitOfWork, _mapper);
	// public IService<UserDto> Users => new UserService(_unitOfWork, _mapper);
}
