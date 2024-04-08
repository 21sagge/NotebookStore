using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        Console.WriteLine();

        context.Database.EnsureDeleted();
        Console.WriteLine("Database deleted.\n");

        context.Database.EnsureCreated();
        Console.WriteLine("Database created.\n");

        Create(context);

        Read(context);

        // UpdateColor(context, 1, "Green");
        // UpdateColor(context, 2, "White");
        // UpdateColor(context, 3, "Red");

        // Read(context);
    }

    private static void Create(NotebookStoreContext.NotebookStoreContext context)
    {
        var appleBrand = context.Brands.FirstOrDefault(b => b.Name == "Apple") ?? new Brand { Name = "Apple" };

        var appleDisplay = context.Displays.FirstOrDefault(d => d.PanelType == "IPS") ??
            new Display
            {
                PanelType = "IPS",
                ResolutionWidth = 2560,
                ResolutionHeight = 1600,
                Size = 13.3
            };

        var ssd256 = context.Storages.FirstOrDefault(s => s.Capacity == 256 && s.Type == "SSD") ??
            new Storage
            {
                Capacity = 256,
                Type = "SSD"
            };

        var ssd512 = context.Storages.FirstOrDefault(s => s.Capacity == 512 && s.Type == "SSD") ??
            new Storage
            {
                Capacity = 512,
                Type = "SSD"
            };

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
            Storage = ssd256
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
                Capacity = 1000,
                Type = "HDD"
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
            Storage = ssd512
        };

        var newNotebook4 = new Notebook
        {
            Brand = appleBrand,
            Model = new Model { Name = "MacBook Pro" },
            Color = "Space Gray",
            Price = 2000,
            Cpu = new Cpu
            {
                Brand = "Apple",
                Model = "M1"
            },
            Display = appleDisplay,
            Memory = new Memory
            {
                Capacity = 16,
                Speed = 3733
            },
            Storage = ssd512
        };

        var newNotebook5 = new Notebook
        {
            Brand = appleBrand,
            Model = new Model { Name = "MacBook Air" },
            Color = "Gold",
            Price = 1200,
            Cpu = new Cpu
            {
                Brand = "Apple",
                Model = "M2"
            },
            Display = appleDisplay,
            Memory = new Memory
            {
                Capacity = 8,
                Speed = 3733
            },
            Storage = ssd256
        };

        context.Notebooks.AddRange(newNotebook1, newNotebook2, newNotebook3, newNotebook4, newNotebook5);

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
            if (notebook.Brand == null || notebook.Model == null || notebook.Cpu == null || notebook.Display == null || notebook.Memory == null || notebook.Storage == null)
            {
                Console.WriteLine("Notebook not found.\n");
                return;
            }

            Console.WriteLine($"{notebook.Brand.Name} {notebook.Model.Name}");
            Console.WriteLine($"\tColor: \t\t{notebook.Color}");
            Console.WriteLine($"\tPrice: \t\t{notebook.Price}€");
            Console.WriteLine($"\tCPU: \t\t{notebook.Cpu.Brand} {notebook.Cpu.Model}");
            Console.WriteLine($"\tDisplay: \t{notebook.Display.Size}\" {notebook.Display.ResolutionWidth}x{notebook.Display.ResolutionHeight} {notebook.Display.PanelType}");
            Console.WriteLine($"\tMemory: \t{notebook.Memory.Capacity}GB {notebook.Memory.Speed}MHz");
            Console.WriteLine($"\tStorage: \t{notebook.Storage.Capacity}GB {notebook.Storage.Type}");
            Console.WriteLine();
        }
    }

    /// <summary>Method <c>UpdateColor</c> updates the color of a notebook.</summary>
    /// <param name="context">The database context.</param>
    /// <param name="id">The id of the notebook.</param>
    /// <param name="color">The new color of the notebook.</param>
    /// <returns>The updated notebook.</returns>
    private static void UpdateColor(NotebookStoreContext.NotebookStoreContext context, int id, string color)
    {
        var notebook = context.Notebooks
            .Include(n => n.Brand)
            .Include(n => n.Model)
            .Include(n => n.Cpu)
            .Include(n => n.Display)
            .Include(n => n.Memory)
            .Include(n => n.Storage)
            .FirstOrDefault(n => n.Id == id);

        if (notebook == null)
        {
            Console.WriteLine("Notebook not found.\n");
            return;
        }

        notebook.Color = color;

        context.SaveChanges();

        if (notebook.Brand == null || notebook.Model == null || notebook.Cpu == null || notebook.Display == null || notebook.Memory == null || notebook.Storage == null)
        {
            Console.WriteLine("Notebook not found.\n");
            return;
        }

        Console.WriteLine($"{notebook.Brand.Name} {notebook.Model.Name} updated color to {color}.\n");
    }
}
