using IbmImporter.Models;

namespace IbmImporter;

public interface IJsonFileParser
{
	/// <summary>
	/// Parses the json file and returns the notebook data
	/// </summary>
	/// <param name="json">Json file</param>
	/// <returns>Notebook data</returns>
	NotebookData? Parse(string json);
}
