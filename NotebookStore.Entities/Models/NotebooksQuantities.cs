namespace NotebookStore.Entities;

public class NotebooksQuantities
{
	public int NotebookId { get; set; }
	public int Quantity { get; set; }
	public virtual Notebook Notebook { get; set; }
}
