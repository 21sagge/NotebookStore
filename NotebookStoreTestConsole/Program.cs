using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        Console.WriteLine();

        Delete(context);

        Create(context);

        Read(context);
    }

    private static void Create(NotebookStoreContext.NotebookStoreContext context)
    {
        context.Database.EnsureCreated();

        var newNotebook1 = new Notebook
        {
            Brand = new Brand { Name = "Dell" },
            Model = new Model { Name = "Inspiron" },
            Color = "Silver",
            Price = 1000,
            Cpu = new Cpu
            {
                Brand = "Intel",
                Model = "Core i5"
            },
            Display = new Display
            {
                Size = 15.6,
                ResolutionWidth = 1920,
                ResolutionHeight = 1080,
                PanelType = "IPS"
            },
            Memory = new Memory
            {
                Capacity = 8,
                Speed = 2666
            },
            Storage = new Storage
            {
                Capacity = 256,
                Type = "SSD"
            }
        };

        var newNotebook2 = new Notebook
        {
            Brand = new Brand { Name = "HP" },
            Model = new Model { Name = "Pavilion" },
            Color = "Black",
            Price = 1200,
            Cpu = new Cpu
            {
                Brand = "AMD",
                Model = "Ryzen 5"
            },
            Display = new Display
            {
                Size = 17.3,
                ResolutionWidth = 1920,
                ResolutionHeight = 1080,
                PanelType = "OLED"
            },
            Memory = new Memory
            {
                Capacity = 16,
                Speed = 3200
            },
            Storage = new Storage
            {
                Capacity = 512,
                Type = "SSD"
            }
        };

        var newNotebook3 = new Notebook
        {
            Brand = new Brand { Name = "Lenovo" },
            Model = new Model { Name = "ThinkPad" },
            Color = "Black",
            Price = 1500,
            Cpu = new Cpu
            {
                Brand = "Intel",
                Model = "Core i7"
            },
            Display = new Display
            {
                Size = 14,
                ResolutionWidth = 2560,
                ResolutionHeight = 1440,
                PanelType = "IPS"
            },
            Memory = new Memory
            {
                Capacity = 32,
                Speed = 3200
            },
            Storage = new Storage
            {
                Capacity = 1024,
                Type = "SSD"
            }
        };

        context.Notebooks.Add(newNotebook1);
        context.Notebooks.Add(newNotebook2);
        context.Notebooks.Add(newNotebook3);

        context.SaveChanges();

        Console.WriteLine("Notebooks created.\n");
    }

    private static void Read(NotebookStoreContext.NotebookStoreContext context)
    {
        var notebooks = context.Notebooks
            .Include(n => n.Brand)
            .Include(n => n.Model)
            .Include(n => n.Cpu)
            .Include(n => n.Display)
            .Include(n => n.Memory)
            .Include(n => n.Storage)
            .ToList();

        foreach (var notebook in notebooks)
        {
            Console.WriteLine($"{notebook.Brand.Name} {notebook.Model.Name}");
            Console.WriteLine($"  Color: \t{notebook.Color}");
            Console.WriteLine($"  Price: \t{notebook.Price}€");
            Console.WriteLine($"  CPU: \t\t{notebook.Cpu.Brand} {notebook.Cpu.Model}");
            Console.WriteLine($"  Display: \t{notebook.Display.Size}\" {notebook.Display.ResolutionWidth}x{notebook.Display.ResolutionHeight} {notebook.Display.PanelType}");
            Console.WriteLine($"  Memory: \t{notebook.Memory.Capacity}GB {notebook.Memory.Speed}MHz");
            Console.WriteLine($"  Storage: \t{notebook.Storage.Capacity}GB {notebook.Storage.Type}");
            Console.WriteLine();
        }
    }

    private static void Update(NotebookStoreContext.NotebookStoreContext context)
    {
        var notebook = context.Notebooks
            .Include(n => n.Brand)
            .Include(n => n.Model)
            .Include(n => n.Cpu)
            .Include(n => n.Display)
            .Include(n => n.Memory)
            .Include(n => n.Storage)
            .FirstOrDefault();

        if (notebook == null)
        {
            Console.WriteLine("Notebook not found.\n");
            return;
        }

        notebook.Color = "Red";

        context.SaveChanges();

        Console.WriteLine("Notebook updated.\n");
    }

    private static void Delete(NotebookStoreContext.NotebookStoreContext context)
    {
        context.Database.EnsureDeleted();

        Console.WriteLine("Database deleted.\n");
    }
}
