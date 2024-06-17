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
        CreateMap<BrandDto, BrandViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Brand, BrandDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<CpuDto, CpuViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ReverseMap();
        CreateMap<Cpu, CpuDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ReverseMap();
        CreateMap<DisplayDto, DisplayViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
            .ForMember(dest => dest.ResolutionWidth, act => act.MapFrom(src => src.ResolutionWidth))
            .ForMember(dest => dest.ResolutionHeight, act => act.MapFrom(src => src.ResolutionHeight))
            .ReverseMap();
        CreateMap<Display, DisplayDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
            .ForMember(dest => dest.ResolutionWidth, act => act.MapFrom(src => src.ResolutionWidth))
            .ForMember(dest => dest.ResolutionHeight, act => act.MapFrom(src => src.ResolutionHeight))
            .ReverseMap();
        CreateMap<MemoryDto, MemoryViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Speed, act => act.MapFrom(src => src.Speed))
            .ReverseMap();
        CreateMap<Memory, MemoryDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Speed, act => act.MapFrom(src => src.Speed))
            .ReverseMap();
        CreateMap<ModelDto, ModelViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Model, ModelDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Storage, StorageDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
            //.ForMember(dest => dest.) TODO
            .ReverseMap();
        CreateMap<StorageDto, StorageViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
            .ReverseMap();
        CreateMap<NotebookDto, NotebookViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Cpu, act => act.MapFrom(src => src.Cpu))
            .ForMember(dest => dest.Display, act => act.MapFrom(src => src.Display))
            .ForMember(dest => dest.Memory, act => act.MapFrom(src => src.Memory))
            .ForMember(dest => dest.Storage, act => act.MapFrom(src => src.Storage))
            .ReverseMap();
        CreateMap<Notebook, NotebookDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Cpu, act => act.MapFrom(src => src.Cpu))
            .ForMember(dest => dest.Display, act => act.MapFrom(src => src.Display))
            .ForMember(dest => dest.Memory, act => act.MapFrom(src => src.Memory))
            .ForMember(dest => dest.Storage, act => act.MapFrom(src => src.Storage))
            .ReverseMap();
        CreateMap<UserDto, UserViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
            .ReverseMap();
        CreateMap<IdentityUser, UserDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.PasswordHash))
            .ReverseMap();
    }
}