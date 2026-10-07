using System.ComponentModel.DataAnnotations;

namespace SwaOlova.Domain.Enums
{
    public enum MerchantDocumentType
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Tax Clearance Certificate")]
        TaxClearanceCertificate = 1,

        [Display(Name = "Business Registration")]
        BusinessRegistration = 2,

        [Display(Name = "Business License")]
        BusinessLicense = 3,

        [Display(Name = "Health Certificate")]
        HealthCertificate = 4,

        [Display(Name = "Compliance Certificate")]
        ComplianceCertificate = 5,

        [Display(Name = "Insurance Certificate")]
        InsuranceCertificate = 6,

        [Display(Name = "Permit License")]
        PermitLicense = 7,

        [Display(Name = "Other")]
        Other = 8
    }
}
