using Newtonsoft.Json;

namespace Library;
public class JsonAccess<T>
{
    public string Path;

    public JsonAccess(string path)
    {
        Path = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 8) + @"\Library" + path;
    }

    public List<T> LoadAll()
    {
        string json = File.ReadAllText(Path);
        var list = JsonConvert.DeserializeObject<List<T>>(json);
        return list!;
    }

    public T LoadItem()
    {
        string json = File.ReadAllText(Path);
        T item = JsonConvert.DeserializeObject<T>(json)!;
        return item;
    }

    public void WriteAll(List<T> inputList)
    {
        var toAddList = JsonConvert.SerializeObject(inputList, Formatting.Indented);
        File.WriteAllText(Path, toAddList);
    }

    public void WriteItem(T inputItem)
    {
        var toAddItem = JsonConvert.SerializeObject(inputItem, Formatting.Indented);
        File.WriteAllText(Path, toAddItem);
    }
}