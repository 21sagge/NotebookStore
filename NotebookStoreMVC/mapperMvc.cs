using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NotebookStore.Business;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC;

public class MapperMvc : Profile
{
    public MapperMvc()
    {
        CreateMap<Brand, BrandDto>()
            .ReverseMap();
        CreateMap<BrandDto, BrandViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Cpu, CpuDto>()
            .ReverseMap();
        CreateMap<CpuDto, CpuViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Display, DisplayDto>()
            .ReverseMap();
        CreateMap<DisplayDto, DisplayViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Memory, MemoryDto>()
            .ReverseMap();
        CreateMap<MemoryDto, MemoryViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Model, ModelDto>()
            .ReverseMap();
        CreateMap<ModelDto, ModelViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Storage, StorageDto>()
            .ReverseMap();
        CreateMap<StorageDto, StorageViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<Notebook, NotebookDto>()
            .ReverseMap();
        CreateMap<NotebookDto, NotebookViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete))
            .ReverseMap();

        CreateMap<IdentityUser, UserDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.PasswordHash))
            .ReverseMap();
        CreateMap<UserDto, UserViewModel>()
            .ReverseMap();
    }
}