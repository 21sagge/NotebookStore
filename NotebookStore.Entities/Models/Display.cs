namespace NotebookStore.Entities;

public class Display
{
  public int Id { get; set; }
    /// <summary>
    /// Misura in pollici del display
    /// </summary>
  public required double Size { get; set; }
  public required int ResolutionWidth { get; set; }
  public required int ResolutionHeight { get; set; }
  public required string PanelType { get; set; }
}