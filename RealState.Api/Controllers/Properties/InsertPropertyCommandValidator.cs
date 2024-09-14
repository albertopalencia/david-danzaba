// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertPropertyCommandValidator.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
using RealState.Application.Properties.Command;

namespace RealState.Api.Controllers.Properties
{
    /// <summary>
    /// Class InsertPropertyCommandValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{RealState.Application.Properties.Command.InsertPropertyCommand}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{RealState.Application.Properties.Command.InsertPropertyCommand}" />
    public class InsertPropertyCommandValidator : AbstractValidator<InsertPropertyCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertPropertyCommandValidator" /> class.
        /// </summary>
        public InsertPropertyCommandValidator()
        {
            RuleFor(c => c.IdOwner).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotNull();
            RuleFor(c => c.Price).GreaterThan(0);
            RuleFor(c => c.Year)
                .GreaterThanOrEqualTo(1900)
                .LessThanOrEqualTo(DateTime.Now.Year);
        }
    }
}
