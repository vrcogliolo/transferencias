using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Numerics;

namespace Transferencias.App.Helpers;

public class BigIntegerJsonConverter : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        value ??= "0";
        return BigInteger.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}