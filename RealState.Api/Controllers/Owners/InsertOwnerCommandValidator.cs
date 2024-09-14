// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-13-2024
// ***********************************************************************
// <copyright file="InsertOwnerCommandValidator.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
using RealState.Application.Owners.Command;

namespace RealState.Api.Controllers.Owners
{
    /// <summary>
    /// Class InsertOwnerCommandValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{RealState.Application.Owners.Command.InsertOwnerCommand}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{RealState.Application.Owners.Command.InsertOwnerCommand}" />
    public class InsertOwnerCommandValidator : AbstractValidator<InsertOwnerCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertOwnerCommandValidator"/> class.
        /// </summary>
        public InsertOwnerCommandValidator()
        {
            RuleFor(o => o.Birthday)
                .NotEmpty()
                .Must(BeAValidDate);

            RuleFor(o => o.Name)
            .NotEmpty();
        }

        /// <summary>
        /// Bes a valid date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }

    }
}
