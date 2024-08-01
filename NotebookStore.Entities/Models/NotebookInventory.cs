namespace NotebookStore.Entities;

public class NotebookInventory
{
	public int NotebookId { get; set; }
	public int Quantity { get; set; }
	public virtual Notebook Notebook { get; set; }
}
