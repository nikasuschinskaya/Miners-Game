﻿using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Miners;
using Miners.Shared.Objects.Prizes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Miners.Presentation.Converters
{
    public class GameObjectConverter : JsonConverter<IGameObject>
    {
        public override IGameObject ReadJson(JsonReader reader, Type objectType, IGameObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var jObject = JObject.Load(reader);

                if (jObject.TryGetValue("Type", StringComparison.OrdinalIgnoreCase, out var typeToken))
                {
                    string typeName = typeToken.Value<string>();
                    Console.WriteLine($"Десериализация объекта типа: {typeName}");

                    switch (typeName)
                    {
                        case "EmptyBlock":
                            return jObject.ToObject<EmptyBlock>();
                        case "MediumStableBlock":
                            return jObject.ToObject<MediumStableBlock>();
                        case "SteadyBlock":
                            return jObject.ToObject<SteadyBlock>();
                        case "WeakResistantBlock":
                            return jObject.ToObject<WeakResistantBlock>();
                        case "Miner":
                            return jObject.ToObject<Miner>();
                        case "Letup":
                            return jObject.ToObject<Letup>();
                        case "Powerup":
                            return jObject.ToObject<Powerup>();
                        case "Bomb":
                            return jObject.ToObject<Bomb>();
                        default:
                            throw new NotSupportedException($"Unsupported object type: {typeName}");
                    }
                }

                throw new JsonSerializationException("Type property not found in JSON");
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"Ошибка во время десериализации: {ex.Message}");
                Console.WriteLine($"Данные JSON: {reader.Value}");
                throw new JsonSerializationException("Ошибка десериализации IGameObject", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, IGameObject value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            serializer.Serialize(writer, value, typeof(IGameObject));
        }
    }
}
