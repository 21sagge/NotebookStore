using IbmImporter.Models;

namespace IbmImporter
{
    public class UnimportedNotebook
    {
        public int Index { get; }
        public Notebook Notebook { get; }
        public string ErrorMessage { get; } = string.Empty;

        public UnimportedNotebook(int index, Notebook notebook, string errorMessage)
        {
            Index = index;
            Notebook = notebook;
            ErrorMessage = errorMessage;
        }
    }
}