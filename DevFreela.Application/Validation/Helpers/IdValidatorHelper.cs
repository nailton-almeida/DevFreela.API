namespace DevFreela.Application.Validation.Helpers;

public class IdValidatorHelper
{
    public static bool IdIsInt(int id)
    {
        return id is int;
    }

    public static bool IdIsGuid(Guid id)
    {
        return id is Guid;
    }
}
