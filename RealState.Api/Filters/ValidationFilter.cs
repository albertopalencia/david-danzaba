// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="ValidationFilter.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
using System.Net;
using System.Reflection;

namespace RealState.Api.Filters;

/// <summary>
/// Class ValidationFilter.
/// </summary>
public static class ValidationFilter
{
    /// <summary>
    /// Validations the filter factory.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="next">The next.</param>
    /// <returns>EndpointFilterDelegate.</returns>
    public static EndpointFilterDelegate ValidationFilterFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        IEnumerable<ValidationDescriptor> validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices);

        if (validationDescriptors.Any())
        {
            return invocationContext => ValidateAsync(validationDescriptors, invocationContext, next);
        }

        return invocationContext => next(invocationContext);
    }

    /// <summary>
    /// Validate as an asynchronous operation.
    /// </summary>
    /// <param name="validationDescriptors">The validation descriptors.</param>
    /// <param name="invocationContext">The invocation context.</param>
    /// <param name="next">The next.</param>
    /// <returns>A Task&lt;System.Object&gt; representing the asynchronous operation.</returns>
    private static async ValueTask<object?> ValidateAsync(IEnumerable<ValidationDescriptor> validationDescriptors, EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        foreach (ValidationDescriptor descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];

            if (argument is not null)
            {
                var validationResult = await descriptor.Validator.ValidateAsync(
                    new ValidationContext<object>(argument)
                );

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            }
        }

        return await next.Invoke(invocationContext);
    }

    /// <summary>
    /// Gets the validators.
    /// </summary>
    /// <param name="methodInfo">The method information.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>IEnumerable&lt;ValidationDescriptor&gt;.</returns>
    static IEnumerable<ValidationDescriptor> GetValidators(MethodBase methodInfo, IServiceProvider serviceProvider)
    {
        ParameterInfo[] parameters = methodInfo.GetParameters();

        for (int index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];

            if (parameter.GetCustomAttribute<ValidateAttribute>() is not null)
            {
                Type validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);

                IValidator? validator = serviceProvider.GetService(validatorType) as IValidator;

                if (validator is not null)
                {
                    yield return new ValidationDescriptor { ArgumentIndex = index, ArgumentType = parameter.ParameterType, Validator = validator };
                }
            }
        }
    }

}
