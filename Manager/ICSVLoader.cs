using System.Collections.Generic;
using Task3.Model;

namespace Task3.Manager
{
    public interface ICSVLoader
    {
        IEnumerable<Record> ReadCsv(string file);
    }
}