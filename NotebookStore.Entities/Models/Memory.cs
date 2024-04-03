namespace NotebookStore.Entities
{
  public class Memory
  {
    public int Id { get; set; }
    public required int Capacity { get; set; }
    public required int Speed { get; set; }
  }
}