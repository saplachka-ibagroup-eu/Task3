using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.Model;

namespace Task3.Manager;

public class RecordManager : IRecordManager
{
    private readonly ApplicationContext _context = new ApplicationContext();
    

    public List<Record> FindAll(Func<Record, bool> predicate) => _context.Records
            .OrderBy(e => e.Id)
            .Where(predicate)
            .ToList();

    public async Task WriteDataAsync(IAsyncEnumerable<Record> data)
    {
       
        await _context.Records.ExecuteDeleteAsync();
        await foreach (var item in data)
        {
            await _context.Records.AddAsync(item);
        }

        await _context.SaveChangesAsync();
       
    }
}
