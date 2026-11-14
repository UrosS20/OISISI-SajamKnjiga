using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Storage.Serialization
{
    public class Serializer<T> where T : ISerializable, new()
    {
        private const char Delimiter = '|';

        public string ToCSV(List<T> objects)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T obj in objects)
            {
                sb.AppendLine(string.Join(Delimiter, obj.ToCSV()));
            }
            return sb.ToString();
        }

        public List<T> FromCSV(IEnumerable<string> lines)
        {
            List<T> objects = new();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] values = line.Split(Delimiter);
                T obj = new();
                obj.FromCSV(values);
                objects.Add(obj);
            }
            return objects;
        }
    }
}
