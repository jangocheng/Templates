namespace ApiTemplate.ViewModelSchemaFilters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Shows an example of a <see cref="ValidationProblemDetails"/> containing errors.
    /// </summary>
    /// <seealso cref="ISchemaFilter" />
    public class ValidationProblemDetailsSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="context">The context.</param>
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (context.SystemType == typeof(ValidationProblemDetails))
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("Property1", "Error message 1");
                modelState.AddModelError("Property1", "Error message 2");

                var problemDetails = new ValidationProblemDetails(modelState)
                {
                    Type = "https://asp.net/core",
                    Title = $"2 validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details.",
                    Instance = "/example"
                };

                model.Default = problemDetails;
                model.Example = problemDetails;
            }
        }
    }
}