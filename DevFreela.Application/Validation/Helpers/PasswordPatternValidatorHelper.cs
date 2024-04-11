using System.Text.RegularExpressions;

namespace DevFreela.Application.Validation.Helpers;

public class PasswordPatternValidatorHelper()
{

    public static bool CheckPatternPasswordPattern(string password)
    {
        string patternRegex = "^(?=.*[0-9])(?=.*[^\\w\\s])(?=(?:[^a-z]*[a-z]){3})(?=.*[A-Z])(?!.*(.)\\1{2}).{8,}$";

        var isMatch = Regex.IsMatch(password, patternRegex);

        return isMatch;

    }

}
