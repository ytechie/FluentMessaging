using System.Collections.Generic;
using System.IO;

namespace Microsoft.FluentMessaging
{
    public interface ISerializer<T>
    {
        Stream Serialize(IEnumerable<T> toSerialize);
        IEnumerable<T> Deserialize(Stream sourceStream);
    }
}
