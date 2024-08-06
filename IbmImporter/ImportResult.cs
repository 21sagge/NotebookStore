namespace IbmImporter;

public class ImportResult
{
    public int Success { get; set; }
    public List<UnimportedNotebook> Unsuccess { get; set; } = new();
}
