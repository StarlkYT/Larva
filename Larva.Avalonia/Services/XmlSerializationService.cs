using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Larva.Avalonia.Services;

public static class XmlSerializationService
{
    public static string Serialize(object instance)
    {
        using var memoryStream = new MemoryStream();
        using var reader = new StreamReader(memoryStream);

        var serializer = new DataContractSerializer(instance.GetType());
        
        serializer.WriteObject(memoryStream, instance);
        memoryStream.Position = 0;

        return reader.ReadToEnd();
    }

    public static T? Deserialize<T>(string xml) where T : notnull
    {
        using var stream = new MemoryStream();

        var data = Encoding.UTF8.GetBytes(xml);

        stream.Write(data, 0, data.Length);
        stream.Position = 0;

        try
        {
            var deserializer = new DataContractSerializer(typeof(T));
            return (T?) deserializer.ReadObject(stream);
        }
        catch (SerializationException)
        {
            return default;
        }
    }
}