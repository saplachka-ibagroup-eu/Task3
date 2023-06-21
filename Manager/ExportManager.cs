using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Task3.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace Task3.Manager;

public class ExportManager : IExportManager
{

    public async Task ExportToExcelAsync(List<Record> data)
    {

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var file = new FileInfo(@"D:\AddresBook.xlsx");

        DeleteIfExists(file);

        using var package = new ExcelPackage(file);
        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        var range = workSheet.Cells.LoadFromCollection(data, true);
        range.AutoFitColumns();
        await package.SaveAsync();

    }

    private static void DeleteIfExists(FileInfo file)
    {
        if (file.Exists)
        {
            file.Delete();
        }
    }

    public async Task ExportToXMLAsync(List<Record> data)
    {
        using FileStream fs = new(@"D:\output.xml", FileMode.Create);
        CancellationTokenSource cts = new();

        var root = new XElement("TestProgram",
        from record in data
        select new XElement("Record", new XAttribute("Id", record.Id),
                           new XElement("Date", record.Date),
                           new XElement("FirstName", record.FirstName),
                           new XElement("LastName", record.LastName),
                           new XElement("SurName", record.SurName),
                           new XElement("City", record.City),
                           new XElement("Country", record.Country)
                       ));
        await root.SaveAsync(fs, SaveOptions.None, cts.Token);
    }
}
