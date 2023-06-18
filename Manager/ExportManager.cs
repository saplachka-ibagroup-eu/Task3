using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Task3.Model;

namespace Task3.Manager
{
    public class ExportManager : IExportManager
    {

        public void ExportToExcel(List<Record> data)
        {
            var wb = new XLWorkbook(); //create workbook
            var ws = wb.Worksheets.Add("Data"); //add worksheet to workbook


            if (data != null && data.Count() > 0)
            {
                ws.FirstCell().InsertData(data);
                ws.Columns().AdjustToContents();
            }

            wb.SaveAs(@"D:\data.xlsx");

        }

        public void ExportToXML(List<Record> data)
        {
            string savePath = @"D:\AddresBook.xml";

            var xmlSavePath = new XElement("TestProgram",
            from record in data
            select new XElement("Record", new XAttribute("Id", record.Id),
                               new XElement("Date", record.Date),
                               new XElement("FirstName", record.FirstName),
                               new XElement("LastName", record.LastName),
                               new XElement("SurName", record.SurName),
                               new XElement("City", record.City),
                               new XElement("Country", record.Country)
                           ));
            xmlSavePath.Save(savePath);
        }

    }
}

