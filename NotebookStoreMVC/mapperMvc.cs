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
        CreateMap<Brand, BrandDto>();
        CreateMap<BrandDto, BrandViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Cpu, CpuDto>();
        CreateMap<CpuDto, CpuViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Display, DisplayDto>();
        CreateMap<DisplayDto, DisplayViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Memory, MemoryDto>();
        CreateMap<MemoryDto, MemoryViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Model, ModelDto>();
        CreateMap<ModelDto, ModelViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Storage, StorageDto>();
        CreateMap<StorageDto, StorageViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<Notebook, NotebookDto>();
        CreateMap<NotebookDto, NotebookViewModel>()
            .ForMember(dest => dest.CanUpdateAndDelete, act => act.MapFrom(src => src.CanUpdate && src.CanDelete));

        CreateMap<IdentityUser, UserDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.PasswordHash));
        CreateMap<UserDto, UserViewModel>();
    }
}