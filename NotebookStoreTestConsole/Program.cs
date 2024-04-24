using NotebookStore.Entities;
using NotebookStore.DAL;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        using var context = new NotebookStoreContext.NotebookStoreContext();
        var unitOfWork = new UnitOfWork(context);

        var brand = new Brand { Name = "Dell" };
        unitOfWork.Brands.Create(brand);

        var brands = unitOfWork.Brands.Read().Result;
        foreach (var b in brands)
        {
            Console.WriteLine(b.Name);
        }

        brand.Name = "HP";
        unitOfWork.Brands.Update(brand);

        brands = unitOfWork.Brands.Read().Result;
        foreach (var b in brands)
        {
            Console.WriteLine(b.Name);
        }

        unitOfWork.Brands.Delete(brand.Id);

        brands = unitOfWork.Brands.Read().Result;
        foreach (var b in brands)
        {
            Console.WriteLine(b.Name);
        }
    }
}
