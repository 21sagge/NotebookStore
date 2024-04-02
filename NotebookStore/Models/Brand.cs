namespace NotebookStore.Models
{
  public class Brand
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public string GetNameById(int id) {
      return Name;
    }
  }
}