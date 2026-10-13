using System.Net.Http;
using System;

namespace CanHazFunny;

public class JokeService : IJokeService
{
    private HttpClient HttpClient { get; } = new();

    public string GetJoke()
    {
        return HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api").Result;
        
    }
}
