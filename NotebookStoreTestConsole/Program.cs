using NotebookStore.Entities;

namespace NotebookStoreTestConsole;

class Program
{
  // public static void PrintNotebook(Notebook notebook)
  // {
  //   System.Console.WriteLine($"{notebook.Brand.Name} {notebook.Model.Name}");
  //   System.Console.WriteLine($"Color: {notebook.Color}");
  //   System.Console.WriteLine($"Price: {notebook.Price}");
  //   System.Console.WriteLine($"Cpu: {notebook.Cpu.Brand} {notebook.Cpu.Model}");
  //   System.Console.WriteLine($"Display: {notebook.Display.Size} {notebook.Display.Resolution} {notebook.Display.PanelType}");
  //   System.Console.WriteLine($"Memory: {notebook.Memory.Capacity}GB {notebook.Memory.Speed}MHz");
  //   System.Console.WriteLine($"Storage: {notebook.Storage.Capacity}GB {notebook.Storage.Type}");
  //   System.Console.WriteLine();
  // }

  static void Main(string[] args)
  {
    var context = new NotebookStoreContext.NotebookStoreContext();

    // Read everything from the database.
    // var notebooks = context.Notebooks.ToList();
    // var brands = context.Brands.ToList();
    // var models = context.Models.ToList();
    // var cpus = context.Cpus.ToList();
    // var displays = context.Displays.ToList();
    // var memories = context.Memories.ToList();
    // var storages = context.Storages.ToList();

    // // Join the tables to get the full notebook information usinig LINQ.
    // var notebookInfo = from notebook in notebooks
    //                    join brand in brands on notebook.BrandId equals brand.Id
    //                    join model in models on notebook.ModelId equals model.Id
    //                    join cpu in cpus on notebook.CpuId equals cpu.Id
    //                    join display in displays on notebook.DisplayId equals display.Id
    //                    join memory in memories on notebook.MemoryId equals memory.Id
    //                    join storage in storages on notebook.StorageId equals storage.Id
    //                    select new
    //                    {
    //                      Brand = brand.Name,
    //                      Model = model.Name,
    //                      notebook.Color,
    //                      notebook.Price,
    //                      Cpu = $"{cpu.Brand} {cpu.Model}",
    //                      Display = $"{display.Size} {display.Resolution} {display.PanelType}",
    //                      Memory = $"{memory.Capacity}GB {memory.Speed}MHz",
    //                      Storage = $"{storage.Capacity}GB {storage.Type}"
    //                    };

    // // Print the notebook information.
    // foreach (var notebook in notebookInfo)
    // {
    //   System.Console.WriteLine($"{notebook.Brand} {notebook.Model}");
    //   System.Console.WriteLine($"Color: {notebook.Color}");
    //   System.Console.WriteLine($"Price: {notebook.Price}");
    //   System.Console.WriteLine($"Cpu: {notebook.Cpu}");
    //   System.Console.WriteLine($"Display: {notebook.Display}");
    //   System.Console.WriteLine($"Memory: {notebook.Memory}");
    //   System.Console.WriteLine($"Storage: {notebook.Storage}");
    //   System.Console.WriteLine();
    // }

    // Add a new notebook to the database.

    var newNotebook1 = new Notebook
    {
      BrandId = 2,
      ModelId = 2,
      Color = "Black",
      Price = 2000,
      CpuId = 2,
      DisplayId = 2,
      MemoryId = 2,
      StorageId = 2
    };

    var newNotebook2 = new Notebook
    {
      BrandId = 3,
      ModelId = 3,
      Color = "White",
      Price = 3000,
      CpuId = 3,
      DisplayId = 3,
      MemoryId = 3,
      StorageId = 3
    };

    context.Notebooks.Add(newNotebook1);
    context.Notebooks.Add(newNotebook2);
    context.SaveChanges();
  }
}
