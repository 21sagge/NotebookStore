namespace NotebookStore.Entities
{
  public class Display
  {
    public int Id { get; set; }
    public required double Size { get; set; }
    public required int[] Resolution { get; set; }
    public required string PanelType { get; set; }
  }
}