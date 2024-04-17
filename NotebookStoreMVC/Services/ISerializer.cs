namespace NotebookStoreMVC.Services;

public interface ISerializer
{
    string Format { get; }

    string Serialize<TSource>(TSource graph);
    TDest? Deserialize<TDest>(string source);
}
