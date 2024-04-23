using AutoMapper;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Services;

internal class MapperMvc : Profile
{
    public MapperMvc()
    {
        CreateMap<NotebookStore.Business.BrandDto, BrandViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Brand, NotebookStore.Business.BrandDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<NotebookStore.Business.CpuDto, CpuViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ReverseMap();
        CreateMap<Cpu, NotebookStore.Business.CpuDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ReverseMap();
        CreateMap<NotebookStore.Business.DisplayDto, DisplayViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
            .ForMember(dest => dest.ResolutionWidth, act => act.MapFrom(src => src.ResolutionWidth))
            .ForMember(dest => dest.ResolutionHeight, act => act.MapFrom(src => src.ResolutionHeight))
            .ReverseMap();
        CreateMap<Display, NotebookStore.Business.DisplayDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
            .ForMember(dest => dest.ResolutionWidth, act => act.MapFrom(src => src.ResolutionWidth))
            .ForMember(dest => dest.ResolutionHeight, act => act.MapFrom(src => src.ResolutionHeight))
            .ReverseMap();
        CreateMap<NotebookStore.Business.MemoryDto, MemoryViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Speed, act => act.MapFrom(src => src.Speed))
            .ReverseMap();
        CreateMap<Memory, NotebookStore.Business.MemoryDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Speed, act => act.MapFrom(src => src.Speed))
            .ReverseMap();
        CreateMap<NotebookStore.Business.ModelDto, ModelViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Model, NotebookStore.Business.ModelDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Storage, NotebookStore.Business.StorageDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
            .ReverseMap();
        CreateMap<NotebookStore.Business.StorageDto, StorageViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
            .ReverseMap();
        CreateMap<NotebookStore.Business.NotebookDto, NotebookViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Cpu, act => act.MapFrom(src => src.Cpu))
            .ForMember(dest => dest.Display, act => act.MapFrom(src => src.Display))
            .ForMember(dest => dest.Memory, act => act.MapFrom(src => src.Memory))
            .ForMember(dest => dest.Storage, act => act.MapFrom(src => src.Storage))
            .ReverseMap();
        CreateMap<Notebook, NotebookStore.Business.NotebookDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Cpu, act => act.MapFrom(src => src.Cpu))
            .ForMember(dest => dest.Display, act => act.MapFrom(src => src.Display))
            .ForMember(dest => dest.Memory, act => act.MapFrom(src => src.Memory))
            .ForMember(dest => dest.Storage, act => act.MapFrom(src => src.Storage))
            .ReverseMap();
        CreateMap<NotebookStore.Business.UserDto, UserViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
            .ReverseMap();
        CreateMap<User, NotebookStore.Business.UserDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
            .ReverseMap();
        CreateMap<Brand, NotebookStore.Business.BrandDto>();
        CreateMap<Cpu, CpuDto>();
        CreateMap<Display, DisplayDto>();
        CreateMap<Memory, MemoryDto>();
        CreateMap<Model, ModelDto>();
        CreateMap<Storage, StorageDto>();
        CreateMap<Notebook, NotebookDto>();
    }
}