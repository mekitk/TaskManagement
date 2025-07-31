using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GorevYonetimUygulamasi.Middlewares
{

    //Swagger UI üzerinden POST isteklerinde Id alanı görünmez,
    //Id gönderilmese bile 400 BadRequest hatası almazsın,
    //JWT Authorize butonu çalışır.

    public class IgnoreIdInSwaggerSchema : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties.ContainsKey("id"))
            {
                schema.Properties.Remove("id");
            }
        }
    }
}
