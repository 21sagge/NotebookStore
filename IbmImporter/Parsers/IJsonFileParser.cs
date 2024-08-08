using IbmImporter.Models;

namespace IbmImporter;

public interface IJsonFileParser
{
	/// <summary>
	/// Parses the json file and returns the notebook data
	/// </summary>
	/// <param name="json">Json file</param>
	/// <returns>Notebook data</returns>
	/// <exception cref="InvalidOperationException">Parser cannot process the file</exception>
	NotebookData Parse(string json);
}
