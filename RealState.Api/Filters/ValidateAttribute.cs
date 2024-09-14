// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="ValidateAttribute.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace RealState.Api.Filters
{
    /// <summary>
    /// Class ValidateAttribute. This class cannot be inherited.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ValidateAttribute : Attribute
    {
    }
}
