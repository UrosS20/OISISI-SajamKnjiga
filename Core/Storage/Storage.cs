using Core.Storage.Serialization;
using System;
using System.Collections.Generic;

using System.Text;

namespace Core.Storage
{
    public class Storage<T> where T : ISerializable, new()
    {
        private readonly string _fileName;
        private readonly Serializer<T> _serializer = new();

        public Storage(string fileName)
        {
            string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string dataFolder = Path.Combine(solutionRoot, "Core", "Data");

            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            _fileName = Path.Combine(dataFolder, fileName);

            if (!File.Exists(_fileName))
            {
                File.Create(_fileName).Close();
            }
        }

        public List<T> Load()
        {
            IEnumerable<string> lines = File.ReadLines(_fileName);
            return _serializer.FromCSV(lines);
        }

        public void Save(List<T> objects)
        {
            string serializedObjects = _serializer.ToCSV(objects);
            using (StreamWriter streamWriter = new StreamWriter(_fileName))
            {
                streamWriter.Write(serializedObjects);
            }
        }
    }
}
