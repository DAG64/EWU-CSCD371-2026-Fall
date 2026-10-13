using System;

namespace CanHazFunny;

public class Program
{
    static void Main(string[] args)
    {
        Jester jester = new Jester(new JokeOut(), new JokeService());
        jester.TellJoke();
    }
}
