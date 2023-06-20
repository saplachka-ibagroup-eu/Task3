using System.Collections.Generic;
using Task3.Model;

namespace Task3.Manager;

public interface ICSVLoader
{
    IAsyncEnumerable<Record> ReadCsvAsync(string file);
}