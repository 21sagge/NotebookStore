namespace NotebookStoreMVC.Services;

public class MemoryDto : BaseDto
{
  public required int Capacity { get; set; }
  public required int Speed { get; set; }
}
