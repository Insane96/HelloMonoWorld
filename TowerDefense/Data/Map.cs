using System;
using System.Collections.Generic;
using Engine.ExtensionMethods;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerDefense.Data.Json;

namespace TowerDefense.Data;

[JsonConverter(typeof(Converter))]
public class Map(List<Vector2> turningPoints, Vector2 startPosition, Vector2 towerPosition)
{
    public List<Vector2> TurningPoints { get; set; } = turningPoints;
    public Vector2 StartPosition { get; set; } = startPosition;
    public Vector2 TowerPosition { get; set; } = towerPosition;

    public class Converter : JsonConverter<Map>
    {
        public override Map? ReadJson(JsonReader reader, Type objectType, Map? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            serializer.Converters.Add(new Vector2JsonConverter());
            
            JToken token = JToken.Load(reader);
            if (token.Type != JTokenType.Object)
                throw new JsonSerializationException($"Expected object, got {token.Type}");

            List<Vector2> turningPoints = [];
            JArray jTurningPoints = token.GetPropertyOrThrow<JArray>("turning_points");
            foreach (JToken turningPoint in jTurningPoints)
            {
                turningPoints.Add(turningPoint.ToObject<Vector2>(serializer));
            }
            
            JObject jStartPosition = token.GetPropertyOrThrow<JObject>("start_position");
            Vector2 startPosition = jStartPosition.ToObject<Vector2>(serializer);
            
            JObject jTowerPosition = token.GetPropertyOrThrow<JObject>("tower_position");
            Vector2 towerPosition = jTowerPosition.ToObject<Vector2>(serializer);
            
            return new Map(turningPoints, startPosition, towerPosition);
        }
        
        public override void WriteJson(JsonWriter writer, Map? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
