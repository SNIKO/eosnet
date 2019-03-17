using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EOS.Client.Models
{
    public class EosDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime) value;
            var iso = date.ToUniversalTime().ToString("s");
            
            writer.WriteValue(iso);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string timeString = reader.Value.ToString();
            var utcDate =timeString.EndsWith("M", StringComparison.OrdinalIgnoreCase)? timeString: $"{reader.Value}Z";
            return DateTime.Parse(utcDate);
        }
    }
}