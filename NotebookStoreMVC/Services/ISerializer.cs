namespace NotebookStoreMVC.Services;

public interface ISerializer
{
    string Serialize<TSource>(TSource graph);
    TDest? Deserialize<TDest>(string source);
}
