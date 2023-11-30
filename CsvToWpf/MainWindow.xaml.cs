using CsvToWpf.ViewModels;
using CsvToWpfData;
using CsvToWpfData.DataSources;
using System.Windows;

namespace CsvToWpf
{
    public partial class MainWindow : Window
    {
        private ClientAreaViewModel clientAreaViewModel;

        public MainWindow(IRepository<Client> repository)
        {
            clientAreaViewModel = new ClientAreaViewModel(repository);
            this.DataContext = clientAreaViewModel;

            InitializeComponent();
        }
    }
}
