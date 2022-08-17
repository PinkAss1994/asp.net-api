using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KarmaStore
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileUploadMime = "multipart/form-data";
            if (operation.RequestBody == null ||
                !operation.RequestBody.Content.Any(x =>
                x.Key.Equals(fileUploadMime, StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }
            var name = context.ApiDescription.ActionDescriptor.DisplayName;
            operation.Parameters.Clear();
            if (context.ApiDescription.ParameterDescriptions[0].Type != typeof(IFormFile))
            {
                var uploadFileMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                {
                    ["files"] = new OpenApiSchema()
                    {
                        Type = "array",
                        Items = new OpenApiSchema()
                        {
                            Type = "string",
                            Format = "binary"
                        }
                    }
                },
                        Required = new HashSet<string>() { "files" }
                    }
                };

                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = { ["multipart/form-data"] = uploadFileMediaType }
                };
            }
        }
    }
}
