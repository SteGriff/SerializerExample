using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializerExample
{
    /// <summary>
    /// Helps with serializing an object to binary and back again.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes an object to binary
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <param name="Output">Binary output of the object</param>
        public static void ObjectToBinary(object Object, out byte[] Output)
        {
            try
            {
                using (MemoryStream Stream = new MemoryStream())
                {
                    BinaryFormatter Formatter = new BinaryFormatter();
                    Formatter.Serialize(Stream, Object);
                    Stream.Seek(0, 0);
                    Output = Stream.ToArray();
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// Deserializes an object from binary
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="Binary">Binary representation of the object</param>
        /// <param name="Object">Object after it is deserialized</param>
        public static void BinaryToObject<T>(byte[] Binary, out T Object)
        {
            try
            {
                using (MemoryStream Stream = new MemoryStream())
                {
                    Stream.Write(Binary, 0, Binary.Length);
                    Stream.Seek(0, 0);
                    BinaryFormatter Formatter = new BinaryFormatter();
                    Object = (T)Formatter.Deserialize(Stream);
                }
            }
            catch { throw; }
        }
    }
}
