// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-13-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="ValidateFileAttribute.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace RealState.Api.Filters
{
    /// <summary>
    /// Class ValidateFileAttribute.
    /// Implements the <see cref="ValidationAttribute" />
    /// </summary>
    /// <seealso cref="ValidationAttribute" />
    public class ValidateFileAttribute(int maxFileSize, string[] allowedExtensions) : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>System.Nullable&lt;ValidationResult&gt;.</returns>
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
