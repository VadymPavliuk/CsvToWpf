namespace CsvToWpfData.DataSources;

//Scv repository can be done in more generic way
//With headers to properties validation, better error handling
//Didn't go all out on parser, since it's not the focus here
public class ClientsCsvRepository : IRepository<Client>
{
    private readonly string _filePath;

    public ClientsCsvRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<Client> GetAll()
    {
        List<Client> clients = new();

        try
        {
            string[] lines = File.ReadAllLines(_filePath);

            // Skiping header
            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(',');

                if (values.Length >= 3)
                {
                    Client client = new Client(values[0].Trim(), values[1].Trim(), values[2].Trim());
                    clients.Add(client);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., file not found, format issues, etc.)
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }

        return clients;
    }
}


