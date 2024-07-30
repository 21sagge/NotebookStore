namespace NotebookStore.Business;

public class Claims
{
	public const string CreateNotebook = "Create Notebook";
	public const string ReadNotebook = "Read Notebook";
	public const string UpdateNotebook = "Update Notebook";
	public const string DeleteNotebook = "Delete Notebook";
	public const string CreateBrand = "Create Brand";
	public const string ReadBrand = "Read Brand";
	public const string UpdateBrand = "Update Brand";
	public const string DeleteBrand = "Delete Brand";
	public const string CreateCpu = "Create Cpu";
	public const string ReadCpu = "Read Cpu";
	public const string UpdateCpu = "Update Cpu";
	public const string DeleteCpu = "Delete Cpu";
	public const string CreateDisplay = "Create Display";
	public const string ReadDisplay = "Read Display";
	public const string UpdateDisplay = "Update Display";
	public const string DeleteDisplay = "Delete Display";
	public const string CreateMemory = "Create Memory";
	public const string ReadMemory = "Read Memory";
	public const string UpdateMemory = "Update Memory";
	public const string DeleteMemory = "Delete Memory";
	public const string CreateModel = "Create Model";
	public const string ReadModel = "Read Model";
	public const string UpdateModel = "Update Model";
	public const string DeleteModel = "Delete Model";
	public const string CreateStorage = "Create Storage";
	public const string ReadStorage = "Read Storage";
	public const string UpdateStorage = "Update Storage";
	public const string DeleteStorage = "Delete Storage";

	public static readonly string[] AllClaims = new string[]
	{
		CreateNotebook,
		ReadNotebook,
		UpdateNotebook,
		DeleteNotebook,
		CreateBrand,
		ReadBrand,
		UpdateBrand,
		DeleteBrand,
		CreateCpu,
		ReadCpu,
		UpdateCpu,
		DeleteCpu,
		CreateDisplay,
		ReadDisplay,
		UpdateDisplay,
		DeleteDisplay,
		CreateMemory,
		ReadMemory,
		UpdateMemory,
		DeleteMemory,
		CreateModel,
		ReadModel,
		UpdateModel,
		DeleteModel,
		CreateStorage,
		ReadStorage,
		UpdateStorage,
		DeleteStorage
	};
}
