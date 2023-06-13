using GameGuessNumber;
using GameGuessNumber.Interface;
using GuessNumberApp.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessNumberApp
{
    public class Program
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
                    services.AddScoped(typeof(INumberGenerator), typeof(NumberGenerator));
                    services.AddTransient(typeof(IUserInteractionReader), typeof(ConsoleReader));
                    services.AddTransient(typeof(IUserInteractionWriter), typeof(ConsoleWriter));
                    //services.AddSingleton(typeof(Game));
                    services.AddSingleton(typeof(IGame), typeof(GameCore));
                })
                .Build();
            var game = host.Services.GetService<IGame>() ?? throw new ArgumentNullException("Game");
            game.Notify += UserInteraction.MessageHandler;
            UserInteraction.Writer(Messages.Greeting);
            try
            {
                do
                {
                    game.StartGame();
                }
                while (UserInteraction.PlayAgain());
            }
            catch (Exception ex)
            {
                UserInteraction.Writer(ex.ToString());
            }
        }
    }
}