using MyClinicWebAPI.Dto;
using MyClinicWebAPI.Models;
using System.Runtime.CompilerServices;

/**
 * Class mapper to creating mapping without AutoMapper. This will be a static Class because we are creating extension methods
 * Example of call: PrescriptionModel m; m.ToPrescriptionDto();
 * */


namespace MyClinicWebAPI.Mappers
{
    public static class PrescriptionMappers
    {

        public static PrescriptionDto ToPrescriptionDto(this PrescriptionModel _prescriptionModel)
        {
            return new PrescriptionDto
            {
                IDPrescription = _prescriptionModel.IDPrescription,
                Description = _prescriptionModel.Description
            };
        }
    }
}
