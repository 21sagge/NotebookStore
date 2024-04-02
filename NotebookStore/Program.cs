using System.Runtime.InteropServices;

namespace NotebookStore
{
  class Program
  {
    public static void PrintNotebook(NotebookStore.Models.Notebook notebook)
    {
      System.Console.WriteLine("Notebook:");
      System.Console.WriteLine($"Id: {notebook.Id}");
      System.Console.WriteLine($"Color: {notebook.Color}");
      System.Console.WriteLine($"Price: {notebook.Price}");
      System.Console.WriteLine($"BrandId: {notebook.BrandId}");
      System.Console.WriteLine($"BrandName: {notebook.Brand.Name}");
      System.Console.WriteLine($"ModelId: {notebook.ModelId}");
      System.Console.WriteLine($"CpuId: {notebook.CpuId}");
      System.Console.WriteLine($"DisplayId: {notebook.DisplayId}");
      System.Console.WriteLine($"MemoryId: {notebook.MemoryId}");
      System.Console.WriteLine($"StorageId: {notebook.StorageId}");
      System.Console.WriteLine();
    }

    static void Main(string[] args)
    {
      var model1 = new NotebookStore.Models.Model
      {
        Id = 1,
        Name = "Inspiron 15 3000"
      };

      var model2 = new NotebookStore.Models.Model
      {
        Id = 2,
        Name = "Inspiron 15 5000"
      };

      var model3 = new NotebookStore.Models.Model
      {
        Id = 3,
        Name = "XPS 13"
      };

      var display1 = new NotebookStore.Models.Display
      {
        Id = 1,
        Size = "15.6",
        Resolution = "1920x1080"
      };

      var display2 = new NotebookStore.Models.Display
      {
        Id = 2,
        Size = "15.6",
        Resolution = "3840x2160"
      };

      var display3 = new NotebookStore.Models.Display
      {
        Id = 3,
        Size = "13.3",
        Resolution = "1920x1080"
      };

      var brand1 = new NotebookStore.Models.Brand
      {
        Id = 1,
        Name = "Dell"
      };

      var brand2 = new NotebookStore.Models.Brand
      {
        Id = 2,
        Name = "HP"
      };

      var brand3 = new NotebookStore.Models.Brand
      {
        Id = 3,
        Name = "Lenovo"
      };

      var cpu1 = new NotebookStore.Models.Cpu
      {
        Id = 1,
        Brand = "Intel",
        Model = "Core i5"
      };

      var cpu2 = new NotebookStore.Models.Cpu
      {
        Id = 2,
        Brand = "Intel",
        Model = "Core i7"
      };

      var cpu3 = new NotebookStore.Models.Cpu
      {
        Id = 3,
        Brand = "AMD",
        Model = "Ryzen 5"
      };

      var memory1 = new NotebookStore.Models.Memory
      {
        Id = 1,
        Capacity = "8",
        Speed = "2400"
      };

      var memory2 = new NotebookStore.Models.Memory
      {
        Id = 2,
        Capacity = "16",
        Speed = "2666"
      };

      var memory3 = new NotebookStore.Models.Memory
      {
        Id = 3,
        Capacity = "32",
        Speed = "3200"
      };

      var storage1 = new NotebookStore.Models.Storage
      {
        Id = 1,
        Type = "SSD",
        Capacity = "256"
      };

      var storage2 = new NotebookStore.Models.Storage
      {
        Id = 2,
        Type = "SSD",
        Capacity = "512"
      };

      var storage3 = new NotebookStore.Models.Storage
      {
        Id = 3,
        Type = "HDD",
        Capacity = "1000"
      };

      var notebook1 = new NotebookStore.Models.Notebook
      {
        Id = 1,
        ModelId = 1,
        BrandId = 1,
        CpuId = 1,
        DisplayId = 1,
        MemoryId = 1,
        StorageId = 1,
        Price = 599,
        Brand = brand1
      };

      var notebook2 = new NotebookStore.Models.Notebook
      {
        Id = 2,
        ModelId = 2,
        BrandId = 1,
        CpuId = 2,
        DisplayId = 2,
        MemoryId = 2,
        StorageId = 2,
        Price = 999,
        Brand = brand2
      };

      var notebook3 = new NotebookStore.Models.Notebook
      {
        Id = 3,
        ModelId = 3,
        BrandId = 1,
        CpuId = 3,
        DisplayId = 3,
        MemoryId = 3,
        StorageId = 3,
        Price = 1299,
        Brand = brand3
      };

      var program = new Program();

            PrintNotebook(notebook1);

            PrintNotebook(notebook2);

            PrintNotebook(notebook3);

      System.Console.WriteLine("Press any key to exit...");

      System.Console.ReadKey();
    }
  }
}