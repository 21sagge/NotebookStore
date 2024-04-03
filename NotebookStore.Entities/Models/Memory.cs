namespace NotebookStore.Entities
{
  public class Memory
  {
    public int Id { get; set; }
    public required string Capacity { get; set; }
    public required string Speed { get; set; }
  }
}