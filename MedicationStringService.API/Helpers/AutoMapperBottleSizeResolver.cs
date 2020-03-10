using System;
using AutoMapper;
using MedicationStringService.API.DTOs;
using MedicationStringService.API.Models;

namespace MedicationStringService.API.Helpers
{
    public class AutoMapperBottleSizeResolver : IValueResolver<MedicationString, MedicationStringDTO, string>
    {
        public string Resolve(MedicationString src, MedicationStringDTO dest, string destMember, ResolutionContext context)
        {
            switch ((int)src.BottleSize)
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