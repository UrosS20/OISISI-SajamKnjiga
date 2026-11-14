using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Storage.Serialization
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
