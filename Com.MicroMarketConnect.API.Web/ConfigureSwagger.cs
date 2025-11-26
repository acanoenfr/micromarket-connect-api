using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Com.MicroMarketConnect.API.Web;

public class ConfigureSwagger(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>, IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.CustomSchemaIds(type => type.Name);

        provider
            .ApiVersionDescriptions
            .ToList()
            .ForEach(d => options.SwaggerDoc(d.GroupName, new OpenApiInfo
            {
                Title = $"MicroMarket Connect API {d.GroupName.ToUpperInvariant()}",
                Version = d.ApiVersion.ToString()
            }));

        options
            .AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "JWT Authorization",
                Description = "Enter your JWT in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

        options
            .AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document),
                        []
                    }
                });
    }

    public void Configure(SwaggerUIOptions options)
    {
        provider
            .ApiVersionDescriptions
            .ToList()
            .ForEach(d => options.SwaggerEndpoint(
                $"/swagger/{d.GroupName}/swagger.json",
                $"MicroMarket Connect API {d.GroupName.ToUpperInvariant()}"));
    }
}
