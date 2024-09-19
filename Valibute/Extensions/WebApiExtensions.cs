using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using Valibute.Attributes.WebApi;
using Valibute.Models.Responses;
using Valibute.Models.WebApi;
using Valibute.Utils;

namespace Valibute.Extensions
{
    public static class WebApiExtensions
    {
        public static IApplicationBuilder UseValibute(this IApplicationBuilder app)
        {
            return app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint == null)
                {
                    await next(context);
                    return;
                }

                var valibuteProperty = endpoint.Metadata.GetMetadata<ValibuteMiddlewareProperty>();
                var valibuteAttribute = endpoint.Metadata.GetMetadata<VEndpoint>();

                if (valibuteProperty == null && valibuteAttribute == null)
                {
                    await next(context);
                    return;
                }

                if (valibuteProperty == null)
                {
                    valibuteProperty = new ValibuteMiddlewareProperty(true, valibuteProperty!.ValibuteType, valibuteProperty.ErrorMessage);
                }


                string jsonBody = string.Empty;
                using (var streamReader = new StreamReader(context.Request.Body))
                {
                    jsonBody = await streamReader.ReadToEndAsync();
                }


                var valibuteClass = JsonConvert.DeserializeObject(jsonBody, valibuteProperty.ValibuteType);
                if (valibuteClass == null)
                {
                    await next(context);
                    return;
                }

                ValidationResponse validationResponse = ValibuteUtils.Validate(valibuteClass, valibuteProperty.ValibuteType);

                if (!validationResponse.IsValid)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    string result = JsonConvert.SerializeObject(
                        new
                        {
                            message = string.IsNullOrEmpty(valibuteProperty.ErrorMessage) ? "Validation error" : valibuteProperty.ErrorMessage,
                            errors = ((ErrorValidationResponse)validationResponse).Errors.Select(x => new { 
                                propertyName = x.PropName,
                                errorMessage = x.Error
                            })
                        });
                    await context.Response.WriteAsync(result);
                }
                await next(context);
            });
        }

        public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder)
            where T : class
        {
            Type type = typeof(T);
            builder.Add(endpointBuilder =>
            {
                endpointBuilder.Metadata.Add(new ValibuteMiddlewareProperty(true, type));
            });

            builder.Produces(422);
            return builder;
        }

        public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder, string errorMessage)
    where T : class
        {
            Type type = typeof(T);
            builder.Add(endpointBuilder =>
            {
                endpointBuilder.Metadata.Add(new ValibuteMiddlewareProperty(true, type, errorMessage));
            });

            builder.Produces(422);
            return builder;
        }
    }
}
