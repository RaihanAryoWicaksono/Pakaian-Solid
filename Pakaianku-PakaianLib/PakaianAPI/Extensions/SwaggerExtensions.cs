using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PakaianApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pakaianku API",
                    Version = "v1",
                    Description = "API untuk sistem penjualan pakaian",
                    Contact = new OpenApiContact
                    {
                        Name = "Pakaianku Developer",
                        Email = "contact@pakaianku.com"
                    }
                });

                // Set the comments path for the Swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                // Define enum values as strings
                c.SchemaFilter<EnumSchemaFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pakaianku API v1");
                c.RoutePrefix = string.Empty; // Untuk mengakses Swagger UI di root URL
                c.DefaultModelsExpandDepth(-1); // Hide the Models section by default
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List); // Collapse all endpoints by default
            });

            return app;
        }
    }

    public class EnumSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                schema.Type = "string";
                schema.Format = null;

                foreach (var name in Enum.GetNames(context.Type))
                {
                    schema.Enum.Add(new OpenApiString(name));
                }
            }
        }
    }
}