using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NotebookStore.Business.Context;
using NotebookStore.DAL;

namespace NotebookStore.Business;

public class Services : IServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public Services(IUnitOfWork unitOfWork, IMapper mapper, IUserContext context, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
    }

    public IService<BrandDto> Brands => new BrandService(_unitOfWork, _mapper);
    public IService<CpuDto> Cpus => new CpuService(_unitOfWork, _mapper);
    public IService<DisplayDto> Displays => new DisplayService(_unitOfWork, _mapper);
    public IService<MemoryDto> Memories => new MemoryService(_unitOfWork, _mapper);
    public IService<ModelDto> Models => new ModelService(_unitOfWork, _mapper);
    public IService<StorageDto> Storages => new StorageService(_unitOfWork, _mapper);
    public IService<NotebookDto> Notebooks => new NotebookService(_unitOfWork, _mapper);
    public IUserService Users => new UserService(_mapper, _context, _userManager);
}
