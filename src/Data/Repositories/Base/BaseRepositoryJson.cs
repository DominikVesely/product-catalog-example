using System.Text.Json;

namespace Data.Repositories.Base;

public abstract class BaseRepositoryJson<T>
{
    protected readonly List<T> _data;

    public BaseRepositoryJson(string filename)
    {
        _data = LoadJsonData(filename) ?? throw new ArgumentException($"File is probably empty. File={filename}", nameof(filename));
    }

    private static List<T> LoadJsonData(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "MockData", fileName);
        var jsonString = File.ReadAllText(path);

        return JsonSerializer.Deserialize<List<T>>(jsonString)!;
    }
}
