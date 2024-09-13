using System.ComponentModel.DataAnnotations;

namespace RealState.Api.Filters
{
    public class ValidateFileAttribute(int maxFileSize, string[] allowedExtensions) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return new ValidationResult("No file uploaded.");
            }

            if (file.Length == 0)
            {
                return new ValidationResult("File is empty.");
            }

            if (file.Length > maxFileSize)
            {
                return new ValidationResult($"File size should not exceed {maxFileSize} bytes.");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return !allowedExtensions.Contains(extension) ? new ValidationResult($"File extension is not allowed. Allowed extensions are: {string.Join(", ", allowedExtensions)}") : ValidationResult.Success;
        }
    }
}
