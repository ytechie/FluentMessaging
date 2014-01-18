using System.IO;

namespace FluentMessaging
{
    public interface ISerializer<T>
    {
        Stream Serialize(T toSerialize);
        T Deserialize(Stream sourceStream);

    }
}
