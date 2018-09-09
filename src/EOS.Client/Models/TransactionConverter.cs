using System;
using Newtonsoft.Json;

namespace EOS.Client.Models
{
    public class TransactionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return reader.Value;
            }
            else
            {
                var transaction = serializer.Deserialize<SignedTransaction>(reader);
                return transaction;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string) || objectType == typeof(SignedTransaction);
        }
    }
}