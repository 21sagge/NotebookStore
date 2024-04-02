namespace NotebookStore.Models
{
  public class Display
  {
    public int Id { get; set; }
    public required string Size { get; set; }
    public required string Resolution { get; set; }
    public required string PanelType { get; set; }
  }
}