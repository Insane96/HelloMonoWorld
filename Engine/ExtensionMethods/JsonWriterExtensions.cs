using Newtonsoft.Json;

namespace Engine.ExtensionMethods;

public static class JsonWriterExtensions
{
    public static void WriteProperty(this JsonWriter writer, string name, object value)
    {
        writer.WritePropertyName(name);
        writer.WriteValue(value);
    }
}