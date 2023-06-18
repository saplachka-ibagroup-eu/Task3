using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task3.Model;

namespace Task3.Manager
{
    public class RecordManager : IRecordManager
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        public void WriteData(IEnumerable<Record> data)
        {
            _context.Records.ExecuteDelete();
            _context.Records.AddRange(data);
            _context.SaveChanges();
        }

        public List<Record> FindAll(Func<Record, bool> predicate) => _context.Records
                .OrderBy(e => e.Id)
                .Where(predicate)
                .ToList();
    }
}
