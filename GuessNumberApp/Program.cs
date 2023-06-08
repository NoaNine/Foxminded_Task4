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
            Console.WriteLine(Messages.Instruction);
            do
            {
                game.StartGame();
            }
            while (PlayAgain());
        }
        
        private static bool PlayAgain()
        {
            Console.WriteLine(Messages.Again);
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "yes": return true;
                case "no": return false;
                default: throw new ArgumentException("Incorrect answer");
            }
        }
    }
}