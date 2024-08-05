using IbmImporter.Models;

namespace IbmImporter
{
    public class UnimportedNotebook
    {
        public Notebook Notebook { get; set; }
        public string ErrorMessage { get; set; }
        public int Index { get; set; }
    }
}