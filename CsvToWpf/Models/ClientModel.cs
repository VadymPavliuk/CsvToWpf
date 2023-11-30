using CsvToWpf.Utility;

namespace CsvToWpf.Models
{
    internal class ClientModel
    {
        [Sortable(true)]
        public string Name { get; set; } = null!;
        [Sortable(true)]
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
