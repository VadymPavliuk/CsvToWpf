namespace CsvToWpfData.DataSources;

public interface IRepository <T> where T : class
{
    List<T> GetAll();

    //Other CRUD operations seems out of scope for this task
}

