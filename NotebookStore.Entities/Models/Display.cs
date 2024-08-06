using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Display : IAuditable
{
  public int Id { get; set; }

  /// <summary>
  /// Size of the display in inches (e.g. 13.3)
  /// </summary>
  [Range(13, 17)]
  public double Size { get; set; }

  /// <summary>
  /// Resolution width (e.g. 2560, 1920)
  /// </summary>
  [Range(1280, 3840)]
  public int ResolutionWidth { get; set; }

  /// <summary>
  /// Resolution height (e.g. 1600, 1080)
  /// </summary>
  [Range(720, 2160)]
  public int ResolutionHeight { get; set; }

  /// <summary>
  /// Type of the panel (IPS, TN, OLED, etc.)
  /// </summary>
  [MaxLength(10)]
  public string PanelType { get; set; } = string.Empty;
  public string? CreatedBy { get; set; }
  public string CreatedAt { get; set; } = string.Empty;
}