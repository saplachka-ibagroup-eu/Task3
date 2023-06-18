using System.Collections.Generic;
using Task3.Model;

namespace Task3.Manager
{
    public interface IExportManager
    {
        void ExportToExcel(List<Record> data);
        void ExportToXML(List<Record> data);
    }
}