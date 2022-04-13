public class IsCharNumber
{
    public static bool Check(char input)
    {
        if ((input == '0')
            || (input == '1')
            || (input == '2')
            || (input == '3')
            || (input == '4')
            || (input == '5')
            || (input == '6')
            || (input == '7')
            || (input == '8')
            || (input == '9'))
            return true;
        else
            return false;
    }
}
