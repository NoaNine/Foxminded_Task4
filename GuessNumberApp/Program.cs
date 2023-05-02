using Communicator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace GuessNumberApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var value = ConfigurationManager.AppSettings["minRange"];
            Settings settings = new Settings();


            var host = CreateHostBuilder(args).Build();
            var myService = host.Services.GetRequiredService<MyService>();
            myService.DoSomething();



            Generator generator = new Generator(0, 100);
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {

                services.AddTransient<MyService>();

            });
    }
}