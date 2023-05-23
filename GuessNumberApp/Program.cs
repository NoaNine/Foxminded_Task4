using Communicator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using IHost host = Host.CreateDefaultBuilder(args)
            //    .ConfigureAppConfiguration(app =>
            //    {
            //        app.SetBasePath(Directory.GetCurrentDirectory());
            //        app.AddJsonFile("appsettings.json");
            //    })
            //    .ConfigureServices((_, services) =>
            //    {
            //        services.Configure<GameSettingsExt>(_.Configuration.GetSection("Settings"));
            //        services.AddScoped(typeof(INumberGenerator), typeof(NumberGenerator));
            //    })
            //    .Build();

            //var game = host.Services.GetService<GameExt>() ?? throw new ArgumentNullException("Game");

            NumberGenerator generator = new NumberGenerator(0, 100);
            Croupier croupier = new Croupier(Console.ReadLine, Console.WriteLine, generator);
            Console.WriteLine(Resources.Messages.Greeting);
            while (true)
            {
                if (croupier.Winner)
                {
                    croupier.PlayAgain();
                }
                if (!croupier.Retry)
                {
                    break;
                }
                if (!croupier.Winner && croupier.Retry)
                {
                    Console.WriteLine(Resources.Messages.Instruction);
                    croupier.AskMeNumber();
                }
            }
        }
    }
}