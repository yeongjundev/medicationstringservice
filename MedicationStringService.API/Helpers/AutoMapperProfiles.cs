using System;
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
                    opt => opt.MapFrom(x => BottleSizeEnumToString(x.BottleSize))
                );

            CreateMap<CountByBottleSize, CountByBottleSizeDTO>()
                .ForMember(
                    dest => dest.BottleSize,
                    opt => opt.MapFrom(x => BottleSizeEnumToString(x.BottleSize))
                );

            CreateMap<CountByMedicationId, CountByMedicationIdDTO>();

            CreateMap<StatisticsResult, StatisticsDTO>();
        }

        public static string BottleSizeEnumToString(BottleSizeEnum bottleSize)
        {
            switch ((int)bottleSize)
            {
                case 0:
                    return "NA";
                case 1:
                    return "S";
                case 2:
                    return "M";
                case 3:
                    return "L";
                case 4:
                    return "XL";
                case 5:
                    return "XXL";
                default:
                    throw new ArgumentException();
            }
        }
    }
}