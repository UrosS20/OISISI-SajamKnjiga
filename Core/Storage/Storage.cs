using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Storage
{
    public class Storage<T> where T : ISerializable, new()
    {
        private string _filePath;
        private readonly Serializer<T> _serializer = new();

        public Storage(string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? solutionRoot = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.Parent;

            if (solutionRoot == null)
            {
                throw new Exception("Ne mogu pronaći root");
            }

            _filePath = Path.Combine(solutionRoot.FullName, "Data", fileName);

            Console.WriteLine($"Storage FilePath: {_filePath}");
        }

        public List<T> Load()
        {
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Kreiran folder: {directory}");
            }

            if (!File.Exists(_filePath))
            {
                FileStream fs = File.Create(_filePath);
                fs.Close();
                Console.WriteLine($"Kreiran prazan fajl: {_filePath}");
                return new List<T>();
            }

            IEnumerable<string> lines = File.ReadLines(_filePath);
            List<T> objects = _serializer.FromCSV(lines);

            return objects;
        }

        public void Save(List<T> objects)
        {
            string serializedObjects = _serializer.ToCSV(objects);
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.Write(serializedObjects);
            }
        }
    }
}
