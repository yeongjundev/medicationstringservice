using AutoMapper;
using MedicationStringService.API.DTOs;
using MedicationStringService.API.Models;

namespace MedicationStringService.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MedicationString, MedicationStringDTO>()
                .ForMember(
                    dest => dest.BottleSize,
                    opt => opt.MapFrom<AutoMapperBottleSizeResolver>()
                );
        }

    }
}