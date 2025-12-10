using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo
            {
                Title = "Alza Products API",
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated ? "This API version has been deprecated." : string.Empty
            };

            options.SwaggerDoc(description.GroupName, info);
        }

        //options.DocInclusionPredicate((docName, apiDesc) =>
        //{
        //    if (apiDesc.GroupName is null)
        //    {
        //        return false;
        //    }

        //    return string.Equals(apiDesc.GroupName, docName, StringComparison.OrdinalIgnoreCase);
        //});
    }
}