using System;
namespace CanHazFunny;

public class JokeOut : IJokeOut
{
    public void OutputJoke(string joke)
    {
        Console.WriteLine(joke);
    }
}
