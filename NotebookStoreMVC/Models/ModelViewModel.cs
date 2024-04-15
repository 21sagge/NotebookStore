using System.ComponentModel.DataAnnotations;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Models;

public class ModelViewModel
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Name { get; set; }
}
