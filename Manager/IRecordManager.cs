using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Model;

namespace Task3.Manager;

public interface IRecordManager
{
    List<Record> FindAll(Func<Record, bool> predicate);
    Task WriteDataAsync(IAsyncEnumerable<Record> data);
}