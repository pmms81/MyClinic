using AutoMapper;
using MyClinicWebAPI.Dto;
using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ClientModel, ClientDto>();
            CreateMap<PrescriptionModel, PrescriptionDto>();
            CreateMap<PrescriptionDto, PrescriptionModel>();
        }
    }
}
