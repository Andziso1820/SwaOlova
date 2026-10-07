using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Portal.Areas.Merchants.Models;

public class UploadViewModel
{
    public Guid MerchantId { get; set; }

    [Required(ErrorMessage = "A document file is required.")]
    public IFormFile? DocumentFile { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DocumentExpiryDate { get; set; }

    [Required(ErrorMessage = "Document type is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid document type.")]
    public MerchantDocumentType DocumentType { get; set; }
}
