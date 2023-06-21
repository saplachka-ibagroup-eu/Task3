using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Model;

namespace Task3.Manager;

public interface IExportManager
{
    Task ExportToExcelAsync(List<Record> data);
    Task ExportToXMLAsync(List<Record> data);
}