namespace NotebookStoreMVC.Services;

public interface ISerializer<T>
{
  string XmlSerialize(IEnumerable<T> data);
  IEnumerable<T> XmlDeserialize(string xml);
  string JsonSerialize(IEnumerable<T> data);
  IEnumerable<T> JsonDeserialize(string json);
}
