using CsvToWpf.Commands.Base;
using CsvToWpf.Models;
using CsvToWpf.ViewModels.Base;
using CsvToWpfData;
using CsvToWpfData.DataSources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CsvToWpf.ViewModels
{
    internal class ClientAreaViewModel : ViewModelBase
    {
        private readonly IRepository<Client> cientRepository;

        #region Collections
        private ObservableCollection<ClientModel> clients = null!;
        public ObservableCollection<ClientModel> Clients
        {
            get => clients;
            set
            {
                clients = value;
                if (Clients != null)
                {
                    ClientsView = CollectionViewSource.GetDefaultView(Clients);
                    ClientsView.Filter = ClientsFilter;
                }
                SetProperty(ref clients, value);
            }
        }

        private ICollectionView clientsView = null!;
        public ICollectionView ClientsView
        {
            get => clientsView;
            set => SetProperty(ref clientsView, value);
        }

        private ObservableCollection<Country> countries = null!;
        public ObservableCollection<Country> Countries
        {
            get => countries;
            set => SetProperty(ref countries, value);
        }

        private List<string> clientSortOptions = null!;
        public List<string> ClientSortOptions
        {
            get => clientSortOptions;
            set => SetProperty(ref clientSortOptions, value);
        } 
        #endregion

        #region Commands
        public ICommand OnClientsSort { get; }
        private bool CanOnClientsSortExecute(object param) => true;
        private void OnOnClientsSortsExecuted(object param)
        {
            SortClient(param);
        }

        private void SortClient(object param)
        {
            if (param is SelectionChangedEventArgs args)
            {
                if (args.AddedItems.Count > 0 && args.AddedItems[0] is not null)
                {
                    ClientsView.SortDescriptions.Clear();
                    ClientsView.SortDescriptions.Add(new SortDescription((string)args.AddedItems[0]!, ListSortDirection.Ascending));
                    ClientsView.Refresh();
                }
            }
             
        }

        public ICommand OnClientsFilter { get; }
        private bool CanOnClientsFilterExecute(object param) => true;
        private void OnOnClientsFilterExecuted(object param)
        {
            ClientsView.Refresh();
        }
        #endregion

        public ClientAreaViewModel(IRepository<Client> cientRepository) 
        {
            this.cientRepository = cientRepository;
            OnClientsFilter = new Command(OnOnClientsFilterExecuted, CanOnClientsFilterExecute);
            OnClientsSort = new Command(OnOnClientsSortsExecuted, CanOnClientsSortExecute);

            InitializeDataDependentCollections();
            InitializeFunctionalCollections();
        }

        private void InitializeDataDependentCollections()
        {
            var clients = cientRepository.GetAll()?.Select(c => new ClientModel { Name = c.Name, Country = c.Country, PhoneNumber = c.PhoneNumber });
            Clients = new ObservableCollection<ClientModel>(clients ?? Enumerable.Empty<ClientModel>());
            Countries = new ObservableCollection<Country>(Clients.Select(cl => new Country { Name = cl.Country }));
        }

        private void InitializeFunctionalCollections()
        {
            ClientSortOptions = typeof(ClientModel).GetSortablePropertyNames();
        }

        private bool ClientsFilter(object obj)
        {
            if (Countries is null)
            {
                return true;
            }

            if (!(obj is ClientModel clientModel))
            {
                return false;
            }

            return Countries.All(c => c.IsChecked == false) ||
                   Countries.Any(c => c.Name == clientModel.Country && c.IsChecked == true);
        }

    }
}
