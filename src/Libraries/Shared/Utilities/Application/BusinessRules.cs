using Shared.Utilities.Results;

namespace Shared.Utilities.Application;

public class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (IResult logic in logics)
        {
            if (!logic.Success)
            {
                return logic;
            }
        }
        return null;
    }
}