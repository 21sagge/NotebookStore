namespace NotebookStore.Entities;

public class Display
{
  public int Id { get; set; }

  /// <summary>
  /// Size of the display in inches (e.g. 13.3)
  /// </summary>
  public required double Size { get; set; }

  /// <summary>
  /// Resolution width (e.g. 2560, 1920)
  /// </summary>
  public required int ResolutionWidth { get; set; }

  /// <summary>
  /// Resolution height (e.g. 1600, 1080)
  /// </summary>
  public required int ResolutionHeight { get; set; }

  /// <summary>
  /// Type of the panel (IPS, TN, OLED, etc.)
  /// </summary>
  public required string PanelType { get; set; }
}