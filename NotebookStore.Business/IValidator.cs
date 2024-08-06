namespace IbmImporter;

public interface IValidator<T>
{
	/// <summary>
	///	Validates the model.
	/// </summary>
	/// <param name="model"> The model to validate. </param>
	///	<returns>
	/// A string containing the error message if the model is invalid, otherwise an empty string.
	/// </returns>
	string Validate(T model);
}
