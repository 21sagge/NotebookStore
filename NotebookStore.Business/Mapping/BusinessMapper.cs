using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NotebookStore.Entities;

namespace NotebookStore.Business.Mapping
{
	internal class BusinessMapper : Profile
	{
		public BusinessMapper()
		{
            CreateMap<Brand, BrandDto>()
            .ReverseMap();

            CreateMap<Cpu, CpuDto>()
            .ReverseMap();

            CreateMap<Display, DisplayDto>()
            .ReverseMap();

            CreateMap<Memory, MemoryDto>()
            .ReverseMap();

            CreateMap<Model, ModelDto>()
            .ReverseMap();

            CreateMap<Storage, StorageDto>()
            .ReverseMap();

            CreateMap<Notebook, NotebookDto>()
            .ReverseMap();

            CreateMap<IdentityUser, UserDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Password, act => act.MapFrom(src => src.PasswordHash))
            .ReverseMap();
        }
	}
}
