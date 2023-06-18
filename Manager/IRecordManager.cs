using System;
using System.Collections.Generic;
using Task3.Model;

namespace Task3.Manager
{
    public interface IRecordManager
    {
        List<Record> FindAll(Func<Record, bool> predicate);
        void WriteData(IEnumerable<Record> data);
    }
}