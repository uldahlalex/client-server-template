using NSwag.Generation;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

public static class SwaggerExtensions
{
    public static void AddTypeToSwagger<T>(this OpenApiDocumentGeneratorSettings settings)
    {
        settings.DocumentProcessors.Add(new TypeMapDocumentProcessor<T>());
    }
}

public class TypeMapDocumentProcessor<T> : IDocumentProcessor
{
    public void Process(DocumentProcessorContext context)
    {
        var schema = context.SchemaGenerator.Generate(typeof(T));
        context.Document.Definitions[typeof(T).Name] = schema;
    }
}

public class MakeAllPropertiesRequiredProcessor : IDocumentProcessor
{
    public void Process(DocumentProcessorContext context)
    {
        foreach (var schema in context.Document.Definitions.Values)
        foreach (var property in schema.Properties)
            schema.RequiredProperties.Add(property.Key);
    }
}