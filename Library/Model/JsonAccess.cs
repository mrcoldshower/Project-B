using Newtonsoft.Json;

namespace Library;
public class JsonAccess<T>
{
    public string Path;

    public JsonAccess(string path)
    {
        Path = Directory.GetCurrentDirectory() + path;
    }

    public List<T> LoadAll()
    {
        string json = File.ReadAllText(Path);
        var list = JsonConvert.DeserializeObject<List<T>>(json);
        return list!;
    }

    public void WriteAll(List<T> inputList)
    {
        var toAddList = JsonConvert.SerializeObject(inputList, Formatting.Indented);
        File.WriteAllText(Path, toAddList);
    }
}