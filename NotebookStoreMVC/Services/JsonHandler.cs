﻿using System.Text;
using System.Text.Json;

namespace NotebookStoreMVC.Services
{
    public class JsonHandler : ISerializer
    {
        public TDest? Deserialize<TDest>(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(source));
            var deserializedData = JsonSerializer.Deserialize<TDest>(stream);
            
            return deserializedData;
        }

        public string Serialize<TSource>(TSource graph)
        {
            return JsonSerializer.Serialize(graph);
        }
    }
}
