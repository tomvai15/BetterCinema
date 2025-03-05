using System.Reflection;
using System.Text;
using BetterCinema.Tools.App;
using CommandLine;

Parser.Default.ParseArguments<Options>(args).WithParsed(Run);

return;

static void Run(Options options)
{
    ArgumentNullException.ThrowIfNull(options);

    var assembly = Assembly.LoadFrom(options.Input);

    var contracts = assembly.GetTypes()
        .Where(t => t.IsClass)
        .Where(t => t.Namespace != null && t.Namespace.StartsWith(options.Namespace))
        .Select(t => new
        {
            Name = t.Name,
            Folders = t.Namespace!.Substring(options.Namespace.Length).Split('.', StringSplitOptions.RemoveEmptyEntries)
                .ToList(),
            Properties = t.GetProperties()
        });


    foreach (var contract in contracts)
    {
        var includeImports = false;
        const char separator = '\\';
        var folders = string.Join(separator, contract.Folders);
        var filePath = $"{options.Output}{separator}{folders}{separator}{contract.Name}.ts";

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        using var file = File.Create(filePath);

        var stringBuilder = new StringBuilder();
        var imports = new List<string>();

        stringBuilder.AppendLine($"export type {contract.Name} = {{");
        foreach (var property in contract.Properties)
        {
            var (typeName, isNullable, import) = GetTypeScriptType(property.PropertyType, options);
            if (import != null)
            {
                var stepBacks = Enumerable.Range(0, contract.Folders.Count).Select(x => "..");
                imports.Add(
                    $"import {{ {typeName} }} from '{string.Join("/", stepBacks)}/{import.Replace('.', '/')}';");
            }

            var propertyName = isNullable ? $"{property.Name}?" : property.Name;
            stringBuilder.AppendLine($"{propertyName}: {typeName}, ");
        }

        stringBuilder.AppendLine("}");

        if (includeImports)
        {
            var importsStringBuilder = new StringBuilder();
            foreach (var import in imports)
            {
                importsStringBuilder.AppendLine(import);
            }

            file.Write(Encoding.UTF8.GetBytes(importsStringBuilder.ToString()));
        }

        file.Write(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
    }
}

static (string propertyType, bool isNullable, string? import) GetTypeScriptType(Type type, Options options)
{
    if (type.IsGenericType)
    {
        if (type.GetGenericTypeDefinition() == typeof(IList<>))
        {
            var argumentType = type.GetGenericArguments()[0];
            var res = GetTypeScriptType(argumentType, options);
            return ($"{res.propertyType}[]", false, res.import);
        }

        if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var argumentType = type.GetGenericArguments()[0];
            var res = GetTypeScriptType(argumentType, options);
            return (res.propertyType, true, res.import);
        }


        throw new InvalidOperationException("The generic type does not implement IEnumerable<> or Nullable<>");
    }

    var name = type.Name switch
    {
        "String" => "string",
        "Boolean" => "boolean",
        "Int32" or "Int64" => "number",
        "DateTime" or "DateTimeOffset" => "Date",
        _ => null
    };

    if (name != null)
    {
        return (name, false, null);
    }

    return (type.Name, false, type.Namespace!.Substring(options.Namespace.Length));
}