namespace NotebookStoreMVC.Services;

public class StorageDto : BaseDto
{
  public required int Capacity { get; set; }
  public required string Type { get; set; }
}
