
namespace Shared.Utilities.Results;
public interface IResult
{
	string Message { get; }
	bool Success { get; }
}
