using System.Security.Cryptography.X509Certificates;

namespace Communicator
{
    public class Croupier
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;
        private int _number = 35;
        private int _tryCounter = 0;

        public Croupier(Func<string> inputProvider, Action<string> outputProvider) 
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }

        public void AskMeNumber()
        {
            string input = _inputProvider() ?? string.Empty;
            _tryCounter++;
            if(!int.TryParse(input, out int inputNamber))
            {
                InvalidNumber();
                return;
            }
            EqualsMyNumber(inputNamber);
        }

        private void InvalidNumber()
        {
            _outputProvider("You entered the incorrect number, must be a positive number from 0 to 100");
        }

        private void EqualsMyNumber(int inputNamber)
        {
            if (inputNamber == _number)
            {
                IsWinner();
            }
            IsBiggerOrSmaller(inputNamber);
        }

        private void IsBiggerOrSmaller(int inputNamber)
        {
            if(inputNamber < _number)
            {
                _outputProvider("My number is bigger, try again");
            }
            if (inputNamber > _number)
            {
                _outputProvider("My number is smaller, try again");
            }
        }

        private void IsWinner()
        {
            _outputProvider("You WIN, congratulation.\n" +
                "You want to play again?");
        }
    }
}