using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Miners;
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

                // Поле "Type" в JSON указывает на конкретный тип объекта
                if (jObject.TryGetValue("Type", StringComparison.OrdinalIgnoreCase, out var typeToken))
                {
                    string typeName = typeToken.Value<string>();

                    // Здесь вам нужно предоставить логику создания объекта в зависимости от типа
                    // Например, если у вас есть классы, реализующие IGameObject с именами BoxObject, PlayerObject и т. д.
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
                        default:
                            throw new NotSupportedException($"Unsupported object type: {typeName}");
                    }
                }

                throw new JsonSerializationException("Type property not found in JSON");
            }
            catch (JsonReaderException ex)
            {
                throw new JsonSerializationException("Error deserializing IGameObject", ex);
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
