using GameGuessNumber;
using GameGuessNumber.Interface;
using GuessNumberApp.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app =>
                {
                    app.SetBasePath(Directory.GetCurrentDirectory());
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((_, services) =>
                {
                    services.Configure<Settings>(_.Configuration.GetSection("Settings"));
                    services.AddTransient(typeof(IUserInteractionReader), typeof(ConsoleReader));
                    services.AddTransient(typeof(IUserInteractionWriter), typeof(ConsoleWriter));
                    services.AddScoped(typeof(INumberGenerator), typeof(NumberGenerator));
                    services.AddSingleton(typeof(Game));
                })
                .Build();
            var game = host.Services.GetService<Game>() ?? throw new ArgumentNullException("Game");
            Console.WriteLine(Messages.Greeting);
            game.StartGame();
        }
    }
}