using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace AspNetCore.Api
{
    public class AuthorizationHeaderOperationFilter : IOperationFilter
    {
        void IOperationFilter.Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters != null)
            {
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "access token",
                    Required = false,
                    Type = "string"
                });
            }
        }
    }
}
