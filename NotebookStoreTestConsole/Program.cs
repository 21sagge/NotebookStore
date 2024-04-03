using NotebookStore.Entities;

namespace NotebookStoreTestConsole;

class Program
{
  static void Main(string[] args)
  {
    var context = new NotebookStoreContext.NotebookStoreContext();

    // Delete(context);

    // Create(context);

    Read(context);
  }

  private static void Create(NotebookStoreContext.NotebookStoreContext context)
  {
    Console.WriteLine("Populating the database with sample data...");

    var brand1 = new Brand { Name = "Dell" };
    var brand2 = new Brand { Name = "HP" };
    var brand3 = new Brand { Name = "Lenovo" };

    context.Brands.Add(brand1);
    context.Brands.Add(brand2);
    context.Brands.Add(brand3);

    var model1 = new Model { Name = "Inspiron" };
    var model2 = new Model { Name = "Pavilion" };
    var model3 = new Model { Name = "ThinkPad" };

    context.Models.Add(model1);
    context.Models.Add(model2);
    context.Models.Add(model3);

    var cpu1 = new Cpu { Brand = "Intel", Model = "Core i5" };
    var cpu2 = new Cpu { Brand = "AMD", Model = "Ryzen 5" };
    var cpu3 = new Cpu { Brand = "Intel", Model = "Core i7" };

    context.Cpus.Add(cpu1);
    context.Cpus.Add(cpu2);
    context.Cpus.Add(cpu3);

    var display1 = new Display { Size = 15.6, Resolution = [1920, 1080], PanelType = "IPS" };
    var display2 = new Display { Size = 17.3, Resolution = [1920, 1080], PanelType = "OLED" };
    var display3 = new Display { Size = 14, Resolution = [2560, 1440], PanelType = "IPS" };

    context.Displays.Add(display1);
    context.Displays.Add(display2);
    context.Displays.Add(display3);

    var memory1 = new Memory { Capacity = 8, Speed = 2666 };
    var memory2 = new Memory { Capacity = 16, Speed = 3200 };
    var memory3 = new Memory { Capacity = 32, Speed = 3200 };

    context.Memories.Add(memory1);
    context.Memories.Add(memory2);
    context.Memories.Add(memory3);

    var storage1 = new Storage { Capacity = 256, Type = "SSD" };
    var storage2 = new Storage { Capacity = 512, Type = "SSD" };
    var storage3 = new Storage { Capacity = 1024, Type = "SSD" };

    context.Storages.Add(storage1);
    context.Storages.Add(storage2);
    context.Storages.Add(storage3);

    var newNotebook1 = new Notebook
    {
      BrandId = 1,
      ModelId = 1,
      Color = "Silver",
      Price = 1000,
      CpuId = 1,
      DisplayId = 1,
      MemoryId = 1,
      StorageId = 1
    };

    var newNotebook2 = new Notebook
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

    var newNotebook3 = new Notebook
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
    context.Notebooks.Add(newNotebook3);

    context.SaveChanges();
  }

  private static void Read(NotebookStoreContext.NotebookStoreContext context)
  {
    var notebooks = context.Notebooks.ToList();
    var brands = context.Brands.ToList();
    var models = context.Models.ToList();
    var cpus = context.Cpus.ToList();
    var displays = context.Displays.ToList();
    var memories = context.Memories.ToList();
    var storages = context.Storages.ToList();

    var notebookInfo = from notebook in notebooks
                       join brand in brands on notebook.BrandId equals brand.Id
                       join model in models on notebook.ModelId equals model.Id
                       join cpu in cpus on notebook.CpuId equals cpu.Id
                       join display in displays on notebook.DisplayId equals display.Id
                       join memory in memories on notebook.MemoryId equals memory.Id
                       join storage in storages on notebook.StorageId equals storage.Id
                       select new
                       {
                         Brand = brand.Name,
                         Model = model.Name,
                         notebook.Color,
                         notebook.Price,
                         Cpu = $"{cpu.Brand} {cpu.Model}",
                         Display = $"{display.Size}\" {display.Resolution[0]}x{display.Resolution[1]} {display.PanelType}",
                         Memory = $"{memory.Capacity}GB {memory.Speed}MHz",
                         Storage = $"{storage.Capacity}GB {storage.Type}"
                       };

    foreach (var notebook in notebookInfo)
    {
      System.Console.WriteLine($"{notebook.Brand} {notebook.Model}");
      System.Console.WriteLine($"Color: {notebook.Color}");
      System.Console.WriteLine($"Price: {notebook.Price}");
      System.Console.WriteLine($"Cpu: {notebook.Cpu}");
      System.Console.WriteLine($"Display: {notebook.Display}");
      System.Console.WriteLine($"Memory: {notebook.Memory}");
      System.Console.WriteLine($"Storage: {notebook.Storage}");
      System.Console.WriteLine();
    }
  }

  private static void Delete(NotebookStoreContext.NotebookStoreContext context)
  {
    var notebook = context.Notebooks.FirstOrDefault() ?? throw new InvalidOperationException("No notebooks found");
    context.Notebooks.Remove(notebook);

    var memory = context.Memories.FirstOrDefault() ?? throw new InvalidOperationException("No memories found");
    context.Memories.Remove(memory);

    var display = context.Displays.FirstOrDefault() ?? throw new InvalidOperationException("No displays found");
    context.Displays.Remove(display);

    var cpu = context.Cpus.FirstOrDefault() ?? throw new InvalidOperationException("No cpus found");
    context.Cpus.Remove(cpu);

    var model = context.Models.FirstOrDefault() ?? throw new InvalidOperationException("No models found");
    context.Models.Remove(model);

    var brand = context.Brands.FirstOrDefault() ?? throw new InvalidOperationException("No brands found");
    context.Brands.Remove(brand);

    context.SaveChanges();
  }
}
