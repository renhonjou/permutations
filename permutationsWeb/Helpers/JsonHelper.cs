using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace permutationsWeb.Helpers
{
    public static class JsonHelper
    {
        public static String Serialize<T>(T obj)
        {
            using (var stream = new MemoryStream())
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                jsonSerializer.WriteObject(stream, obj);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(String json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                stream.Position = 0;

                try
                {
                    return jsonSerializer.ReadObject(stream) as T;
                }
                catch
                {
                    return default(T);
                }
            }
        }
    }
}
