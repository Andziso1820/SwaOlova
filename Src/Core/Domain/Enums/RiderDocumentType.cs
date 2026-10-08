using System.ComponentModel.DataAnnotations;

namespace SwaOlova.Domain.Enums
{
    public enum RiderDocumentType
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Driver's License")]
        DriversLicense = 1,

        [Display(Name = "Identity Document")]
        IdentityDocument = 2,

        [Display(Name = "Vehicle Registration")]
        VehicleRegistration = 3,

        [Display(Name = "Vehicle Insurance")]
        VehicleInsurance = 4,

        [Display(Name = "Roadworthy Certificate")]
        RoadworthyCertificate = 5,

        [Display(Name = "Police Clearance")]
        PoliceClearance = 6,

        [Display(Name = "Proof of Address")]
        ProofOfAddress = 7,

        [Display(Name = "Other")]
        Other = 8
    }
}
