// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="ValidationDescriptor.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;

namespace RealState.Api.Filters
{
    /// <summary>
    /// Class ValidationDescriptor.
    /// </summary>
    public class ValidationDescriptor
    {
        /// <summary>
        /// Gets the index of the argument.
        /// </summary>
        /// <value>The index of the argument.</value>
        public required int ArgumentIndex { get; init; }
        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>The type of the argument.</value>
        public required Type ArgumentType { get; init; }
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>The validator.</value>
        public required IValidator Validator { get; init; }
    }
}
