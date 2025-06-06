namespace dotnet_stocks_api.Services;

public class TestService
{
    public int AddNumbers(int a, int b, int c)
        {
            if (a == 0)
            {
                throw new Exception("Number Cannot be 0");
            }

            if (b == 0)
            {
                throw new Exception("Number Cannot be 0");
            }

            if (c == 0)
            {
                throw new Exception("Number Cannot be 0");
            }

            if (a == b)
            {
                throw new Exception("Two Numbers Cannot be Same");
            }

            if (b == c)
            {
                throw new Exception("Two Numbers Cannot be Same");
            }

            if (c == a)
            {
                throw new Exception("Two Numbers Cannot be Same");
            }
            return a+b+c;
        }
}