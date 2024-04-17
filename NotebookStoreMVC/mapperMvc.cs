using AutoMapper;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Services;

internal class MapperMvc : Profile
{
    public MapperMvc()
    {
        CreateMap<Brand, BrandViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Cpu, CpuViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ReverseMap();
        CreateMap<Display, DisplayViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
            .ForMember(dest => dest.ResolutionWidth, act => act.MapFrom(src => src.ResolutionWidth))
            .ForMember(dest => dest.ResolutionHeight, act => act.MapFrom(src => src.ResolutionHeight))
            .ReverseMap();
        CreateMap<Memory, MemoryViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Speed, act => act.MapFrom(src => src.Speed))
            .ReverseMap();
        CreateMap<Model, ModelViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Storage, StorageViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Capacity, act => act.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type))
            .ReverseMap();
        CreateMap<Notebook, NotebookViewModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, act => act.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Cpu, act => act.MapFrom(src => src.Cpu))
            .ForMember(dest => dest.Display, act => act.MapFrom(src => src.Display))
            .ForMember(dest => dest.Memory, act => act.MapFrom(src => src.Memory))
            .ForMember(dest => dest.Storage, act => act.MapFrom(src => src.Storage))
            .ReverseMap();
        CreateMap<Brand, BrandDto>();
        CreateMap<Cpu, CpuDto>();
        CreateMap<Display, DisplayDto>();
        CreateMap<Memory, MemoryDto>();
        CreateMap<Model, ModelDto>();
        CreateMap<Storage, StorageDto>();
        CreateMap<Notebook, NotebookDto>();
    }
}