using System;

namespace CanHazFunny;

public class Jester
{
    private IJokeOut JokeOut { get; }
    private IJokeService JokeService { get; }

    public Jester(IJokeOut jokeOut, IJokeService jokeService)
    {
        if (jokeOut == null)
            throw new ArgumentNullException(nameof(jokeOut));
        JokeOut = jokeOut;

        if (jokeService == null)
            throw new ArgumentNullException(nameof(jokeService));
        JokeService = jokeService;
    }

    public void TellJoke()
    {
        string joke = JokeService.GetJoke();
        while (joke.Contains("Chuck Norris", StringComparison.OrdinalIgnoreCase))
        {
            joke = JokeService.GetJoke();
        }
        JokeOut.OutputJoke(joke);
    }
}
