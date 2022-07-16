using Shared.Utilities.Results;

namespace Shared.Utilities.Application;

public static class BusinessRules
{
    /// <summary>
    /// Runs all rules of operation
    /// </summary>
    /// <param name="logics"></param>
    /// <returns></returns>
    public static IResult Run(params IResult[] logics)
    {
        return logics.FirstOrDefault(logic => !logic.Success);
    }
}