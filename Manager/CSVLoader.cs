using System.Collections.Generic;
using System.IO;
using System.Text;
using Task3.Model;

namespace Task3.Manager
{
    public class CSVLoader : ICSVLoader
    {
        private readonly ApplicationContext _context = new ApplicationContext();

        public IEnumerable<Record> ReadCsv(string file)
        {
            using (StreamReader sr = new StreamReader(file, Encoding.Default))
            {

                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();

                    if (string.IsNullOrEmpty(line)) continue;

                    string[] fields = line.Split(',');
                    Record record = new Record()
                    {
                        Date = fields[0],
                        FirstName = fields[1],
                        LastName = fields[2],
                        SurName = fields[3],
                        City = fields[4],
                        Country = fields[5],
                    };
                    yield return record;
                }
            }
        }


    }
}
