using System.IO;

namespace Microsoft.FluentMessaging
{
    public interface ISerializer<T>
    {
        Stream Serialize(T toSerialize);
        T Deserialize(Stream sourceStream);

    }
}
