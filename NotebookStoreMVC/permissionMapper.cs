using AutoMapper;
using NotebookStore.Business;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC;

public class PermissionMapper : Profile
{
	private readonly IUserService _userService;

	public PermissionMapper(IUserService userService)
	{
		_userService = userService;

		var currentUser = _userService.GetCurrentUser().Result;

		CreateMap<Brand, BrandDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<BrandDto, BrandViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();

		CreateMap<Cpu, CpuDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<CpuDto, CpuViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();

		CreateMap<Display, DisplayDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<DisplayDto, DisplayViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();

		CreateMap<Memory, MemoryDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<MemoryDto, MemoryViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();

		CreateMap<Model, ModelDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<ModelDto, ModelViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();

		CreateMap<Storage, StorageDto>()
			.ForMember(dest => dest.CanUpdate, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ForMember(dest => dest.CanDelete, act => act.MapFrom(src => src.CreatedBy == currentUser.Id || currentUser.Role == "Admin" || src.CreatedBy == null))
			.ReverseMap();

		CreateMap<StorageDto, StorageViewModel>()
			.ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
			.ReverseMap();
	}
}
