using NotebookStore.Models;

namespace NotebookStore
{
  class Program
  {
    public static void PrintNotebook(NotebookStore.Models.Notebook notebook)
    {
      System.Console.WriteLine($"{notebook.Brand.Name} {notebook.Model.Name}");
      System.Console.WriteLine($"Color: {notebook.Color}");
      System.Console.WriteLine($"Price: {notebook.Price}");
      System.Console.WriteLine($"Cpu: {notebook.Cpu.Brand} {notebook.Cpu.Model}");
      System.Console.WriteLine($"Display: {notebook.Display.Size} {notebook.Display.Resolution} {notebook.Display.PanelType}");
      System.Console.WriteLine($"Memory: {notebook.Memory.Capacity}GB {notebook.Memory.Speed}MHz");
      System.Console.WriteLine($"Storage: {notebook.Storage.Capacity}GB {notebook.Storage.Type}");
      System.Console.WriteLine();
    }

    static void Main(string[] args)
    {
      var model1 = new Model
      {
        Id = 1,
        Name = "Inspiron 15 3000"
      };

      var model2 = new Model
      {
        Id = 2,
        Name = "Inspiron 15 5000"
      };

      var model3 = new Model
      {
        Id = 3,
        Name = "XPS 13"
      };

      var display1 = new Display
      {
        Id = 1,
        Size = "15.6",
        Resolution = "1920x1080",
        PanelType = "IPS"
      };

      var display2 = new Display
      {
        Id = 2,
        Size = "15.6",
        Resolution = "3840x2160",
        PanelType = "OLED"
      };

      var display3 = new Display
      {
        Id = 3,
        Size = "13.3",
        Resolution = "1920x1080",
        PanelType = "IPS"
      };

      var brand1 = new Brand
      {
        Id = 1,
        Name = "Dell"
      };

      var brand2 = new Brand
      {
        Id = 2,
        Name = "HP"
      };

      var brand3 = new Brand
      {
        Id = 3,
        Name = "Lenovo"
      };

      var cpu1 = new Cpu
      {
        Id = 1,
        Brand = "Intel",
        Model = "Core i5"
      };

      var cpu2 = new Cpu
      {
        Id = 2,
        Brand = "Intel",
        Model = "Core i7"
      };

      var cpu3 = new Cpu
      {
        Id = 3,
        Brand = "AMD",
        Model = "Ryzen 5"
      };

      var memory1 = new Memory
      {
        Id = 1,
        Capacity = "8",
        Speed = "2400"
      };

      var memory2 = new Memory
      {
        Id = 2,
        Capacity = "16",
        Speed = "2666"
      };

      var memory3 = new Memory
      {
        Id = 3,
        Capacity = "32",
        Speed = "3200"
      };

      var storage1 = new Storage
      {
        Id = 1,
        Type = "SSD",
        Capacity = "256"
      };

      var storage2 = new Storage
      {
        Id = 2,
        Type = "SSD",
        Capacity = "512"
      };

      var storage3 = new Storage
      {
        Id = 3,
        Type = "HDD",
        Capacity = "1000"
      };

      var notebook1 = new Notebook
      {
        Id = 1,
        ModelId = 1,
        Model = model1,
        BrandId = 1,
        Brand = brand1,
        CpuId = 1,
        Cpu = cpu1,
        DisplayId = 1,
        Display = display1,
        MemoryId = 1,
        Memory = memory1,
        StorageId = 1,
        Storage = storage1,
        Price = 599,
        Color = "Black"
      };

      var notebook2 = new Notebook
      {
        Id = 2,
        ModelId = 2,
        Model = model2,
        BrandId = 1,
        Brand = brand1,
        CpuId = 2,
        Cpu = cpu2,
        DisplayId = 2,
        Display = display2,
        MemoryId = 2,
        Memory = memory2,
        StorageId = 2,
        Storage = storage2,
        Price = 999,
        Color = "Silver"
      };

      var notebook3 = new Notebook
      {
        Id = 3,
        ModelId = 3,
        Model = model3,
        BrandId = 1,
        Brand = brand1,
        CpuId = 3,
        Cpu = cpu3,
        DisplayId = 3,
        Display = display3,
        MemoryId = 3,
        Memory = memory3,
        StorageId = 3,
        Storage = storage3,
        Price = 1299,
        Color = "White"
      };

      PrintNotebook(notebook1);
      PrintNotebook(notebook2);
      PrintNotebook(notebook3);
    }
  }
}
