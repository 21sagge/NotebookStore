namespace IbmImporter;

public class ImportResult
{
    public int Success { get; set; }
    public List<UnimportedNotebook> Unsuccess { get; set; } = new();

    public bool IsSuccess => Success > 0;

    public string ResultMessage 
    => Success == 0
        ? $"{Unsuccess.Count} notebooks failed to import"
        : Success == (Unsuccess.Count + Success)
            ? $"{Success} notebooks imported successfully"
            : $"{Success} notebooks imported successfully, {Unsuccess.Count} notebooks failed to import";
}
