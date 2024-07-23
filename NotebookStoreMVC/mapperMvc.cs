using AutoMapper;
using NotebookStore.Business;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC;

public class MapperMvc : Profile
{
    public MapperMvc()
    {
        CreateMap<BrandDto, BrandViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<CpuDto, CpuViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<DisplayDto, DisplayViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<MemoryDto, MemoryViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<ModelDto, ModelViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<StorageDto, StorageViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<NotebookDto, NotebookViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<UserDto, UserViewModel>()
            .ReverseMap();
    }
}