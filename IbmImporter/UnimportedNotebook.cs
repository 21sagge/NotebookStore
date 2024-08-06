using IbmImporter.Models;

namespace IbmImporter
{
    public class UnimportedNotebook
    {
        public int Index { get; set; }
        public Notebook Notebook { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
    }
}