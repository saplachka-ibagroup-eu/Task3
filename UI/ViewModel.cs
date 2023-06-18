using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task3.Manager;
using Task3.Model;

namespace Task3.UI
{
    public class ViewModel : ViewModelBase
    {
        private readonly ICSVLoader _csvLoader;
        private readonly IRecordManager _recordManager;
        private readonly IExportManager _exportManager;
        private Record _record;
        private ObservableCollection<Record> _records;
        private ICommand _importCommand;
        private ICommand _filterCommand;
        private ICommand _exportToExcelCommand;
        private ICommand _exportToXMLCommand;


        public ViewModel()
        {
            _csvLoader = new CSVLoader();
            _recordManager = new RecordManager();
            _exportManager = new ExportManager();
        }


        public ObservableCollection<Record> Records
        {
            get
            {
                _records ??= new ObservableCollection<Record>();
                return _records;
            }
            set
            {
                _records = value;
                NotifyPropertyChanged(nameof(Records));
            }
        }

        private string _date;
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        private string _surName;
        public string SurName
        {
            get
            {
                return _surName;
            }
            set
            {
                _surName = value;
                NotifyPropertyChanged(nameof(SurName));
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged(nameof(City));
            }
        }

        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                NotifyPropertyChanged(nameof(Country));
            }
        }

        public ICommand ImportCommand
        {
            get
            {
                if (_importCommand == null)
                {
                    _importCommand = new RelayCommand(param => ImportFileAsync(),
                        null);
                }
                return _importCommand;
            }
        }

        public ICommand ExportToExcelCommand
        {
            get
            {
                if (_exportToExcelCommand == null)
                {
                    _exportToExcelCommand = new RelayCommand(param => ExportData("Excel"),
                        null);
                }
                return _exportToExcelCommand;
            }
        }

        public ICommand ExportToXMLCommand
        {
            get
            {
                if (_exportToXMLCommand == null)
                {
                    _exportToXMLCommand = new RelayCommand(param => ExportData("XML"),
                        null);
                }
                return _exportToXMLCommand;
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(param => ReadData(),
                        null);
                }
                return _filterCommand;
            }
        }

        private async void ImportFileAsync()
        {


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open csv file";
            ofd.DefaultExt = "*.csv";
            ofd.Filter = "Documents (*.csv)|*.csv";
            ofd.ShowDialog();

            string fileName = ofd.FileName;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                FileInfo fi = new FileInfo(fileName);
                string csv = fi.FullName;
                 var records = _csvLoader.ReadCsv(csv);
                _recordManager.WriteData(records);
                await ReadData();
            }
        }


        private Task ReadData()
        {
            Records.Clear();

            return Task.Run(() =>
            {
                var records = _recordManager.FindAll(Predicate);
                foreach (var record in records)
                {
                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        Records.Add(record);
                    });
                }
            });
        }



        private bool Predicate(Record entry)
        {
            return (string.IsNullOrEmpty(Date) || entry.Date.Contains(Date))
                && (string.IsNullOrEmpty(FirstName) || entry.FirstName.Contains(FirstName)) &&
                (string.IsNullOrEmpty(LastName) || entry.LastName.Contains(LastName)) &&
                (string.IsNullOrEmpty(SurName) || entry.SurName.Contains(SurName)) &&
                (string.IsNullOrEmpty(City) || entry.City.Contains(City)) &&
                (string.IsNullOrEmpty(Country) || entry.Country.Contains(Country));
        }

        private Task ExportData(string type) => Task.Run(() =>
            {
                List<Record> data = _recordManager.FindAll(Predicate);
                if (type == "Excel")
                {
                    _exportManager.ExportToExcel(data);

                }
                else
                {
                    _exportManager.ExportToXML(data);
                }

                MessageBox.Show($"Exported to {type}");
            });
    }

}
