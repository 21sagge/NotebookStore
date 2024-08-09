using System.Security.Cryptography;
using IbmImporter.Models;
using NotebookStore.DAL;

namespace IbmImporter;

public class DataImporter
{
    private readonly IJsonFileParser parser;
    private readonly IValidator<Notebook> validator;
    private readonly IRepository<NotebookStore.Entities.Notebook> notebookRepository;

    public DataImporter(
        IJsonFileParser parser,
        IValidator<Notebook> validator,
        IRepository<NotebookStore.Entities.Notebook> notebookRepository)
    {
        this.parser = parser;
        this.validator = validator;
        this.notebookRepository = notebookRepository;
    }

    public async Task<ImportResult> ImportAsync(string file)
    {
        var importResult = new ImportResult();

        var notebookData = parser.Parse(file);

        foreach (var notebook in notebookData.Notebooks)
        {
            var validationResult = validator.Validate(notebook);

            if (!string.IsNullOrEmpty(validationResult))
            {
                importResult.Unsuccess.Add(new UnimportedNotebook(notebookData.Notebooks.IndexOf(notebook), notebook, validationResult));

                continue;
            }

            var importNotebookResult = await ImportNotebook(notebook, notebookData.Customer);

            if (!string.IsNullOrEmpty(importNotebookResult))
            {
                importResult.Unsuccess.Add(new UnimportedNotebook(notebookData.Notebooks.IndexOf(notebook), notebook, importNotebookResult));

                continue;
            }

            importResult.Success++;
        }

        return importResult;
    }

    private async Task<string> ImportNotebook(Notebook notebook, string customer)
    {
        var culture = new System.Globalization.CultureInfo("it-IT");

        NotebookStore.Entities.Notebook notebookEntity = new()
        {
            Color = notebook.Color,
            Price = (int)notebook.Price,
            Brand = new()
            {
                Name = notebook.Name,
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture)
            },
            Cpu = new()
            {
                Model = notebook.ProcessorModel,
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture)
            },
            Display = new()
            {
                ResolutionWidth = notebook.Monitor.Width,
                ResolutionHeight = notebook.Monitor.Height,
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture)
            },
            Memory = new()
            {
                Capacity = notebook.Ram,
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture)
            },
            Model = new()
            {
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture),
            },
            Storage = new()
            {
                // Capacità casuale per evitare unique constraint del database 
                // (perché non viene passata la capacità dello storage nel json)
                Capacity = RandomNumberGenerator.GetInt32(128, 1024),
                CreatedBy = customer,
                CreatedAt = DateTime.Now.ToString(culture)
            },
            CreatedBy = customer,
            CreatedAt = DateTime.Now.ToString(culture)
        };

        try
        {
            await notebookRepository.Create(notebookEntity);
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
            {
                return e.InnerException.Message;
            }

            return e.Message;
        }

        return string.Empty;
    }
}
