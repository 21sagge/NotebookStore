namespace NotebookStore.Entities
{
  public class Storage
  {
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Capacity { get; set; }
  }
}