using GameGuessNumber;
using GuessNumberApp.Resources;

namespace GuessNumberApp
{
    public class UserInteraction
    {
        public static bool PlayAgain()
        {
            Writer(Messages.Again);
            string answer = Reader();
            switch (answer)
            {
                case "yes": return true;
                case "no": return false;
                default: throw new ArgumentException("Incorrect answer");
            }
        }

        public static void MessageHandler(GameCore gameCore)
        {
            switch (gameCore.Status)
            {
                case GameStatus.Start:
                    {
                        Writer(Messages.Instruction);
                        break;
                    }
                case GameStatus.Limit:
                    {
                        Writer(Messages.AttemptLimit);
                        break;
                    }
                case GameStatus.Bigger:
                    {
                        Writer(Messages.Bigger);
                        break;
                    }
                case GameStatus.Smaller:
                    {
                        Writer(Messages.Smaller);
                        break;
                    }
                case GameStatus.Win:
                    {
                        Writer(Messages.Winner);
                        break;
                    }
                case GameStatus.LetsTry:
                    {
                        int answer = GetInt();
                        gameCore.SetUserNumber(answer);
                        break;
                    }
                default: throw new ArgumentException("Set status is not processed or does not exist");
            }
        }

        public static string Reader() => Console.ReadLine();

        public static void Writer(string message) => Console.WriteLine(message);

        private static int GetInt()
        {
            string input = Reader();
            if (!int.TryParse(input, out int outputNamber))
            {
                throw new ArgumentException("Must be only a positive number");
            }
            return outputNamber;
        }
    }
}
