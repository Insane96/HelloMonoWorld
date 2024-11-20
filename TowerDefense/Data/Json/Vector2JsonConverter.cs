using System;
using Engine.ExtensionMethods;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TowerDefense.Data.Json;

public class Vector2JsonConverter : JsonConverter<Vector2>
{
    public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);
        if (token.Type != JTokenType.Object)
            throw new JsonSerializationException($"Expected object, got {token.Type}");

        return new Vector2(
            (token["x"] ?? throw new JsonSerializationException("Expected x in object")).Value<float>(), 
            (token["y"] ?? throw new JsonSerializationException("Expected y in object")).Value<float>()
        );
    }
    
    public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WriteProperty("x", value.X);
        writer.WriteProperty("y", value.Y);
        writer.WriteEndObject();
    }
}